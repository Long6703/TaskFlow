using Application.Contracts.IRepository;
using Application.DTOs;
using Application.Exceptions;
using Application.IService;
using Application.Validation;
using AutoMapper;
using BCrypt.Net;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;


        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<IEnumerable<string>> GetUserPermissions(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterAsync(UserCreateDTO userCreateDTO)
        {
            _logger.LogInformation($"RegisterAsync called for {userCreateDTO.Email}");
            var _validator = new UserCreateDTOValidator(_userRepository);  
            var validationResult = await _validator.ValidateAsync(userCreateDTO);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning($"Validation failed: {string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}");
                throw new BadRequestException("Invalid Register Request", validationResult);
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userCreateDTO.Password);
            var user = _mapper.Map<User>(userCreateDTO);
            user.PasswordHash = hashedPassword;
            await _userRepository.AddAsync(user);
            _logger.LogInformation($"User {user.Email} registered successfully");
            return true;
        }
    }
}

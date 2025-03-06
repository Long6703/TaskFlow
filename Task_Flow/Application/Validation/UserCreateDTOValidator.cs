using Application.Contracts.IRepository;
using Application.DTOs;
using FluentValidation;

namespace Application.Validation
{
    public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>
    {
        private readonly IUserRepository _userRepository;

        public UserCreateDTOValidator(IUserRepository userRepository)
        {

            _userRepository = userRepository;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Full Name is required")
                .Length(2, 100).WithMessage("Full Name must be between 2 and 100 characters");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address")
                .MustAsync(async (email, cancellation) => !await _userRepository.EmailExistsAsync(email))
                .WithMessage("Email already exists. Please use a different email.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(8, 100).WithMessage("Password must be at least 8 characters long")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character");
        }
    }
}

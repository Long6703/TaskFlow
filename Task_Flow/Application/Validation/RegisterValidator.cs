using Application.Contracts.IRepository;
using FluentValidation;
using Shared.Models;

namespace Application.Validation
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        private readonly IAuthRepository _authRepository;

        public RegisterValidator(IAuthRepository authRepository)
        {

            _authRepository = authRepository;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Full Name is required")
                .Length(2, 100).WithMessage("Full Name must be between 2 and 100 characters");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address")
                .MustAsync(async (email, cancellation) => !await _authRepository.EmailExistsAsync(email));
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(8, 100).WithMessage("Password must be at least 8 characters long")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required")
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}

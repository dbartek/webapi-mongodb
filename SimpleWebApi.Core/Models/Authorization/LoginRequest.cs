using FluentValidation;

namespace SimpleWebApi.Core.Models.Authorization
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username).NotNull().Length(5, 20);
            RuleFor(x => x.Password).NotNull().Length(8, 20);
        }
    }
}
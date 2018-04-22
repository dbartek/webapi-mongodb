using FluentValidation;

namespace SimpleWebApi.Core.Models.Authorization
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModelValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.Username).NotNull().Length(5, 20);
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.Password).NotNull().Length(8, 20);
        }
    }
}
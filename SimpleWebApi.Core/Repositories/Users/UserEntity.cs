using SimpleWebApi.Core.Repositories.Base;

namespace SimpleWebApi.Core.Repositories.Users
{
    public class UserEntity : BaseEntity
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}


namespace SimpleWebApi.Core.Repositories.Users
{
    public interface IUsersRepository
    {
        UserEntity GetByUsername(string username);
        bool Create(UserEntity userEntity);
    }
}

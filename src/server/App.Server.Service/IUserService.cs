using App.Domain.Contracts;

namespace App.Server.Service
{
    public interface IUserService
    {
        string CreateUser(UserDto dto);
        bool Authenticate(UserDto dto);
    }
}
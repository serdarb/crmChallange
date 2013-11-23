using System.Linq;
using App.Domain;
using App.Domain.Contracts;
using App.Domain.Repo;
using AutoMapper;

namespace App.Server.Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly IEntityRepository<User> _userRepository;

        public UserService(IEntityRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public string CreateUser(UserDto dto)
        {
            // password should be stored with a bcrypt logic
            // skipped just to earn time.
            // you may want to check for a better sample
            // https://github.com/serdarb/TemelWebGuvenligi/blob/master/src/bcrypt/app.js

            if (string.IsNullOrEmpty(dto.Email)
                || string.IsNullOrEmpty(dto.Password))
            {
                return null;
            }

            var user = _userRepository.AsQueryable().FirstOrDefault(x => x.Email == dto.Email);
            if (user != null)
            {
                return null;
            }

            var item = Mapper.Map<UserDto, User>(dto);
            var result = _userRepository.Save(item);
            
            return result.Ok ? item.IdStr : null;
        }

        public bool Authenticate(UserDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email)
                || string.IsNullOrEmpty(dto.Password))
            {
                return false;
            }

            var user = _userRepository.AsQueryable().FirstOrDefault(x => x.Email == dto.Email);
            if (user == null)
            {
                return false;
            }

            return user.Password == dto.Password;
        }
    }
}

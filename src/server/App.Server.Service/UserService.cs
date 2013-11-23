using App.Domain;
using App.Domain.Contracts;
using App.Domain.Repo;
using AutoMapper;

namespace App.Server.Service
{
    public class UserService
    {
        private readonly IEntityRepository<User> _repository;

        public UserService(IEntityRepository<User> repository)
        {
            _repository = repository;
        }

        public string CreateUser(UserDto dto)
        {
            var item = Mapper.Map<UserDto, User>(dto);
            var result = _repository.Save(item);
            
            return result.Ok ? item.IdStr : null;
        }
    }
}

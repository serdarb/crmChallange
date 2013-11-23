using System.Linq;
using App.Domain;
using App.Domain.Contracts;
using App.Domain.Repo;
using AutoMapper;

namespace App.Server.Service
{
    public class CompanyService
    {
        private readonly IEntityRepository<Company> _companyRepository;
        private readonly IEntityRepository<User> _userRepository;

        public CompanyService(IEntityRepository<Company> companyRepository,
                              IEntityRepository<User> userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public string CreateCompany(CompanyDto dto)
        {
            if (string.IsNullOrEmpty(dto.AdminEmail)
                || string.IsNullOrEmpty(dto.Name))
            {
                return null;
            }

            var user = _userRepository.AsQueryable().FirstOrDefault(x => x.Email == dto.AdminEmail);
            if (user == null)
            {
                return null;
            }

            var company = _companyRepository.AsQueryable().FirstOrDefault(x => x.Name == dto.Name);
            if (company != null)
            {
                return null;
            }

            dto.AdminId = user.IdStr;
            var item = Mapper.Map<CompanyDto, Company>(dto);
            var result = _companyRepository.Save(item);

            return result.Ok ? item.IdStr : null;
        }
    }
}
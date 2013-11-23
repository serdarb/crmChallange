using System.Linq;
using App.Domain;
using App.Domain.Contracts;
using App.Domain.Repo;
using AutoMapper;

namespace App.Server.Service
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly EntityRepository<Customer> _customerRepository;
        private readonly EntityRepository<Company> _companyRepository;
        private readonly EntityRepository<User> _userRepository;

        public CustomerService(EntityRepository<Customer> customerRepository, 
                               EntityRepository<Company> companyRepository, 
                               EntityRepository<User> userRepository)
        {
            _customerRepository = customerRepository;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public string CreateCustomer(CustomerDto dto)
        {
            if (string.IsNullOrEmpty(dto.CreatedBy)
               || string.IsNullOrEmpty(dto.CompanyName)
               || string.IsNullOrEmpty(dto.Email))
            {
                return null;
            }

            var user = _userRepository.AsQueryable().FirstOrDefault(x => x.Email == dto.CreatedBy);
            if (user == null)
            {
                return null;
            }

            var company = _companyRepository.AsQueryable().FirstOrDefault(x => x.Name == dto.CompanyName);
            if (company == null)
            {
                return null;
            }

            dto.CompanyId = company.IdStr;
            var item = Mapper.Map<CustomerDto, Customer>(dto);
            var result = _customerRepository.Save(item);

            return result.Ok ? item.IdStr : null;
        }
    }
}
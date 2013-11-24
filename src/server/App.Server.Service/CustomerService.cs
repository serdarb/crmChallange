using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using App.Domain;
using App.Domain.Contracts;
using App.Domain.Repo;

namespace App.Server.Service
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly IEntityRepository<Customer> _customerRepository;
        private readonly IEntityRepository<Company> _companyRepository;
        private readonly IEntityRepository<User> _userRepository;

        public CustomerService(IEntityRepository<Customer> customerRepository,
                               IEntityRepository<Company> companyRepository,
                               IEntityRepository<User> userRepository)
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

        public Task<List<CustomerDto>> GetAll()
        {
            var customers = _customerRepository.FindAll();
            var list = customers.Select(Mapper.Map<Customer, CustomerDto>).ToList();
            return Task.FromResult(list);
        }
    }
}
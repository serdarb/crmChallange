
using System.Collections.Generic;
using System.Linq;
using App.Domain.Contracts;
using App.Domain.Repo;
using App.Server.Service;
using NUnit.Framework;

namespace App.Domain.Test
{
    [TestFixture]
    public class ServiceTests
    {
        private UserService _userService;
        private CompanyService _companyService;
        private CustomerService _customerService;
        private LocalizationService _localizationService;

        [TestFixtureSetUp]
        public void setup_tests()
        {
            AutoMapperConfiguration.CreateMaps();

            var userRepository = new EntityRepository<User>();
            userRepository.DeleteAll();

            var companyRepository = new EntityRepository<Company>();
            companyRepository.DeleteAll();

            var customerRepository = new EntityRepository<Customer>();
            customerRepository.DeleteAll();

            var localizaitonRepository = new EntityRepository<LocalizationString>();
            localizaitonRepository.DeleteAll();

            _userService = new UserService(userRepository);
            _companyService = new CompanyService(companyRepository, userRepository);
            _customerService = new CustomerService(customerRepository, companyRepository, userRepository);
            _localizationService = new LocalizationService(localizaitonRepository);
        }

        [Test]
        public void should_signup()
        {
            var dto = new UserDto
            {
                Email = "valid@email.com",
                FirstName = "Valid",
                LastName = "Name",
                Password = "password",
                Language = "en"
            };

            var id = _userService.CreateUser(dto);
            Assert.IsNotNull(id);
        }

        [Test]
        public void should_authenticate()
        {
            var dto = new UserDto
            {
                Email = "valid2@email2.com",
                FirstName = "Valid 2",
                LastName = "Name 2",
                Password = "password2",
                Language = "en"
            };

            var id = _userService.CreateUser(dto);
            Assert.IsNotNull(id);

            var result = _userService.Authenticate(dto);
            Assert.IsTrue(result);
        }

        [Test]
        public void should_create_company()
        {
            var user = new UserDto
            {
                Email = "valid3@email3.com",
                FirstName = "Valid 3",
                LastName = "Name 3",
                Password = "password3",
                Language = "en"
            };

            var id = _userService.CreateUser(user);
            Assert.IsNotNull(id);

            var company = new CompanyDto();
            company.Name = "Company Name";
            company.Url = "company.com";
            company.AdminEmail = user.Email;
            company.AdminId = id;
            company.Language = user.Language;
            company.CustomFields = new List<CustomFieldDto>();

            var companyId = _companyService.CreateCompany(company);
            Assert.IsNotNull(companyId);

            var user2 = _userService.GetUser(id);

            Assert.AreEqual(companyId, user2.CompanyId);
            Assert.AreEqual(company.Name, user2.CompanyName);
        }

        [Test]
        public void should_setup_customer_custom_fields()
        {
            var user = new UserDto
            {
                Email = "valid4@email4.com",
                FirstName = "Valid 4",
                LastName = "Name 4",
                Password = "password4",
                Language = "en"
            };

            var id = _userService.CreateUser(user);
            Assert.IsNotNull(id);

            var company = new CompanyDto
            {
                Name = "Company Name 2",
                Url = "company2.com",
                AdminEmail = user.Email,
                AdminId = id,
                Language = user.Language,
                CustomFields = new List<CustomFieldDto>()
            };

            var companyId = _companyService.CreateCompany(company);
            Assert.IsNotNull(companyId);

            var customFields = new List<CustomFieldDto>
            {
                new CustomFieldDto
                {
                    Name = "Birthday",
                    DisplayNameEn = "Birthday",
                    DisplayNameTr = "Doğum Günü"
                },
                new CustomFieldDto
                {
                    Name = "FavoriteTeam",
                    DisplayNameEn = "Favorite Team",
                    DisplayNameTr = "Favori Takım"
                }
            };

            var dto = new CustomFieldSettingDto
            {
                CustomFieldDtos = customFields,
                CompanyId = companyId
            };

            var result = _companyService.SetCustomerCustomFields(dto);
            Assert.IsTrue(result);
        }

        [Test]
        public void should_create_customer()
        {
            var user = new UserDto
            {
                Email = "valid5@email5.com",
                FirstName = "Valid 5",
                LastName = "Name 5",
                Password = "password5",
                Language = "en"
            };

            var id = _userService.CreateUser(user);
            Assert.IsNotNull(id);

            var company = new CompanyDto
            {
                Name = "Company Name 3",
                Url = "company3.com",
                AdminEmail = user.Email,
                AdminId = id,
                Language = user.Language,
                CustomFields = new List<CustomFieldDto>()
            };

            var companyId = _companyService.CreateCompany(company);
            Assert.IsNotNull(companyId);

            var customFields = new List<CustomFieldDto>
            {
                new CustomFieldDto
                {
                    Name = "Birthday",
                    DisplayNameEn = "Birthday",
                    DisplayNameTr = "Doğum Günü"
                },
                new CustomFieldDto
                {
                    Name = "FavoriteTeam",
                    DisplayNameEn = "Favorite Team",
                    DisplayNameTr = "Favori Takım"
                }
            };

            var dto = new CustomFieldSettingDto
            {
                CustomFieldDtos = customFields,
                CompanyId = companyId
            };

            var result = _companyService.SetCustomerCustomFields(dto);
            Assert.IsTrue(result);

            var customFieldValues = new List<NameValueDto>
            {
                new NameValueDto
                {
                    Name = "Birthday",
                    Value="1982-08-02"
                },
                new NameValueDto
                {
                    Name = "FavoriteTeam",
                    Value= "Beşiktaş"
                }
            };

            var customer = new CustomerDto
            {
                Email = "validcustomer@email.com",
                FirstName = "Customer",
                LastName = "Name",
                Password = "customerpassword",
                Language = "en",
                CompanyId = companyId,
                CompanyName = company.Name,
                CreatedBy = user.Email,
                CustomFieldValues = customFieldValues
            };

            var customerId = _customerService.CreateCustomer(customer);
            Assert.IsNotNull(customerId);
        }

        [Test]
        public void should_get_user()
        {
            var dto = new UserDto
            {
                Email = "valid6@email6.com",
                FirstName = "Valid 6",
                LastName = "Name 6",
                Password = "password6",
                Language = "en"
            };

            var id = _userService.CreateUser(dto);
            Assert.IsNotNull(id);

            var result = _userService.GetUser(id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, id);
            Assert.AreEqual(result.Email, dto.Email);
        }

        [Test]
        public void should_get_user_by_email()
        {
            var dto = new UserDto
            {
                Email = "valid7@email7.com",
                FirstName = "Valid 7",
                LastName = "Name 7",
                Password = "password7",
                Language = "en"
            };

            var id = _userService.CreateUser(dto);
            Assert.IsNotNull(id);

            var result = _userService.GetUserByEmail(dto.Email);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, id);
            Assert.AreEqual(result.Email, dto.Email);
        }

        [Test]
        public async void should_get_all_customers()
        {
            var user = new UserDto
            {
                Email = "valid8@email8.com",
                FirstName = "Valid 8",
                LastName = "Name 8",
                Password = "password8",
                Language = "en"
            };

            var id = _userService.CreateUser(user);
            Assert.IsNotNull(id);

            var company = new CompanyDto
            {
                Name = "Company Name 4",
                Url = "company4.com",
                AdminEmail = user.Email,
                AdminId = id,
                Language = user.Language,
                CustomFields = new List<CustomFieldDto>()
            };

            var companyId = _companyService.CreateCompany(company);
            Assert.IsNotNull(companyId);

            var customFields = new List<CustomFieldDto>
            {
                new CustomFieldDto
                {
                    Name = "Birthday",
                    DisplayNameEn = "Birthday",
                    DisplayNameTr = "Doğum Günü"
                },
                new CustomFieldDto
                {
                    Name = "FavoriteTeam",
                    DisplayNameEn = "Favorite Team",
                    DisplayNameTr = "Favori Takım"
                }
            };

            var dto = new CustomFieldSettingDto
            {
                CustomFieldDtos = customFields,
                CompanyId = companyId
            };

            var result = _companyService.SetCustomerCustomFields(dto);
            Assert.IsTrue(result);

            var customFieldValues = new List<NameValueDto>
            {
                new NameValueDto
                {
                    Name = "Birthday",
                    Value="1982-08-02"
                },
                new NameValueDto
                {
                    Name = "FavoriteTeam",
                    Value= "Beşiktaş"
                }
            };

            var customer = new CustomerDto
            {
                Email = "validcustomer2@email.com",
                FirstName = "Customer 2",
                LastName = "Name 2",
                Password = "customerpassword 2",
                Language = "en",
                CompanyId = companyId,
                CompanyName = company.Name,
                CreatedBy = user.Email,
                CustomFieldValues = customFieldValues
            };

            var customerId = _customerService.CreateCustomer(customer);
            Assert.IsNotNull(customerId);

            customer = new CustomerDto
            {
                Email = "validcustomer3@email.com",
                FirstName = "Customer 3",
                LastName = "Name 3",
                Password = "customerpassword 3",
                Language = "en",
                CompanyId = companyId,
                CompanyName = company.Name,
                CreatedBy = user.Email,
                CustomFieldValues = customFieldValues
            };

            customerId = _customerService.CreateCustomer(customer);
            Assert.IsNotNull(customerId);

            var customers = await _customerService.GetAll();
            Assert.IsNotNull(customers);
            Assert.GreaterOrEqual(customers.Count, 2);
        }

        [Test]
        public async void should_get_all_screen_texts()
        {
            var list = await _localizationService.GetAll("tr");
            Assert.IsNotNull(list);

            Assert.IsTrue(list.Any(x => x.Name == "Email"));
            Assert.IsTrue(list.Any(x => x.Name == "Email" && x.Value == "E-posta"));
        }
    }
}

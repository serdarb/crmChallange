
using System.Collections.Generic;
using App.Domain.Contracts;
using App.Domain.Repo;
using App.Server.Service;
using NUnit.Framework;

namespace App.Domain.Test
{
    [TestFixture]
    public class UserTests
    {
        private UserService _userService;
        private CompanyService _companyService;

        [SetUp]
        public void setup_tests()
        {
            AutoMapperConfiguration.CreateMaps();

            var userRepository = new EntityRepository<User>("TestDB");
            userRepository.DeleteAll();

            var companyRepository = new EntityRepository<Company>("TestDB");
            companyRepository.DeleteAll();

            _userService = new UserService(userRepository);
            _companyService = new CompanyService(companyRepository, userRepository);
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
            company.CustomFields = new List<string>();

            var companyId = _companyService.CreateCompany(company);
            Assert.IsNotNull(companyId);
        }

        [Test]
        public void should_setup_customer_custom_fields()
        {
        }

        [Test]
        public void should_create_customer()
        {
        }
    }
}

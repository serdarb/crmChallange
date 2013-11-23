
using App.Domain.Contracts;
using App.Domain.Repo;
using App.Server.Service;
using NUnit.Framework;

namespace App.Domain.Test
{
    [TestFixture]
    public class UserTests
    {
        private UserService _service;

        [SetUp]
        public void setup_tests()
        {
            AutoMapperConfiguration.CreateMaps();

            var repository = new EntityRepository<User>("TestDB");
            _service = new UserService(repository);
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
                Lang = "en"
            };

            var id = _service.CreateUser(dto);
            Assert.IsNotNull(id);
        }

        [Test]
        public void should_authenticate()
        {
        }

        [Test]
        public void should_create_company()
        {
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

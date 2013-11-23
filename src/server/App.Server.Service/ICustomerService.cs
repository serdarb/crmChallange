using App.Domain.Contracts;

namespace App.Server.Service
{
    public interface ICustomerService
    {
        string CreateCustomer(CustomerDto dto);
    }
}
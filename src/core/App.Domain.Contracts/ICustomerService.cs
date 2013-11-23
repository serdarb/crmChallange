using System.ServiceModel;

namespace App.Domain.Contracts
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        string CreateCustomer(CustomerDto dto);
    }
}
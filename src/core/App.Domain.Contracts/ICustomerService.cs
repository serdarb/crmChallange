using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace App.Domain.Contracts
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        string CreateCustomer(CustomerDto dto);

        [OperationContract]
        Task<List<CustomerDto>> GetAll();
    }
}
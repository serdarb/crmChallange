using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace App.Domain.Contracts
{
    [ServiceContract]
    public interface ICompanyService
    {
        [OperationContract]
        string CreateCompany(CompanyDto dto);
        [OperationContract]
        bool SetCustomerCustomFields(CustomFieldSettingDto dto);

        [OperationContract]
        Task<List<CustomFieldDto>> GetAllCustomFields(string companyId);

        [OperationContract]
        CompanyDto GetCompany(string id);

        [OperationContract]
        bool AddNewCustomerCustomField(string companyId, CustomFieldDto dto);
    }
}
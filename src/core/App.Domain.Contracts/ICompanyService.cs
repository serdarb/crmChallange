using System.ServiceModel;

namespace App.Domain.Contracts
{
    [ServiceContract]
    public interface ICompanyService
    {
        [OperationContract]
        string CreateCompany(CompanyDto dto);
        [OperationContract]
        bool SetCustomerCustomFields(CustomFieldSettingDto dto);
    }
}
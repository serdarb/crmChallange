using App.Domain.Contracts;

namespace App.Server.Service
{
    public interface ICompanyService
    {
        string CreateCompany(CompanyDto dto);

        bool SetCustomerCustomFields(CustomFieldSettingDto dto);
    }
}
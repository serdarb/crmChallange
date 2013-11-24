using App.Domain;
using App.Domain.Contracts;
using AutoMapper;

namespace App.Server.Service
{
    public class AutoMapperConfiguration
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap(typeof (UserDto), typeof (User));
            Mapper.CreateMap(typeof (User), typeof (UserDto));

            Mapper.CreateMap(typeof(CompanyDto), typeof(Company));
            Mapper.CreateMap(typeof(Company), typeof(CompanyDto));

            Mapper.CreateMap(typeof(CustomFieldDto), typeof(CustomField));
            Mapper.CreateMap(typeof(CustomField), typeof(CustomFieldDto));

            Mapper.CreateMap(typeof(NameValueDto), typeof(NameValue));
            Mapper.CreateMap(typeof(NameValue), typeof(NameValueDto));

            Mapper.CreateMap(typeof(CustomerDto), typeof(Customer));
            Mapper.CreateMap(typeof(Customer), typeof(CustomerDto));
        }
    }
}

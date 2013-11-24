using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace App.Domain.Contracts
{
    [ServiceContract]
    public interface ILocalizationService
    {
        [OperationContract]
        Task<List<NameValueDto>> GetAll(string culture);
    }
}
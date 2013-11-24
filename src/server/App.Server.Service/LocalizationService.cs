using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain;
using App.Domain.Contracts;
using App.Domain.Repo;
using AutoMapper;

namespace App.Server.Service
{
    public class LocalizationService : BaseService, ILocalizationService
    {
        private readonly IEntityRepository<LocalizationString> _repository;

        public LocalizationService(IEntityRepository<LocalizationString> repository)
        {
            _repository = repository;
        }

        public async Task<List<NameValueDto>> GetAll(string culture)
        {
            var strings = _repository.FindAll().ToList();
            if (!strings.Any())
            {
                InsertDefaultTexts();
                strings = _repository.FindAll().ToList();
            }

            var list = new List<NameValueDto>();
            switch (culture)
            {
                case "en":
                    list.AddRange(strings.Select(item => new NameValueDto { Name = item.Name, Value = item.EN }));

                    break;
                case "tr":
                    list.AddRange(strings.Select(item => new NameValueDto { Name = item.Name, Value = item.TR }));
                    break;
            }

            return await Task.FromResult(list);
        }

        private void InsertDefaultTexts()
        {
            var list = new List<LocalizationString>();

            list.Add(new LocalizationString { Name = "English", EN = "English", TR = "İngilizce" });
            list.Add(new LocalizationString { Name = "Turkish", EN = "Turkish", TR = "Türkçe" });

            list.Add(new LocalizationString { Name = "Language", EN = "Language", TR = "Dil" });
            list.Add(new LocalizationString { Name = "Email", EN = "Email", TR = "E-posta" });
            list.Add(new LocalizationString { Name = "Password", EN = "Password", TR = "Şifre" });

            list.Add(new LocalizationString { Name = "FirstName", EN = "First Name", TR = "Ad" });
            list.Add(new LocalizationString { Name = "LastName", EN = "Last Name", TR = "Soyad" });
            list.Add(new LocalizationString { Name = "CompanyName", EN = "Company Name", TR = "Firma Adı" });
            list.Add(new LocalizationString { Name = "CompanyUrl", EN = "Company Url", TR = "Firma Url" });

            list.Add(new LocalizationString { Name = "Signup", EN = "Signup", TR = "Üye Ol" });
            list.Add(new LocalizationString { Name = "Login", EN = "Login", TR = "Giriş" });
            list.Add(new LocalizationString { Name = "Logout", EN = "Logut", TR = "Çıkış" });

            _repository.AddBulk(list);
        }
    }
}
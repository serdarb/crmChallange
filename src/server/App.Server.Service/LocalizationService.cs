using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using App.Domain;
using App.Domain.Contracts;
using App.Domain.Repo;
using App.Utils;

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
                case ConstHelper.en:
                    list.AddRange(strings.Select(item => new NameValueDto { Name = item.Name, Value = item.EN }));

                    break;
                case ConstHelper.tr:
                    list.AddRange(strings.Select(item => new NameValueDto { Name = item.Name, Value = item.TR }));
                    break;
            }

            return await Task.FromResult(list);
        }

        private void InsertDefaultTexts()
        {
            var list = new List<LocalizationString>();

            list.Add(new LocalizationString { Name = "App", EN = "App", TR = "Uygulama" });

            list.Add(new LocalizationString { Name = "English", EN = "English", TR = "İngilizce" });
            list.Add(new LocalizationString { Name = "Turkish", EN = "Turkish", TR = "Türkçe" });

            list.Add(new LocalizationString { Name = "Language", EN = "Language", TR = "Dil" });
            list.Add(new LocalizationString { Name = "Email", EN = "Email", TR = "E-posta" });
            list.Add(new LocalizationString { Name = "Password", EN = "Password", TR = "Şifre" });

            list.Add(new LocalizationString { Name = "Name", EN = "Name", TR = "Ad" });
            list.Add(new LocalizationString { Name = "FirstName", EN = "First Name", TR = "Ad" });
            list.Add(new LocalizationString { Name = "LastName", EN = "Last Name", TR = "Soyad" });
            list.Add(new LocalizationString { Name = "CompanyName", EN = "Company Name", TR = "Firma Adı" });
            list.Add(new LocalizationString { Name = "CompanyUrl", EN = "Company Url", TR = "Firma Url" });

            list.Add(new LocalizationString { Name = "Signup", EN = "Signup", TR = "Üye Ol" });
            list.Add(new LocalizationString { Name = "Login", EN = "Login", TR = "Giriş" });
            list.Add(new LocalizationString { Name = "Logout", EN = "Logut", TR = "Çıkış" });
            list.Add(new LocalizationString { Name = "Home", EN = "Home", TR = "Ana Sayfa" });
            list.Add(new LocalizationString { Name = "Customer", EN = "Customer", TR = "Müşteri" });
            list.Add(new LocalizationString { Name = "ListCustomers", EN = "List Customers", TR = "Müşteri Listesi" });
            list.Add(new LocalizationString { Name = "AddNew", EN = "Add New", TR = "Yeni ekle" });
            list.Add(new LocalizationString { Name = "Settings", EN = "Settings", TR = "Ayarlar" });
            list.Add(new LocalizationString { Name = "ListCustomFields", EN = "List Custom Fields", TR = "Özel alanlar" });
            list.Add(new LocalizationString { Name = "AddNewCustomField", EN = "Add New Custom Field", TR = "Yeni özel alan ekle" });
            list.Add(new LocalizationString { Name = "Save", EN = "Save", TR = "Kaydet" });
            list.Add(new LocalizationString { Name = "NewCustomer", EN = "New Customer", TR = "Yeni Müşteri" });

            list.Add(new LocalizationString { Name = "CompanyCustomFields", EN = "Company Custom Fields", TR = "Özel Alanlar" });
            list.Add(new LocalizationString { Name = "DisplayNameTR", EN = "Display Name in Turkish", TR = "Türkçe Alan Adı" });
            list.Add(new LocalizationString { Name = "DisplayNameEN", EN = "Display Name in English", TR = "İngilizce Alan Adı" });

            list.Add(new LocalizationString { Name = "FailMsg", EN = "Failed, check fields and try again", TR = "Lütfen girdiğiniz değerleri kontrol edip tekrar deneyiniz" });



            _repository.AddBulk(list);
        }
    }
}

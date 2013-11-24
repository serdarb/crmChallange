using App.Utils;

namespace App.Client.Web.Models
{
    public class CustomFieldCreateModel : BaseModel
    {
        public string Name { get; set; }
        public string DisplayTr { get; set; }
        public string DisplayEn { get; set; }

        public bool IsValid(CustomFieldCreateModel model)
        {
            return !string.IsNullOrEmpty(model.DisplayTr)
                   && !string.IsNullOrEmpty(model.DisplayEn)
                   && !string.IsNullOrEmpty(model.Name);
        }
    }
}
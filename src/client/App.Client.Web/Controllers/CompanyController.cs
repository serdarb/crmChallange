using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using App.Client.Web.Models;
using App.Client.Web.Services;
using App.Domain.Contracts;

namespace App.Client.Web.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(
            IUserService userService,
            ICompanyService companyService,
            IFormsAuthenticationService formsAuthenticationService)
            : base(userService, formsAuthenticationService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var items = await _companyService.GetAllCustomFields(CurrentUser.CompanyId);
            return View(items);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View(new CustomFieldCreateModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(CustomFieldCreateModel model)
        {
            if (!model.IsValid(model))
            {
                model.Msg = ViewBag.Txt["FailMsg"];
                return View(model);
            }

            var dto = new CustomFieldDto
            {
                Name = model.Name,
                DisplayNameEn = model.DisplayEn,
                DisplayNameTr = model.DisplayTr
            };

            var setted = _companyService.AddNewCustomerCustomField(CurrentUser.CompanyId,dto);
            if (!setted)
            {
                model.Msg = ViewBag.Txt["FailMsg"];
                return View(model);
            }

            return RedirectToAction("Index", "Company");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Client.Web.Models;
using App.Client.Web.Services;
using App.Domain.Contracts;
using App.Utils;

namespace App.Client.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly ICompanyService _companyService;
        public UserController(
            IUserService userService,
            IFormsAuthenticationService formsAuthenticationService,
            ICompanyService companyService)
            : base(userService, formsAuthenticationService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult SignUp(SignupModel model)
        {
            if (!IsModelValid(model))
            {
                model.Msg = "Failed, check fields and try again";
                return View(model);
            }

            var userDto = new UserDto
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Language = model.Language,
                Password = model.Password
            };

            var userId = _userService.CreateUser(userDto);
            if (userId == null)
            {
                model.Msg = "Failed, check fields and try again";
                return View(model);
            }

            var companyDto = new CompanyDto();
            companyDto.Name = model.CompanyName;
            companyDto.Url = model.CompanyUrl;
            companyDto.AdminEmail = model.Email;
            companyDto.AdminId = userId;
            companyDto.Language = model.Language;
            companyDto.CustomFields = new List<CustomFieldDto>();

            _companyService.CreateCompany(companyDto);

            _formsAuthenticationService.SignIn(userId, true);
            return RedirectToHome();
        }

        private static bool IsModelValid(SignupModel model)
        {
            return !string.IsNullOrEmpty(model.FirstName)
                   && !string.IsNullOrEmpty(model.LastName)
                   && !string.IsNullOrEmpty(model.Language)
                   && !string.IsNullOrEmpty(model.Password)
                   && !string.IsNullOrEmpty(model.CompanyName)
                   && !string.IsNullOrEmpty(model.CompanyUrl)
                   && !string.IsNullOrEmpty(model.Email)
                   && model.Email.IsEmail();
        }
    }
}
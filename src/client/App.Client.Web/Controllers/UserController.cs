using System.Collections.Generic;
using System.Web.Mvc;

using App.Client.Web.Models;
using App.Client.Web.Services;
using App.Domain.Contracts;

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

        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToHome();
            }

            return View(new LoginModel());
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToHome();
            }

            if (!model.IsValid(model))
            {
                model.Msg = "Failed, check fields and try again";
                return View(model);
            }

            var userDto = new UserDto
            {
                Email = model.Email,
                Password = model.Password
            };

            if (_userService.Authenticate(userDto))
            {
                var user = _userService.GetUserByEmail(model.Email);
                _formsAuthenticationService.SignIn(user.Id, true);
                return RedirectToHome();
            }

            model.Msg = "Failed, check fields and try again";
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                _formsAuthenticationService.SignOut();
            }

            return RedirectToHome();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToHome();
            }

            return View(new SignupModel());
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult SignUp(SignupModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToHome();
            }

            if (!model.IsValid(model))
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
    }
}
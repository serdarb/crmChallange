using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Client.Web.Models;
using App.Client.Web.Services;

namespace App.Client.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IFormsAuthenticationService _formsAuthenticationService;
        public UserController(IFormsAuthenticationService formsAuthenticationService)
        {
            _formsAuthenticationService = formsAuthenticationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
#if DEBUG
            if (Request.UserHostAddress == "::1") { _formsAuthenticationService.SignIn("::1", true); }
#endif

            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SignUp(SignupModel model)
        {
            return View();
        }
    }
}
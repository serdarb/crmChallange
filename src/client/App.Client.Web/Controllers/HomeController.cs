using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Client.Web.Services;
using App.Domain.Contracts;

namespace App.Client.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(
            IUserService userService, 
            IFormsAuthenticationService formsAuthenticationService) 
            : base(userService, formsAuthenticationService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
	}
}
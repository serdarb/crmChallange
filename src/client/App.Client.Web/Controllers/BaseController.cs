using System.Threading;
using System.Web.Mvc;

using MongoDB.Bson;

using App.Client.Web.Services;
using App.Domain.Contracts;
using App.Utils;


namespace App.Client.Web.Controllers
{
    public class BaseController : Controller
    {
        public readonly IUserService _userService;
        public readonly IFormsAuthenticationService _formsAuthenticationService;

        public BaseController(
            IUserService userService,
            IFormsAuthenticationService formsAuthenticationService)
        {
            _userService = userService;
            _formsAuthenticationService = formsAuthenticationService;
        }

        public ActionResult RedirectToHome()
        {
            return RedirectToAction("Index", "Home");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetLanguage();

            base.OnActionExecuting(filterContext);
        }

        public void SetLanguage()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = ConstHelper.CultureEN;
                Thread.CurrentThread.CurrentUICulture = ConstHelper.CultureEN;

                ViewBag.Txt = HttpContext.Application[ConstHelper.en_txt];

                var langCookie = Request.Cookies[ConstHelper.__Lang];
                if (langCookie != null)
                {
                    var lang = langCookie.Value;
                    if (lang == ConstHelper.tr)
                    {
                        ViewBag.Txt = HttpContext.Application[ConstHelper.tr_txt];

                        Thread.CurrentThread.CurrentCulture = ConstHelper.CultureTR;
                        Thread.CurrentThread.CurrentUICulture = ConstHelper.CultureTR;
                    }
                }
                else
                {
                    if (!User.Identity.IsAuthenticated) return;
                    if (CurrentUser.Language == ConstHelper.tr)
                    {
                        ViewBag.Txt = HttpContext.Application[ConstHelper.tr_txt];

                        Thread.CurrentThread.CurrentCulture = ConstHelper.CultureTR;
                        Thread.CurrentThread.CurrentUICulture = ConstHelper.CultureTR;
                    }
                }
            }
            catch { }
        }

        private UserDto _currentUser;
        public UserDto CurrentUser
        {
            get
            {
                if (_currentUser != null) return _currentUser;

                ObjectId id;
                if (User.Identity.IsAuthenticated
                    && ObjectId.TryParse(User.Identity.Name, out id))
                {
                    _currentUser = _userService.GetUser(User.Identity.Name);
                }
                else
                {
                    _formsAuthenticationService.SignOut();
                }

                return _currentUser;
            }
        }

    }
}
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
//using WebApplication1.Models;

//namespace WebApplication1.Controllers
//{
//    public class AccountController : Controller
//    {
//        private ApplicationUserManager _userManager;

//        public AccountController()
//        {
//        }

//        public AccountController(ApplicationUserManager userManager)
//        {
//            UserManager = userManager;
//        }

//        public ApplicationUserManager UserManager
//        {
//            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
//            private set => _userManager = value;
//        }

//        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

//        // GET: /Account/Register
//        public ActionResult Register()
//        {
//            return View();
//        }

//        // POST: /Account/Register
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
//                var result = await UserManager.CreateAsync(user, model.Password);
//                if (result.Succeeded)
//                {
//                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie));
//                    return RedirectToAction("Index", "Store");
//                }
//                AddErrors(result);
//            }
//            return View(model);
//        }

//        // GET: /Account/Login
//        public ActionResult Login()
//        {
//            return View();
//        }

//        // POST: /Account/Login
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Login(LoginViewModel model)
//        {
//            if (!ModelState.IsValid) return View(model);

//            var user = await UserManager.FindAsync(model.Email, model.Password);
//            if (user != null)
//            {
//                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie));
//                return RedirectToAction("Index", "Store");
//            }
//            ModelState.AddModelError("", "Invalid login attempt.");
//            return View(model);
//        }

//        // GET: /Account/Logout
//        public ActionResult Logout()
//        {
//            AuthenticationManager.SignOut();
//            return RedirectToAction("Index", "Store");
//        }

//        private void AddErrors(IdentityResult result)
//        {
//            foreach (var error in result.Errors)
//                ModelState.AddModelError("", error);
//        }
//    }
//}

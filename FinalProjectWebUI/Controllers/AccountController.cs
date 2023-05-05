using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.Entity.Identity;
using FinalProjectWebUI.Models.FormModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FinalProjectWebUI.Controllers
{
    public class AccountController : Controller
    {
        readonly SignInManager<AppUser> signInManager;
        readonly UserManager<AppUser> userManager;
        readonly FinalDbContext db;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userInManager, FinalDbContext db)
        {
            this.signInManager = signInManager;
            this.userManager = userInManager;
            this.db = db;
        }


        [AllowAnonymous]
        public IActionResult Login(int categorystore)
        {
            ViewBag.Categorystore = categorystore;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        async public Task<IActionResult> Login(UserFormModel model)
        {
            string UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (ModelState.IsValid)
            {
                var appUser = await userManager.FindByNameAsync(model.Username);
                if (appUser == null)
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    goto showSameView;
                }
                var result = await signInManager.PasswordSignInAsync(appUser, model.Password, true, true);

                if (result.Succeeded)
                {

                    string redirect = Request.Query["returnUrl"];
                    if (string.IsNullOrWhiteSpace(redirect))
                        return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    goto showSameView;
                }
            }
        showSameView:
            return View(model);
        }

        [AllowAnonymous]
        async public Task<IActionResult> Logout(int categorystore)
        {
            ViewBag.Categorystore = categorystore;
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public IActionResult Register(RegisterFormModel model, bool register, int categorystore)
        {
            ViewBag.Categorystore = categorystore;
            if (model.Email == null || model.UserName == null || model.Password == null)
            {
                return View();
            }
            else
            {
                return View(model);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        async public Task<IActionResult> Register(RegisterFormModel model)
        {
            if (ModelState.IsValid)
            {

                Customer customer = new Customer()
                {
                   
                    Name = model.Name,
                    Surname = model.Surname,

                };
                db.Customers.Add(customer);
                db.SaveChanges();

                var appUser = new AppUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    CustomerId = customer.Id
                };
                var result = await userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {

                    return RedirectToAction(nameof(Login));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(model);
        }

        public IActionResult Accessdenied()
        {
            return RedirectToAction("Login");
        }
    }
}

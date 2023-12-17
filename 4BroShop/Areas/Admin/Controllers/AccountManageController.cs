using _4BroShop.Areas.Admin.ViewModels;
using _4BroShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace _4BroShop.Areas.Admin.Controllers
{
    public class AccountManageController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private ApplicationDbContext db = new ApplicationDbContext();
        public AccountManageController()
        {
            _dbContext = new ApplicationDbContext(); // Khởi tạo đối tượng DbContext
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_dbContext));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext));
        }

        public AccountManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _roleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var items = db.Users.ToList().ToPagedList(pageNumber, pageSize);
            return View(items);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.Phone
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, model.Role);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Người dùng không tồn tại.";
                return RedirectToAction("Index", "AccountManage", new { area = "Admin" });
            }

            var roles = _userManager.GetRoles(user.Id);
            var availableRoles = db.Roles.ToList().Select(r => new SelectListItem { Value = r.Name, Text = r.Name });

            var viewModel = new EditAccountViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Phone = user.PhoneNumber,
                Email = user.Email,
                Address = user.Address,
                Roles = availableRoles,
                SelectedRole = roles.FirstOrDefault()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = model.UserName;
                user.FullName = model.FullName;
                user.PhoneNumber = model.Phone;
                user.Email = model.Email;
                user.Address = model.Address;

                // Update user details
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // Assign the selected role to the user
                    await _userManager.RemoveFromRolesAsync(user.Id, _userManager.GetRoles(user.Id).ToArray());
                    await _userManager.AddToRoleAsync(user.Id, model.SelectedRole);

                    return RedirectToAction("Index", "Home", new { area = "Admin" }); 
                }

                AddErrors(result);
            }
            model.Roles = db.Roles.ToList().Select(r => new SelectListItem { Value = r.Name, Text = r.Name });
            return View(model);
        }
        public async Task<ActionResult> Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }
        public async Task<ActionResult> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (!await _userManager.IsInRoleAsync(userId, roleName))
            {
                await _userManager.AddToRoleAsync(userId, roleName);
            }

            return RedirectToAction("Index", "Home"); // Redirect to a suitable page after assigning the role
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}

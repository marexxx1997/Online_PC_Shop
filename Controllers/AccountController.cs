using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Static;
using OnlineShop.Data.ViewModels;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbCContext _context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbCContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Laptops");
                    }
                }
                else
                {
                    TempData["Error"] = "Pogrešna e-mail adresa ili lozinka. Molim Vas pokušajte ponovo!";

                }
                return View(loginVM);
            }
            TempData["Error"] = "Pogrešna e-mail adresa ili lozinka. Molim Vas pokušajte ponovo!";
            return View(loginVM);


        }

        public IActionResult Edit(string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            var response = new RegisterVM()
            {
                FullName = user.FullName,
                EmailAddress = user.Email
            };
            return View(response);
        }
        [HttpPost]
        public IActionResult Edit(string id, RegisterVM registerVM)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            //if (!ModelState.IsValid)
            //{
            //    return View(registerVM);
            //}
            if(user!=null)
            {
                user.FullName= registerVM.FullName;
                user.Email = registerVM.EmailAddress;
                
                _context.SaveChanges();
            }
            return RedirectToAction("Users");
        }
        public IActionResult Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if(user != null )
            {
                TempData["Error"] = "Ova e-mail adresa je zauzeta!";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };
            var newUserRespnse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (newUserRespnse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Laptops");
        }

        [HttpGet]
        public IActionResult DeleteUser(string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Users"); 
        }
    }
}

﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp1.Data;
using RunGroupWebApp1.Models;
using RunGroupWebApp1.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RunGroupWebApp1.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if(user != null)
            {
                //user is found
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //correct pass
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }
                //incorrect pass
                TempData["Error"] = "Wrong credentials. please, try again.";
                return View(loginViewModel);
            }
            //user not found
            TempData["Error"] = "Wrong credentials. please, try again.";
            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if(user != null)
            {
                TempData["Error"] = "Thiss email address is already in use";
                return View(registerViewModel);
            }
            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded) 
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return RedirectToAction("Index", "Race");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Race");
        }
    }
}

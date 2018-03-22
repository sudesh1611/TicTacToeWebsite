using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicTacToeWebsite.Data;
using TicTacToeWebsite.Models;

namespace TicTacToeWebsite.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserAccountDbContext _context;

        public LoginModel(UserAccountDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public bool KeepLoggedIn { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                UserAccount user=null;
                var Accounts = await _context.UserAccount.ToListAsync();
                foreach (var item in Accounts)
                {
                    if(item.UserName==Input.UserName.ToLower())
                    {
                        if(item.Password==Input.Password)
                        {
                            user = item;
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return Page();
                        }
                    }
                }

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.GivenName,user.UserName),
                    new Claim(ClaimTypes.Email,user.EmailID),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                };
                if (Input.KeepLoggedIn)
                {
                    authProperties.IsPersistent = true;
                    authProperties.ExpiresUtc= DateTimeOffset.UtcNow.AddMonths(1);
                }

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);


                return RedirectToPage("/Index");
            }

            // Something failed. Redisplay the form.
            return Page();
        }

    }
}
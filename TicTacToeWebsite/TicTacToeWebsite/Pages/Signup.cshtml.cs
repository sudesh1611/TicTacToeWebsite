using System;
using System.Collections.Generic;
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
    public class SignupModel : PageModel
    {
        [BindProperty]
        public UserAccount InputUser { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public string ReturnUrl { get; set; }

        private readonly UserAccountDbContext _context;

        public SignupModel(UserAccountDbContext context)
        {
            _context = context;
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
                var Accounts = await _context.UserAccount.ToListAsync();
                foreach (var item in Accounts)
                {
                    if(item.UserName==InputUser.UserName.ToLower())
                    {
                        ModelState.AddModelError(string.Empty, "Username Not Available.");
                        return Page();
                    }
                    if (item.EmailID == InputUser.EmailID.ToLower())
                    {
                        ModelState.AddModelError(string.Empty, "Email ID already registered.");
                        return Page();
                    }
                }

                UserAccount NewUser = new UserAccount
                {
                    FullName = InputUser.FullName,
                    UserName = InputUser.UserName.ToLower(),
                    EmailID = InputUser.EmailID.ToLower(),
                    Password = InputUser.Password,
                    ConfirmPassword = InputUser.ConfirmPassword,
                    SingleEasyLoses = 0,
                    SingleEasyTies = 0,
                    SingleEasyWins = 0,
                    SingleHardLoses = 0,
                    SingleHardTies = 0,
                    SingleHardWins = 0,
                    DoubleLoses = 0,
                    DoubleTies = 0,
                    DoubleWins = 0
                };

                try
                {
                    var result = await _context.UserAccount.AddAsync(NewUser);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return RedirectToPage("/Error");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, NewUser.FullName),
                    new Claim(ClaimTypes.Email,NewUser.EmailID),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                };
                authProperties.IsPersistent = true;
                authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(1);

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
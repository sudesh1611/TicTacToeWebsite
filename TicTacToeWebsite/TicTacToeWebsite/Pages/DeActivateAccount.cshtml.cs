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
using TicTacToeWebsite.Controllers;
using TicTacToeWebsite.Data;

namespace TicTacToeWebsite.Pages
{
    public class DeActivateAccountModel : PageModel
    {
        [BindProperty]
        public string Password { get; set; }

        private readonly UserAccountDbContext _userContext;

        public DeActivateAccountModel(UserAccountDbContext context)
        {
            _userContext = context;
        }

        public void OnGet()
        {

        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(string returnURL=null)
        {
            if (User.Identity.IsAuthenticated)
            {
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                var user = await _userContext.UserAccount.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                if (user != null)
                {
                    if(user.Password!=Password)
                    {
                        ModelState.AddModelError(string.Empty, "Password is Wrong!");
                        return Page();
                    }
                    else
                    {
                        _userContext.UserAccount.Remove(user);
                        await _userContext.SaveChangesAsync();
                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        return RedirectToPage("/Index");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User Already Deleted!!");
                    return Page();
                }
            }
            return Page();
        }
    }
}
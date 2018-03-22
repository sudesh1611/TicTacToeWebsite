using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeWebsite.Data;

namespace TicTacToeWebsite.Controllers
{
    public class AccountsController : ControllerBase
    {
        private readonly UserAccountDbContext _userContext;

        public class UpdateModelWeb
        {
            public string Type { get; set; }
            public string Counts { get; set; }
        }

        public AccountsController( UserAccountDbContext context)
        {
            _userContext = context;
        }

        public async Task<IActionResult> LogoutWeb()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }

        public async Task<string>  UpdateStatsWeb( UpdateModelWeb model)
        {
            if(ModelState.IsValid)
            {
                if(User.Identity.IsAuthenticated)
                {
                    string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                    var user = await _userContext.UserAccount.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                    if (user == null)
                    {
                        return "False";
                    }
                    else
                    {
                        if (model.Type == "SingleEasyWin")
                        {
                            user.SingleEasyWins = user.SingleEasyWins + long.Parse(model.Counts);
                        }
                        else if (model.Type == "SingleEasyTie")
                        {
                            user.SingleEasyTies = user.SingleEasyTies + long.Parse(model.Counts);
                        }
                        else if (model.Type == "SingleEasyLoss")
                        {
                            user.SingleEasyLoses = user.SingleEasyLoses + long.Parse(model.Counts);
                        }
                        else if (model.Type == "SingleHardWin")
                        {
                            user.SingleHardWins = user.SingleHardWins + long.Parse(model.Counts);
                        }
                        else if (model.Type == "SingleHardTie")
                        {
                            user.SingleHardTies = user.SingleHardTies + long.Parse(model.Counts);
                        }
                        else if (model.Type == "SingleHardLoss")
                        {
                            user.SingleHardLoses = user.SingleHardLoses + long.Parse(model.Counts);
                        }
                        else if (model.Type == "DoubleWin")
                        {
                            user.DoubleWins = user.DoubleWins + long.Parse(model.Counts);
                        }
                        else if (model.Type == "DoubleTie")
                        {
                            user.DoubleTies = user.DoubleTies + long.Parse(model.Counts);
                        }
                        else if (model.Type == "DoubleLoss")
                        {
                            user.DoubleLoses = user.DoubleLoses + long.Parse(model.Counts);
                        }

                        try
                        {
                            _userContext.UserAccount.Update(user);
                            await _userContext.SaveChangesAsync();
                            return "True";
                        }
                        catch (Exception ex)
                        {

                            return "False";
                        }
                    }
                }
                return "False";
            }
            return "False";
        }

    }
}
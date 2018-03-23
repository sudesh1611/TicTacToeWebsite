using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicTacToeWebsite.Data;

namespace TicTacToeWebsite.Pages
{
    public class StatisticsModel : PageModel
    {

        private readonly UserAccountDbContext _userContext;

        public class InputModel
        {
            public string UserName { get; set; }
            public string FullName { get; set; }
            public long SingleEasyWins { get; set; }
            public long SingleEasyLoses { get; set; }
            public long SingleEasyTies { get; set; }
            public long SingleHardWins { get; set; }
            public long SingleHardLoses { get; set; }
            public long SingleHardTies { get; set; }
            public long DoubleWins { get; set; }
            public long DoubleLoses { get; set; }
            public long DoubleTies { get; set; }
            public string EmailID { get; set; }
            public long TotalPlayed { get; set; }
        }

        [BindProperty]
        public InputModel inputModel { get; set; }

        public StatisticsModel(UserAccountDbContext context)
        {
            _userContext = context;
        }
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                var user = await _userContext.UserAccount.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                if (user != null)
                {
                    inputModel = new InputModel
                    {
                        UserName = user.UserName,
                        FullName = user.FullName,
                        EmailID = user.EmailID,
                        SingleEasyLoses = user.SingleEasyLoses,
                        SingleEasyTies = user.SingleEasyTies,
                        SingleEasyWins = user.SingleEasyWins,
                        SingleHardLoses = user.SingleHardLoses,
                        SingleHardTies = user.SingleHardTies,
                        SingleHardWins = user.SingleHardWins,
                        DoubleLoses = user.DoubleLoses,
                        DoubleTies = user.DoubleTies,
                        DoubleWins = user.DoubleWins,
                        TotalPlayed = user.SingleEasyLoses + user.SingleEasyTies + user.SingleEasyWins + user.SingleHardLoses + user.SingleHardTies + user.SingleHardWins + user.DoubleLoses + user.DoubleTies + user.DoubleWins
                    };
                }
                return Page();
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
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
    public class SinglePlayerEasyGameModel : PageModel
    {
        [TempData]
        public string PlayerName { get; set; }

        public class InputModel
        {
            public string CurrentPlayerName { get; set; }
            public string Won { get; set; }
            public string Lost { get; set; }
            public string Tie { get; set; }
        }

        [BindProperty]
        public InputModel inputModel { set; get; }

        private readonly UserAccountDbContext _userContext;

        public SinglePlayerEasyGameModel( UserAccountDbContext context)
        {
            _userContext = context;
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            inputModel = new InputModel();
            if (User.Identity.IsAuthenticated)
            {
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                var user = await _userContext.UserAccount.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                if (user != null)
                {
                    inputModel.CurrentPlayerName = user.UserName;
                    inputModel.Won = user.SingleEasyWins.ToString();
                    inputModel.Tie = user.SingleEasyTies.ToString();
                    inputModel.Lost = user.SingleEasyLoses.ToString();
                }
            }
            else if (String.IsNullOrEmpty(PlayerName))
            {
                return RedirectToPage("/SinglePlayerLogin");
            }
            else
            {
                inputModel.CurrentPlayerName = PlayerName;
                inputModel.Won = "0";
                inputModel.Tie = "0";
                inputModel.Lost = "0";
            }
            return Page();
        }
    }
}
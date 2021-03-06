﻿using System;
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
    public class SinglePlayerHardGameModel : PageModel
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

        public SinglePlayerHardGameModel(UserAccountDbContext context)
        {
            _userContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            inputModel = new InputModel();
            if (User.Identity.IsAuthenticated)
            {
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                var user = await _userContext.UserAccount.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                if(user!=null)
                {
                    inputModel.CurrentPlayerName = user.UserName;
                    inputModel.Won = user.SingleHardWins.ToString();
                    inputModel.Tie = user.SingleHardTies.ToString();
                    inputModel.Lost = user.SingleHardLoses.ToString();
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
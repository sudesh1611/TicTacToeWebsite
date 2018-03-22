using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TicTacToeWebsite.Pages
{
    public class SinglePlayerLoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string PlayerName { get; set; }

        public class InputModel
        {
            [Required]
            [MinLength(4, ErrorMessage = "Length should be atleast 4")]
            [MaxLength(15, ErrorMessage = "Length shouldn't exceed 15")]
            public string PlayerName { set; get; }

            public string Difficulty { set; get; }
        }

        public IActionResult OnPost(string url=null)
        {
            if(ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    PlayerName = User.Identity.Name;
                }
                else
                {
                    PlayerName = Input.PlayerName;
                }

                if (Input.Difficulty == "Easy")
                {
                    return RedirectToPage("/SinglePlayerEasyGame");
                }
                else
                {
                    return RedirectToPage("/SinglePlayerHardGame");
                }
            }
            return Page();
        }
    }
}
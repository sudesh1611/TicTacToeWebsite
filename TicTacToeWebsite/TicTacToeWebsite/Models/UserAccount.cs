using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeWebsite.Models
{
    public class UserAccount
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FullName is required")]
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

        [Required(ErrorMessage = " Email is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Entered Email Address is not valid")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = " Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

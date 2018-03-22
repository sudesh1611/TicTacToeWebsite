using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeWebsite.Data
{
    public class UserAccountDbContext: DbContext
    {
        public UserAccountDbContext(DbContextOptions<UserAccountDbContext> options):base(options)
        {

        }

        public DbSet<TicTacToeWebsite.Models.UserAccount> UserAccount { get; set; }
    }
}

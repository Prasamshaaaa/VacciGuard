using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class AuthDBContext : DbContext
    {

        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


    }
}

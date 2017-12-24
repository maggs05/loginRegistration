using Microsoft.EntityFrameworkCore;
 
namespace loginRegistration.Models
{
    public class LoginRegContext : DbContext
    {
        public LoginRegContext(DbContextOptions<LoginRegContext> options) : base(options) { }

        public DbSet<HomePageUsers>users{get;set;}
        
    }
}
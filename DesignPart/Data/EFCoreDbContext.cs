using DesignPart.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace DesignPart.Data
{
    public class EFCoreDbContext:DbContext
    {
        // connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-I1B96CP\SQLEXPRESS;Database=ButtFixx;Trusted_Connection=True;Encrypt=False");
        }

        // dbsets
        public DbSet<User> Users { get; set; } 
    }
}

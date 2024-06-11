using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace RealEstate.Models
{
    public class MyDatabase : DbContext
    {
        public DbSet<Account> Accounts { get; set; }     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=myportfolioweb;user=root;password=;SslMode=none");
        }
    }
}

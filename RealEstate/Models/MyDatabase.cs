using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using RealEstate.Models.Entities;

namespace RealEstate.Models
{
    public class MyDatabase : DbContext
    {
        public DbSet<Account> Accounts { get; set; }     

        public DbSet<Service> Services { get; set; }

        public DbSet<Photo> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=realestate;user=root;password=;SslMode=none");
        }
    }
}
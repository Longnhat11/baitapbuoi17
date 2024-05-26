using baitapbuoi17.Models;
using Microsoft.EntityFrameworkCore;

namespace baitapbuoi17.DataAccess.DBContext
{
    public class ProductDBcontext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PBSFM7Q;Initial Catalog=Baitapbuoi17;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True");
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Variants> Variants { get; set; }
    }
}

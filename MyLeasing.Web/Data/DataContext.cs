using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(
            options)
        {
        }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Lessee> Lessees { get; set; }
    }
}
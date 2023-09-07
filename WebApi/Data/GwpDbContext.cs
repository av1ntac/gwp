using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class GwpDbContext: DbContext
    {
        public GwpDbContext(DbContextOptions<GwpDbContext> options)
            : base(options)
        { }

        public DbSet<Country> Country { get; set; }
        public DbSet<LineOfBusiness> LineOfBusiness { get; set; }
        public DbSet<CountryLineOfBusinessGwp> CountryLineOfBusinessGwt { get; set; }
    }
}

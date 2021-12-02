using Microsoft.EntityFrameworkCore;
using TransnationalLanka.Rms.Mobile.Dal.Entities;

namespace TransnationalLanka.Rms.Mobile.Dal
{
    public class RmsDbContext : DbContext
    {
        public RmsDbContext(DbContextOptions<RmsDbContext> options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
    }
}

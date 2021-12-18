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
        public DbSet<LocationItem> LocationItems { get; set; }
        public DbSet<ItemStorage> ItemStorages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FieldDefinition> FieldDefinitions { get; set; }
    }
}

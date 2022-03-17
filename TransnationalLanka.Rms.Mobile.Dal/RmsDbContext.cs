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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserPassword> UserPasswords { get; set; }
        public DbSet<PickList> PickLists { get; set; }
        public DbSet<MobileDevice> MobileDevices { get; set; }
        public DbSet<RequestDetail> RequestDetails { get; set; }
        public DbSet<RequestView> RequestViews { get; set; }
        public DbSet<DocketEmptyDetail> DocketEmptyDetails { get; set; }
        public DbSet<DocketDetail> DocketDetails { get; set; }
        public DbSet<EmptyDocketPrintHeader> EmptyDocketPrintHeaders { get; set; }
        public DbSet<DocketPrintSlice> DocketPrintSlices { get; set; }
        public DbSet<ValidateCartonResult> ValidateCartonResults { get; set; }
        public DbSet<RequestHeader> RequestHeaders { get; set; }
        public DbSet<RequestSignatureImage> RequestSignatureImages { get; set; }
        public DbSet<CartonSplitResultModel> CartonSplitResultModels { get; set; }
        public DbSet<UserLogger> UserLoggers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

        modelBuilder.Entity<DocketEmptyDetail>().HasNoKey();
        modelBuilder.Entity<DocketDetail>().HasNoKey();
        modelBuilder.Entity<ValidateCartonResult>().HasNoKey();
        modelBuilder.Entity<CartonSplitResultModel>().HasNoKey();
            

        }
    }
}

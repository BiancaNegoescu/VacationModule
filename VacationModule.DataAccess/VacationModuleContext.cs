using Microsoft.EntityFrameworkCore;
using VacationModule.POCO;

namespace VacationModule.DataAccess
{
    public class VacationModuleContext: DbContext
    {
        public VacationModuleContext() { }

        public VacationModuleContext(DbContextOptions<VacationModuleContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<VacationRequest> VacationRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.VacationRequests)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }

    }
}
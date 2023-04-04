using Microsoft.EntityFrameworkCore;
using VacationModule.POCO;

namespace VacationModule.DataAccess
{
    public class VacationModuleContext: DbContext
    {
        public VacationModuleContext() { }

        public VacationModuleContext(DbContextOptions<VacationModuleContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

    }
}
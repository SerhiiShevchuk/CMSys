using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CMSys.Infrastructure
{
    internal class AppContext : DbContext
    {
        internal AppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
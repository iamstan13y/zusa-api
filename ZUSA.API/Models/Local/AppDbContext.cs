using Microsoft.EntityFrameworkCore;
using ZUSA.API.Models.Data;

namespace ZUSA.API.Models.Local
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Account>? ZAccounts { get; set; }
        public DbSet<GeneratedCode>? GeneratedCodes { get; set; }
        public DbSet<School>? Schools { get; set; }
        public DbSet<Sport>? Sports { get; set; }
        public DbSet<TeamMember>? TeamMembers { get; set; }
        public DbSet<Subscription>? Subscriptions { get; set; }
    }
}
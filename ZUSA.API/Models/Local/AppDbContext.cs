using Microsoft.EntityFrameworkCore;
using ZUSA.API.Models.Data;

namespace ZUSA.API.Models.Local
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Account>? Accounts { get; set; }
        public DbSet<OtpCode>? OtpCodes { get; set; }
        public DbSet<School>? Schools { get; set; }
        public DbSet<Sport>? Sports { get; set; }
        public DbSet<TeamMember>? TeamMembers { get; set; }
        public DbSet<Subscription>? Subscriptions { get; set; }
    }
}
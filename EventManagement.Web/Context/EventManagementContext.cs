using EventManagement.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Web.Context
{
    public class EventManagementContext : IdentityDbContext<ApplicationUser>
    {
        public EventManagementContext(DbContextOptions<EventManagementContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Media> MediaFiles { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Fakebook.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Fakebook.Infrastructure.Identity
{
    public class FakebookIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public FakebookIdentityContext(DbContextOptions<FakebookIdentityContext> options)
            : base(options)
        {
			Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
			
            base.OnModelCreating(builder);
			builder.Entity<ApplicationUser>().ToTable("IdentityUsers");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

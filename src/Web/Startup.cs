using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Data;
using Fakebook.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Fakebook.Web
{
    public class Startup
    {
		public Startup(IConfiguration configuration) 
		{
			Configuration = configuration;
		}

		private readonly IConfiguration Configuration;

		public void ConfigureDevelopmentServices(IServiceCollection services)
		{
			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			ConfigureServices(services);
		}

		public void ConfigureProductionServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			ConfigureServices(services);
		}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddDbContext<FakebookContext>(options => 
				options.UseSqlServer(
					Configuration.GetConnectionString("FakebookConnection")));

			services.AddDbContext<FakebookIdentityContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("IdentityConnection")));


			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.SignIn.RequireConfirmedAccount = true;
			})
			.AddDefaultUI()
			.AddEntityFrameworkStores<FakebookIdentityContext>()
			.AddDefaultTokenProviders();

			// services.AddDefaultIdentity<ApplicationUser>(options =>
			// {
			// 	options.User.RequireUniqueEmail = true;
			// 	options.SignIn.RequireConfirmedAccount = true;
			// })
			// .AddEntityFrameworkStores<FakebookIdentityContext>()
			// .AddDefaultTokenProviders();

			services.AddRazorPages();
			services.AddScoped<IPostRepository, PostRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
				app.UseStatusCodePages();
				app.UseStaticFiles();
            }

            app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
				endpoints.MapRazorPages();
            });
        }
    }
}

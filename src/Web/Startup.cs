using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Fakebook.Core.Interfaces;
using Fakebook.Core.Services;
using Fakebook.Core.Entities;
using Fakebook.Infrastructure.Data;
using Fakebook.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

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
			services.AddControllersWithViews()
				.AddRazorRuntimeCompilation()
				.AddNewtonsoftJson(o =>
					o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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

			services.AddRazorPages();
			
			services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
			services.AddScoped<IPostRepository, PostRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IFriendshipRepository, FriendshipRepository>();
			services.AddScoped<INotificationsRepository, NotificiationsRepository>();

			services.AddScoped<IPostService, PostService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IFriendsService, FriendsService>();
			services.AddScoped<INotificationsService, NotificationsService>();
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
				endpoints.MapControllerRoute(
					"profileRoute",
					"{controller=Profile}/{action=Index}/{userPublicId}"
				);
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Fakebook.Core.Interfaces;
using Fakebook.Core.Services;
using Fakebook.Infrastructure.Data;
using Fakebook.Infrastructure.Identity;
using Fakebook.Web.Areas.Messenger;

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

			services.AddDbContext<FakebookContext>(options =>
				options.UseInMemoryDatabase("Fakebook"));

			services.AddDbContext<FakebookIdentityContext>(options =>
				options.UseInMemoryDatabase("Identity"));

			ConfigureServices(services);
		}

		public void ConfigureProductionServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			services.AddDbContext<FakebookContext>(options => 
				options.UseSqlServer(
					Configuration.GetConnectionString("FakebookConnection")));

			services.AddDbContext<FakebookIdentityContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("IdentityConnection")));

			ConfigureServices(services);
		}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.SignIn.RequireConfirmedAccount = false;
			})
				.AddDefaultUI()
				.AddEntityFrameworkStores<FakebookIdentityContext>()
				.AddDefaultTokenProviders();
				
			services.AddAuthentication().AddFacebook(options =>
			{
				options.AppId = Configuration["Authentication:Facebook:AppId"];
				options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
			});
			
			services.AddRazorPages();
			services.AddSignalR();
			services.AddAutoMapper(typeof(Startup).Assembly);

			services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
			services.AddScoped<IPostRepository, PostRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IFriendshipRepository, FriendshipRepository>();
			services.AddScoped<ICommentRepository, CommentRepository>();

			services.AddScoped<IPostService, PostService>();
			services.AddScoped<ICommentService, CommentService>();
			services.AddScoped(typeof(ILikeService<>), typeof(LikeService<>));
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IFriendsService, FriendsService>();
			services.AddScoped<IPhotoService, PhotoService>();
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
					"{controller=Profile}/{action=Index}/{userPublicId?}"
				);
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
				endpoints.MapHub<MessengerHub>("/Messenger");
            });
        }
    }
}

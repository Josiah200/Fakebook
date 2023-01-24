using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Fakebook.Core.Interfaces;
using Fakebook.Core.Services;
using Fakebook.Infrastructure.Data;
using Fakebook.Infrastructure.Identity;
using Fakebook.Web.Areas.Messenger;
using System;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Data.Sqlite;
namespace Fakebook.Web
{
	public class Startup
	{

		private readonly IConfiguration Configuration;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

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
			services.AddDbContext<FakebookContext>(options =>
				options.UseSqlite(Configuration.GetConnectionString("FakebookConnection")));

			services.AddDbContext<FakebookIdentityContext>(options =>
				options.UseSqlite(Configuration.GetConnectionString("IdentityConnection")));

			services.AddControllersWithViews()
				.AddNewtonsoftJson(o =>
					o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

			services.AddAuthentication()
				.AddFacebook(options =>
				{
					options.AppId = Configuration["ASPNETCORE_FACEBOOK_APP_ID"];
					options.AppSecret = Configuration["ASPNETCORE_FACEBOOK_APP_SECRET"];
				})
				.AddGoogle(options =>
				{
					options.ClientId = Configuration["ASPNETCORE_GOOGLE_ID"];
					options.ClientSecret = Configuration["ASPNETCORE_GOOGLE_SECRET"];
				});

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

			services.AddCors(options =>
			{
				options.AddDefaultPolicy(
					builder =>
					{
						builder.WithOrigins("http://localhost:5000", "http://localhost:5001",
								 "https://localhost:5001", "https://localhost:5000")
						.AllowAnyHeader()
						.WithMethods("GET", "POST")
						.AllowCredentials();
					});
			});

			services.AddRazorPages();
			services.AddSignalR(options => options.EnableDetailedErrors = true);
			services.AddAutoMapper(typeof(Startup).Assembly);

			services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
			services.AddScoped<IPostRepository, PostRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IFriendshipRepository, FriendshipRepository>();

			services.AddScoped<IPostService, PostService>();
			services.AddScoped<ICommentService, CommentService>();
			services.AddScoped(typeof(ILikeService<>), typeof(LikeService<>));
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IFriendsService, FriendsService>();
			services.AddScoped<IPhotoService, PhotoService>();
			services.AddScoped<IMessengerService, MessengerService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{

			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseStatusCodePages();
			}
			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCors();
			app.UseStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					"profileRoute",
					"{controller=Profile}/{action=Index}/{userPublicId?}"
				);
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
				endpoints.MapHub<MessengerHub>("/messenger");
			});
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Fakebook.Core.Entities;
using Fakebook.Infrastructure.Data;
using Fakebook.Core.Interfaces;

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
			services.AddDbContext<FakebookContext>(o => {
				o.UseSqlServer(
					Configuration.GetConnectionString("FakebookConnection"));
			});
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

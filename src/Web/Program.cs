using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fakebook.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var fakebookContext = services.GetRequiredService<FakebookContext>();
					await ContextSeed.SeedAllAsync(fakebookContext);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message + "\nSeeding failed.");
				}
			}
			host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
		{
         	var host = Host.CreateDefaultBuilder(args)
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.AddConsole();
				})
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
			host.ConfigureAppConfiguration((config) => config.AddEnvironmentVariables(prefix: "ASPNETCORE_"));
			return host;
		}
    }
}

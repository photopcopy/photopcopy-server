using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace photopcopy_server
{
	public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-6.0#webroota
                    webBuilder.UseWebRoot("public");
                    webBuilder.UseStartup<Startup>();
                });
    }
}

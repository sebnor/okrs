using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.ApplicationInsights.AspNetCore;

namespace OKRs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(true)
                .UseApplicationInsights()
#if DEBUG
                .UseUrls("http://*:5000")
#endif
                .UseStartup<Startup>();
    }
}

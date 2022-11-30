using Microsoft.AspNetCore;

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
#if DEBUG
                .UseUrls("https://*:5000")
#endif
                .UseStartup<Startup>();
    }
}

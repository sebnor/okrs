using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace OKRs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(true)
                .UseApplicationInsights()
#if DEBUG
                .UseUrls("http://*:5000")
#endif
                .UseStartup<Startup>()
                .Build();
    }
}

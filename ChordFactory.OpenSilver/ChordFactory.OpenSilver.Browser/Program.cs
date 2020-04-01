using DotNetForHtml5;
using Microsoft.AspNetCore.Blazor.Hosting;
using System.Windows;

namespace ChordFactory.OpenSilver.Browser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Cshtml5Initializer.Initialize();
            IWebAssemblyHost host = CreateHostBuilder(args).Build();
            host.Run();
            host.Dispose();
            Application.RunApplication(() =>
            {
                var app = new ChordFactory.OpenSilver.App();
            });
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();
    }
}

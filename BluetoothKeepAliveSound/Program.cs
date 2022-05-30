using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BluetoothKeepAliveSound
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHostBuilder host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => { services.AddHostedService<Worker>(); }).UseWindowsService();

            host.Build().Run();
        }
    }
}
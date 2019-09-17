using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System.Net;
using Actor.Demo.Grains;
using System.Threading.Tasks;

namespace Actor.Demo.SiloHost
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            return new HostBuilder()
                          .UseOrleans(builder =>
                          {
                              builder
                                  .UseLocalhostClustering()
                                  .Configure<ClusterOptions>(options =>
                                  {
                                      options.ClusterId = "dev";
                                      options.ServiceId = "Actor.Demo";
                                  })
                                  .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                                  .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(InventoryService).Assembly).WithReferences());
                          })
                          .ConfigureServices(services =>
                          {
                              services.Configure<ConsoleLifetimeOptions>(options =>
                              {
                                  options.SuppressStatusMessages = true;
                              });
                          })
                          .ConfigureLogging(builder =>
                          {
                              builder.AddConsole();
                          })
                          .RunConsoleAsync();
        }
    }
}

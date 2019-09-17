using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gRPC.Demo.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            const string host = "localhost";
            const int port = 9007;
            var channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            var client = new GRPCDemo.gRPC.gRPCClient(channel);
            var taskList = new List<Task>();
            for (var i = 0; i < 20; i++)
            {
                taskList.Add(Task.Run(async () =>
              {
                  var response = await client.DeductionAsync(new GRPCDemo.DeductionRequest()
                  {
                      Auantity = 5
                  });
                  Console.WriteLine("\n{0}", response.TotalInventory);
              }));
            }
            Task.WaitAll(taskList.ToArray());
            var result = client.GetInventoryQuantity(new GRPCDemo.GetInventoryQuantityRequest());
            Console.WriteLine("库存最终数量\n{0}", result.TotalInventory);
            Console.WriteLine("任意键退出...");
            Console.ReadKey();
        }
    }
}

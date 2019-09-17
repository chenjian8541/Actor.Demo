using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Actor.Demo.Interfaces;
using Microsoft.Extensions.Hosting;
using Orleans;

namespace Actor.Demo.Client
{
    public class InventoryClientHostedService : IHostedService
    {
        private readonly IClusterClient _client;

        public InventoryClientHostedService(IClusterClient client, IApplicationLifetime lifetime)
        {
            _client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var inventoryServiceClent1 = _client.GetGrain<IInventoryService>(0);
            var taskList = new List<Task>();
            for (var i = 0; i < 20; i++)
            {
                taskList.Add(Task.Run(async () =>
                 {
                     var response = await inventoryServiceClent1.Deduction(5);
                     Console.WriteLine("\n{0}", response);
                 }));
            }
            Task.WaitAll(taskList.ToArray());
            var result = await inventoryServiceClent1.GetInventoryQuantity();
            Console.WriteLine("库存最终数量\n{0}", result);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

using System;
using gRPC.Demo.Server.Impl;
using Grpc;
using Grpc.Core;
using GRPCDemo;

namespace gRPC.Demo.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            const string host = "localhost";
            const int port = 9007;
            var server = new Grpc.Core.Server
            {
                Services = { GRPCDemo.gRPC.BindService(new InventoryServiceImpl()) },
                Ports = { new ServerPort(host, port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine($"gRPC server listening on port {port}");
            Console.WriteLine("任意键退出...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using GRPCDemo;

namespace gRPC.Demo.Server.Impl
{
    /// <summary>
    /// 库存服务
    /// </summary>
    public class InventoryServiceImpl : GRPCDemo.gRPC.gRPCBase
    {
        /// <summary>
        /// 总库存数量
        /// </summary>
        private static int _totalInventory = 100;

        /// <summary>
        /// 扣减库存
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<DeductionReply> Deduction(DeductionRequest request, ServerCallContext context)
        {
            System.Threading.Thread.Sleep(new Random().Next(1, 5) * 1000);
            _totalInventory = _totalInventory - request.Auantity;
            return Task.FromResult(new DeductionReply()
            {
                TotalInventory = _totalInventory
            });
        }

        /// <summary>
        /// 获取库存数量
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<GetInventoryQuantityReply> GetInventoryQuantity(GetInventoryQuantityRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetInventoryQuantityReply()
            {
                TotalInventory = _totalInventory
            });
        }
    }
}

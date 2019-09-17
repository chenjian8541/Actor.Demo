using Actor.Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Actor.Demo.Grains
{
    /// <summary>
    /// 库存服务
    /// </summary>
    public class InventoryService : Orleans.Grain, IInventoryService
    {
        /// <summary>
        /// 总库存数量
        /// </summary>
        private static int _totalInventory = 100;

        /// <summary>
        /// 扣减库存
        /// </summary>
        /// <param name="auantity"></param>
        /// <returns></returns>
        public Task<int> Deduction(int auantity)
        {
            System.Threading.Thread.Sleep(new Random().Next(1, 5) * 1000);
            _totalInventory = _totalInventory - auantity;
            return Task.FromResult(_totalInventory);
        }

        /// <summary>
        /// 获取库存数量
        /// </summary>
        /// <returns></returns>
        public Task<int> GetInventoryQuantity()
        {
            return Task.FromResult(_totalInventory);
        }
    }
}

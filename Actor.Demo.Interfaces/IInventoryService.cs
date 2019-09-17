using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Actor.Demo.Interfaces
{
    /// <summary>
    /// 库存服务
    /// </summary>
    public interface IInventoryService : Orleans.IGrainWithIntegerKey
    {
        /// <summary>
        /// 扣减库存
        /// </summary>
        /// <param name="auantity">扣减的数量</param>
        /// <returns></returns>
        Task<int> Deduction(int auantity);

        /// <summary>
        /// 获取库存数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetInventoryQuantity();
    }
}

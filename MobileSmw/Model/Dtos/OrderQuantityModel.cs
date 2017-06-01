using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dtos
{
    /// <summary>
    /// 订单数量模型
    /// </summary>
    public class OrderQuantityModel
    {
        /// <summary>
        /// 设置或获取待付款订单数量
        /// </summary>
        public int NotPayQuantity { set; get; } = 0;

        /// <summary>
        /// 设置或获取已付款订单
        /// </summary>
        public int HasPayedQuantity { set; get; } = 0;
    }
}

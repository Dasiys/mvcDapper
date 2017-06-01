using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dtos;
using Model.MetadataModel;

namespace IBLL
{
    public interface IOrderService:IBaseService<TB_Order>
    {
        /// <summary>
        /// 设置或获取订单列表
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        IList<OrderListModel> GetOrders(OrderStatus status);

        /// <summary>
        /// 设置或获取订单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderDetailModel GetOrderDetail(string orderId);
    }
}

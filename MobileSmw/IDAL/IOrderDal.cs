using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IOC;
using Model.MetadataModel;
using Model.ViewModel;

namespace IDAL
{
    public interface IOrderDal:IBaseDal<TB_Order>,IDependency
    {

        /// <summary>
        /// 设置或获取订单列表
        /// </summary>
        /// <param name="status"></param>
        /// <param name="buyUid"></param>
        /// <returns></returns>
        IList<OrderListModel> GetOrders(OrderStatus status, int buyUid);

        /// <summary>
        /// 设置或获取订单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderDetailModel GetOrderDetail(string orderId);

        /// <summary>
        /// 获得订单数量
        /// </summary>
        /// <param name="buyUid"></param>
        /// <returns></returns>
       OrderQuantityModel GetOrderQuantity(int buyUid);
    }
}

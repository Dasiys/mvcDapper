using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.NLog;
using Dapper;
using IBLL;
using IDAL;
using Model.MetadataModel;
using Model.ViewModel;

namespace BLL
{
    /// <summary>
    /// 订单服务
    /// </summary>
    public class OrderService:BaseService<TB_Order>,IOrderService
    {
        private readonly IOrderDal _orderDal;
        public OrderService(IOrderDal orderDal, ILogFactory logFactory) : base(orderDal, logFactory)
        {
            _orderDal = orderDal;
        }

        /// <summary>
        /// 设置或获取订单列表
        /// </summary>
        /// <param name="status"></param>
        /// <param name="buyUid"></param>
        /// <returns></returns>
        public IList<OrderListModel> GetOrders(OrderStatus status,int buyUid)
        {
            return _orderDal.GetOrders(status, buyUid);
        }

        /// <summary>
        /// 设置或获取订单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderDetailModel GetOrderDetail(string orderId)
        {
            var result = _orderDal.GetOrderDetail(orderId);
            if (result == null)
            {
                _LogFactory.Error($"{GetType().Name}:{new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name}","获取订单详情失败",new {OrderId=orderId});
                Function.ExceptionThrow("Error", "获取订单详情失败，请重试");
            }
            return result;
        }

        /// <summary>
        /// 获得订单数量
        /// </summary>
        /// <param name="buyUid"></param>
        /// <returns></returns>
        public OrderQuantityModel GetOrderQuantity(int buyUid)
        {
            return _orderDal.GetOrderQuantity(buyUid);
        }
    }
}

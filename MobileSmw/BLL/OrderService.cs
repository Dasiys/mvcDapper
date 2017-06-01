using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NLog;
using Dapper;
using IBLL;
using IDAL;
using Model.Dtos;
using Model.MetadataModel;

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
        /// <returns></returns>
        public IList<OrderListModel> GetOrders(OrderStatus status)
        {
            var param=new DynamicParameters();
            var condition = "";
            if (status != OrderStatus.All)
            {
                condition = "Status=@Status";
                param.Add("Status", status);
            }
            var fields =
                "top 1 OrderID,ProductName,Count,Unit,Price,Status,Img,[Size] as Size,Money";
           return  _orderDal.GetModels<OrderListModel>("View_TB_Order_OrderDetail", condition, param, fields);
        }

        /// <summary>
        /// 设置或获取订单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderDetailModel GetOrderDetail(string orderId)
        {
            string tableName = "View_TB_Order_OrderDetail inner join TB_UserInfo on SellUid=Uid";
            string field = "CompanyName as SellCompany,View_TB_Order_OrderDetail.InputTime,StoreAddr,Img,[Size] as Size,Money,Price,Count,Unit,ProductName,Status,OrderId";
            string condition = "orderId=@orderId";
            var param=new DynamicParameters();
            param.Add("orderId",orderId);
            return _orderDal.GetSingleModel<OrderDetailModel>(tableName, condition, param, field);
        }
    }
}

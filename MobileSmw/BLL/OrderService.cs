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
        /// <param name="buyUid"></param>
        /// <returns></returns>
        public IList<OrderListModel> GetOrders(OrderStatus status,int buyUid)
        {
            var param=new DynamicParameters();
            var condition = "BuyUid=@BuyUid";
            param.Add("BuyUid",buyUid);
            if (status != OrderStatus.All)
            {
                condition = " and Status=@Status";
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
            var result = _orderDal.GetSingleModel<OrderDetailModel>(tableName, condition, param, field);
            if (result == null)
            {
                _LogFactory.Error($"{GetType().Name}:{new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name}","获取订单详情失败",new {OrderId=orderId});
                ExceptionThrow("Error", "获取订单详情失败，请重试");
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
            var tableName = "TB_order";
            var sql = $"select count(1) as NotPayQuantity  from {tableName} where BuyUid=@BuyUid and Status=1 " +
                      $"select count(1) as HasPayedQuantity from {tableName} where BuyUid=@BuyUid and Status in (2,3,4)";
            var param = new DynamicParameters();
            param.Add("BuyUid", buyUid);
            Func<SqlMapper.GridReader, OrderQuantityModel> func = multi =>
            {
                if (!multi.IsConsumed)
                {
                    var result1 = multi.Read();
                    var result2 = multi.Read();
                    return new OrderQuantityModel
                    {
                        NotPayQuantity = Convert.ToInt32(result1.FirstOrDefault()?.NotPayQuantity),
                        HasPayedQuantity = Convert.ToInt32(result2.FirstOrDefault()?.HasPayedQuantity)
                    };
                }
                return new OrderQuantityModel();
            };
            return _orderDal.MultipleQuery(sql, param, func);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Common.UnitOfWork;
using Dapper;
using IDAL;
using Model.Dtos;
using Model.MetadataModel;

namespace DAL
{
    public class OrderDal:BaseDal<TB_Order>,IOrderDal
    {
        public OrderDal(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        /// <summary>
        /// Multiple Query
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private TM MultipleQuery<TM>(string sql, DynamicParameters param, Func<SqlMapper.GridReader, TM> func)
        {
            using (var conn=UnitOfWork.GetDbConnection())
            {
                using (var multi=conn.QueryMultiple(sql,param))
                {
                  return  func(multi);
                }
            }
        }

        /// <summary>
        /// 设置或获取订单列表
        /// </summary>
        /// <param name="status"></param>
        /// <param name="buyUid"></param>
        /// <returns></returns>
        public IList<OrderListModel> GetOrders(OrderStatus status, int buyUid)
        {
            var param = new DynamicParameters();
            var condition = "BuyUid=@BuyUid";
            param.Add("BuyUid", buyUid);
            if (status != OrderStatus.All)
            {
                condition = " and Status=@Status";
                param.Add("Status", status);
            }
            var fields =
                "top 1 OrderID,ProductName,Count,Unit,Price,Status,Img,[Size] as Size,Money";
            return GetModels<OrderListModel>("View_TB_Order_OrderDetail", condition, param, fields);
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
            var param = new DynamicParameters();
            param.Add("orderId", orderId);
            return GetSingleModel<OrderDetailModel>(tableName, condition, param, field);
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
            return MultipleQuery(sql, param, func);
        }
    }
}

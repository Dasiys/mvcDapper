using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MetadataModel
{
    /// <summary>
    /// 订单详情视图
    /// </summary>
    public class View_TB_Order_OrderDetail
    {
        public string OrderID { get; set; }
        public int ID { get; set; }
        public int BuyUid { get; set; }
        public int SellUid { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? ProductCate { get; set; }
        public int? Count { get; set; }
        public string Unit { get; set; }
        public decimal? Price { get; set; }
        public decimal? Money { get; set; }
        public string AdminID { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public OrderType OrderType { get; set; }
        /// <summary>
        /// 是否开票
        /// </summary>
        public bool IsKP { get; set; }
        public string Note { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus Status { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public System.DateTime InputTime { get; set; }
        public string PID { get; set; }
        public string CID { get; set; }
        public string Img { get; set; }
        public string StoreAddr { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public string Grade { get; set; }
        public string Area { get; set; }
        public string Attribute { get; set; }
        public string Water { get; set; }
        public string BoxWay { get; set; }
    }
}

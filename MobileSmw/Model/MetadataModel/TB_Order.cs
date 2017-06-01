using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MetadataModel
{
    /// <summary>
    /// 订单
    /// </summary>
    public class TB_Order:IEntityBase
    {
        public int Id { get; set; }
        public string OrderID { get; set; }
        public int BuyUid { get; set; }
        public int SellUid { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int? ProductCate { get; set; }
        public int? Count { get; set; }
        public string Unit { get; set; }
        public decimal? Price { get; set; }
        public decimal? Money { get; set; }
        public string AdminID { get; set; }
        public OrderType OrderType { get; set; }
        public bool IsKP { get; set; }
        public string Note { get; set; }
        public byte Status { get; set; }
        public System.DateTime InputTime { get; set; }
    }
}

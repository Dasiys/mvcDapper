using Model.MetadataModel;

namespace Model.ViewModel
{
    public class OrderDetailModel
    {
        /// <summary>
        /// 设置或获取订单Id
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 设置或获取订单状态
        /// </summary>
        public OrderStatus Status { set; get; }

        /// <summary>
        /// 设置或获取产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 设置或获取单位
        /// </summary>
        public string Unit { set; get; }

        /// <summary>
        /// 设置或获取数量
        /// </summary>
        public int Count { set; get; }

        /// <summary>
        /// 设置或获取单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 设置或获取总价
        /// </summary>
        public decimal Money { set; get; }

        /// <summary>
        /// 设置或获取规格
        /// </summary>
        public string Size { set; get; }

        /// <summary>
        /// 设置或获取图片
        /// </summary>
        public string Img { set; get; }

        /// <summary>
        /// 设置或获取订单状态描述
        /// </summary>
        public string OrderStatusDes
        {
            get
            {
                switch (Status)
                {
                    case OrderStatus.Complete:
                        return "已完成";
                    case OrderStatus.Inspection:
                        return "验货中";
                    case OrderStatus.Invalid:
                        return "作废";
                    case OrderStatus.NotPay:
                        return "待付款";
                    case OrderStatus.WaitToPickUp:
                        return "待提货";
                    default:
                        return "未知";
                }
            }
        }

        /// <summary>
        /// 设置或获取仓储地址
        /// </summary>
        public string StoreAddr { set; get; }

        /// <summary>
        /// 设置或获取下单时间
        /// </summary>
        public string InputTime { set; get; }
        /// <summary>
        /// 设置或获取卖方公司
        /// </summary>
        public string SellCompany { set; get; }
    }
}

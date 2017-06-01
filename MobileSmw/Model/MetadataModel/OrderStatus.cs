using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MetadataModel
{
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 全部
        /// </summary>
        All=-1,
        /// <summary>
        /// 作废
        /// </summary>
        Invalid=0,
        /// <summary>
        /// 待付款
        /// </summary>
        NotPay=1,
        /// <summary>
        /// 待提货
        /// </summary>
        WaitToPickUp=2,
        /// <summary>
        /// 验货中
        /// </summary>
        Inspection=3,
        /// <summary>
        /// 完成
        /// </summary>
        Complete=4
    }
}

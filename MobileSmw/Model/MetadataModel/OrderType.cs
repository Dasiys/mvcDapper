using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MetadataModel
{
    /// <summary>
    /// 订单类型
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 撮合线上交易
        /// </summary>
        Online=1,
        /// <summary>
        /// 撮合线下交易
        /// </summary>
        Offline=2
    }
}

using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MetadataModel
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class TB_UserInfo:IEntityBase
    {
        /// <summary>
        /// 设置或获取Id
        /// </summary>
        [Column(Name = "Uid")]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Tel { get; set; }
        public string Photo { get; set; }
        public string Addr { get; set; }
        public string QQ { get; set; }
        public string PID { get; set; }
        public string CID { get; set; }
        public string AID { get; set; }
        public string BusinessRegister { get; set; }
        public string CardID { get; set; }
        /// <summary>
        /// 注册类型
        /// </summary>
        public RegType RegType { get; set; }
        public bool IsCertify { get; set; }
        public bool IsCheck { get; set; }
        public System.DateTime InputTime { get; set; }
        public DateTime? TrunTime { get; set; }
        public AllocationType Allocation { get; set; }
        public string AdminID { get; set; }
        public string BrandName { get; set; }
        public byte Rec { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsBrand { get; set; }
        public bool IsShop { get; set; }
        public string MainProduct { get; set; }
        public bool IsQuotation { get; set; }
        public string Maturity { get; set; }
        public string Purchase { get; set; }
        public string MonthVolume { get; set; }
        public string Note { get; set; }
        public decimal AccountMoney { get; set; }
    }
}

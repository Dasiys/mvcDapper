using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MetadataModel
{
    /// <summary>
    /// 信用购
    /// </summary>
    public class TB_XiMuCreditApply:IEntityBase
    {
        /// <summary>
        /// 设置或获取序号
        /// </summary>
        [Column(Name= "XiMuCreditApplyID")]
        public int Id { get; set; }
        /// <summary>
        /// 设置或获取会员编号
        /// </summary>
        public int Uid { get; set; }
        /// <summary>
        /// 设置或获取联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 设置或获取电话号码
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 设置或获取公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 设置或获取采购商编号 
        /// </summary>
        public int? BuyUserRecordID { get; set; }
        /// <summary>
        /// 设置或获取身份证号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 设置或获取添加时间
        /// </summary>
        public System.DateTime InputTime { get; set; }=DateTime.Now;
        /// <summary>
        /// 设置或获取备注
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 设置或获取处理状态 0 未处理  1 已处理
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 设置或获取照片
        /// </summary>
        public string Photo { get; set; }


    }
}

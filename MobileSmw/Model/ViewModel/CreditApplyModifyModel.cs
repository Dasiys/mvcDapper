using System.ComponentModel.DataAnnotations;

namespace Model.ViewModel
{
    public class CreditApplyModifyModel
    {
        /// <summary>
        /// 设置或获取会员编号
        /// </summary>
        public int Uid { get; set; }
        /// <summary>
        /// 设置或获取姓名
        /// </summary>
        [Required(AllowEmptyStrings=false,ErrorMessage = "请填写姓名")]
        public string Contact { get; set; }
        /// <summary>
        /// 设置或获取电话号码
        /// </summary>
        [Required(AllowEmptyStrings = false,ErrorMessage = "请填写手机号码")]
        public string Tel { get; set; }
        /// <summary>
        /// 设置或获取公司名称
        /// </summary>
        [Required(AllowEmptyStrings =false,ErrorMessage = "请填写公司名称")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 设置或获取身份证
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 设置或获取备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 设置或获取照片
        /// </summary>
        public string Photo { get; set; }
    }
}

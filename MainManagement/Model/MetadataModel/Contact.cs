using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Model
{
    public class Contact
    {
        public Guid ContactID{ set; get; }
        public int ID { get; set; }




        ///<summary>
        /// 名称
        ///</summary>
        [Display(Name = "电话")]
        [Required(ErrorMessage = "必填项")]
        [StringLength(11, ErrorMessage = "长度不能超过11位")]
        public string Tel { get; set; }

        ///<summary>
        /// 姓名
        ///</summary>
        [Display(Name = "联系人")]
        [Required(ErrorMessage = "必填项")]
        [StringLength(5,ErrorMessage ="长度不能超过5个汉字")]
        public string ContactName { get; set; }
    }
}

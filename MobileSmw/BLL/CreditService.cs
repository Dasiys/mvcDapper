using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.NLog;
using IBLL;
using IDAL;
using Model.MetadataModel;
using AutoMapper;
using Common;
using Model.ViewModel;

namespace BLL
{
    /// <summary>
    /// 信用购
    /// </summary>
    public class CreditService:BaseService<TB_XiMuCreditApply>, ICreditService
    {
        private readonly ICreditApplyDal _creditApplyDal;
        public CreditService(ICreditApplyDal creditApplyDal, ILogFactory logFactory) : base(creditApplyDal, logFactory)
        {
            _creditApplyDal = creditApplyDal;
        }

        /// <summary>
        /// 插入搜木金融申请数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertCreditApply(CreditApplyModifyModel model)
        {
            Function.EntityFilter(model);
            var entity = Mapper.Map<CreditApplyModifyModel, TB_XiMuCreditApply>(model);
            var result = base.Insert(entity);
            if (result < 1)
            {
                _LogFactory.Error($"{GetType().Name}:{new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name}","信用购申请失败",model);
                Function.ExceptionThrow("Error", "添加失败，请重试");
            }
            return result;
        }

        /// <summary>
        /// 检查数据
        /// </summary>
        /// <param name="entity"></param>
        public override void Validate(TB_XiMuCreditApply entity)
        {
            if (!string.IsNullOrEmpty(entity.CardID))
            {
                string pattern = @"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$";
                if (!Regex.IsMatch(entity.CardID, pattern, RegexOptions.IgnoreCase))
                {
                   Function.ExceptionThrow("CardIdError","请输入有效证件号");
                }
            }
            base.Validate(entity);
        }
    }
}

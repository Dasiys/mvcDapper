using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC;
using Model.Dtos;
using Model.MetadataModel;

namespace IBLL
{
    public interface ICreditService: IBaseService<TB_XiMuCreditApply>,IDependency
    {
        /// <summary>
        /// 添加搜木金融记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int InsertCreditApply(CreditApplyModifyModel model);
    }

}

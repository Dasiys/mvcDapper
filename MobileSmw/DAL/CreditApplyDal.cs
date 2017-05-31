using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.UnitOfWork;
using IDAL;
using Model.MetadataModel;

namespace DAL
{
    public class CreditApplyDal:BaseDal<TB_XiMuCreditApply>,ICreditApplyDal
    {
        public CreditApplyDal(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC;
using Model.MetadataModel;

namespace IDAL
{
    public interface ICreditApplyDal:IBaseDal<TB_XiMuCreditApply>,IDependency
    {
    }
}

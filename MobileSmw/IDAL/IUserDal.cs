using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.MetadataModel;
using IOC;

namespace IDAL
{
    public interface IUserDal:IBaseDal<TB_UserInfo>,IDependency
    {
    }
}

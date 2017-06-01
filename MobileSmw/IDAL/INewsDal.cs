using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC;
using Model.MetadataModel;

namespace IDAL
{
    public interface INewsDal:IBaseDal<TB_News>, IDependency
    {
    }
}

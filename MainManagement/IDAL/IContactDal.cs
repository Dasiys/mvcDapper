using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Ioc;

namespace IDAL
{
    public interface IContactDal:IBaseDal<Contact>,IDependency
    {

    }
}

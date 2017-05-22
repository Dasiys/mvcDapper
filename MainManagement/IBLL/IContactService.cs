using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Ioc;

namespace IBLL
{
    public interface IContactService:IBaseService<Contact>,IDependency
    {
        void BeginTransactionInsert(Contact t);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Ioc;


namespace Common.UnitOfWork
{
    public interface IUnitOfWork:IDependency
    {
        void ExcuteTransaction(Action action);

        IDbConnection GetConnection();
    }
}

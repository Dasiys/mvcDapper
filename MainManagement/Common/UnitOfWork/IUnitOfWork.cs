using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Ioc;


namespace Common.UnitOfWork
{
    public interface IUnitOfWork:IDisposable,IDependency
    {
        IDbConnection Connection { get; }

        //IDbTransaction Transaction { get; }

        //void Commit();

        //void Rollback();

        //void BeginTransaction();

        void UseTransaction(Action action);
    }
}

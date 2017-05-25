using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using IOC;

namespace Common.UnitOfWork
{
    public interface IUnitOfWork: IDependency
    {
        IDbConnection GetDbConnection();

        IDbTransaction BeginTransaction(IDbConnection conn);

        void EndTransactionRollback(IDbTransaction transaction);

        void EndTransactionCommit(IDbTransaction transaction);

        void ExcuteTransactionScope(Action action);
    }
}

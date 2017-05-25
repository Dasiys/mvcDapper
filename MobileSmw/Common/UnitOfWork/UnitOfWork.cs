using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Common.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["WebSite"].ConnectionString;
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IDbConnection conn)
        {
          return  conn.BeginTransaction();
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        /// <param name="transaction"></param>
        public void EndTransactionRollback(IDbTransaction transaction)
        {
            transaction.Commit();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction"></param>
        public void EndTransactionCommit(IDbTransaction transaction)
        {
            transaction.Rollback();
        }

        /// <summary>
        /// 使用事务
        /// </summary>
        /// <param name="action"></param>
        public void ExcuteTransactionScope(Action action)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    action();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // ignored
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }
    }
}

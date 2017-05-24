using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


using System.Configuration;
using System.Transactions;

namespace Common.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public static string ConnString = ConfigurationManager.ConnectionStrings["WebSite"].ConnectionString;

        /// <summary>
        /// 使用分布式事务(嵌套事务的时候可以用TransactionScope)
        /// </summary>
        /// <param name="action"></param>
        public void ExcuteTransaction(Action action)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    action();
                    scope.Complete();
                }
                catch (Exception e)
                {

                    throw e;
                }
                finally
                {
                    scope.Dispose();
                }

            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <returns>当前事务</returns>
        public  IDbTransaction BeginTransaction(IDbConnection conn)
        {
            return conn.BeginTransaction();
        }

        /// <summary>
        /// 结束事务，回滚操作
        /// </summary>
        /// <param name="transaction">要结束的事务</param>
        public void EndTransactionRollback(IDbTransaction transaction)
        {
            transaction.Rollback();
        }

        /// <summary>
        /// 结束事务，确认操作
        /// </summary>
        /// <param name="transaction">要结束的事务</param>
        public void EndTransactionCommit(IDbTransaction transaction)
        {
            transaction.Commit();
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            var conn = new SqlConnection(ConnString);
            conn.Open();
            return conn;
        }
    }
}

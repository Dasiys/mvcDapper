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
        /// 使用事务
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

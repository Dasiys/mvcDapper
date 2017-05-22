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
    public class UnitOfWork: IUnitOfWork
    {
        public static string connString = ConfigurationManager.ConnectionStrings["WebSite"].ConnectionString;


        public IDbConnection Connection { get; private set; }

        public UnitOfWork()
        {
            this.Connection = new SqlConnection(connString);
            if (this.Connection.State != ConnectionState.Open)
            {
                this.Connection.Open();
            }
        }
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

                    throw;
                }

            }
        }

        public void Dispose()
        {
            this.Connection?.Close();
            this.Connection?.Dispose();
            this.Connection = null;
        }
    }
}

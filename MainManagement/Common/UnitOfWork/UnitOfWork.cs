using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


using System.Configuration;



namespace Common.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        public static string connString = ConfigurationManager.ConnectionStrings["WebSite"].ConnectionString;


        public IDbConnection Connection { get; private set; }

        public IDbTransaction Transaction { get; private set; }

        public UnitOfWork()
        {
            this.Connection = new SqlConnection(connString);
            if (this.Connection.State != ConnectionState.Open)
            {
                this.Connection.Open();
            }
        }


        public void BeginTransaction()
        {
            this.Transaction = this.Connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                this.Transaction.Commit();
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }

        }

        public void Dispose()
        {
            this.Connection?.Close();
            this.Connection?.Dispose();
            this.Connection = null;

            this.Transaction?.Dispose();
            this.Transaction = null;
        }

        public void Rollback()
        {
            this.Transaction.Rollback();
        }
    }
}

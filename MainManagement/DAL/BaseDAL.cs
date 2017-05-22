using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.UnitOfWork;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Reflection;
using Model;

namespace DAL
{
    public class BaseDAL<T> where T : class, new()
    {
        

        protected IUnitOfWork UnitOfWork { get; private set; }

        public BaseDAL(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        /// <summary>
        /// 执行 增 删  改 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="strSql"></param>
        /// <returns></returns>
        protected int Execute(T t, string strSql)
        {
            using (var conn=UnitOfWork.Connection)
            {
                var i = conn.Execute(strSql, t);
                return i;
            }
        }

        public virtual void Update(T obj)
        {
            using (var conn = UnitOfWork.Connection)
            {
                

            }
        }

        public List<T> Query(string strSql, DynamicParameters Para)
        {
            using (var conn = UnitOfWork.Connection)
            {
                return conn.Query<T>(strSql, Para).ToList();
            }
        }

        /// <summary>
        /// 公共分页
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public  PageDataView<T> GetPageData<s>(PageCriteria criteria, object param = null)
        {
            using (var conn = UnitOfWork.Connection)
            {

                var p = new DynamicParameters();
                string proName = "ProcGetPageData";
                p.Add("TableName", criteria.TableName);
                p.Add("PrimaryKey", criteria.PrimaryKey);
                p.Add("Fields", criteria.Fields);
                p.Add("Condition", criteria.Condition);
                p.Add("CurrentPage", criteria.CurrentPage);
                p.Add("PageSize", criteria.PageSize);
                p.Add("Sort", criteria.Sort);
                p.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var pageData = new PageDataView<T>();
                pageData.Items = conn.Query<T>(proName, p, commandType: CommandType.StoredProcedure).ToList();

                pageData.TotalNum = p.Get<int>("RecordCount");
                pageData.TotalPageCount = Convert.ToInt32(Math.Ceiling(pageData.TotalNum * 1.0 / criteria.PageSize));


                pageData.CurrentPage = criteria.CurrentPage > pageData.TotalPageCount ? pageData.TotalPageCount : criteria.CurrentPage;


                return pageData;
            }
        }
    }
}

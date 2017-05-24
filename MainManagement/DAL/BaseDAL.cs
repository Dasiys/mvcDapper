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
using Dapper.Contrib.Extensions;
using IDAL;
using Model;

namespace DAL
{
    public class BaseDal<T>
        where T : class, new()
    {
        protected IUnitOfWork UnitOfWork { get; }

        public BaseDal(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// 执行 增 删  改 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="strSql"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        protected int Execute(T t, string strSql,IDbTransaction transaction=null)
        {
            using (var conn=UnitOfWork.GetConnection())
            {
                return conn.Execute(strSql, t,transaction);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int Update(T obj)
        {
            using (var conn = UnitOfWork.GetConnection())
            {
              return conn.Update(obj)?1:0;
            }
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int Insert(T obj)
        {
            using (var conn = UnitOfWork.GetConnection())
            {
               return (int)conn.Insert(obj) ;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int Delete(T obj)
        {
            using (var conn = UnitOfWork.GetConnection())
            {
              return conn.Delete(obj)?1:0;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public List<T> Query(string strSql, DynamicParameters para)
        {
            using (var conn = UnitOfWork.GetConnection())
            {
                return conn.Query<T>(strSql, para).ToList();
            }
        }

        /// <summary>
        /// 单条记录查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public virtual T GetSingleModel(string strSql, DynamicParameters para)
        {
            using (var conn = UnitOfWork.GetConnection())
            {
                return conn.QueryFirst<T>(strSql, para);
            }
        }

        /// <summary>
        /// 公共分页
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual  PageDataView<T> GetPageData(PageCriteria criteria, object param = null)
        {
            using (var conn = UnitOfWork.GetConnection())
            {
                var p = new DynamicParameters();
                const string proName = "ProcGetPageData";
                p.Add("TableName", criteria.TableName);
                p.Add("PrimaryKey", criteria.PrimaryKey);
                p.Add("Fields", criteria.Fields);
                p.Add("Condition", criteria.Condition);
                p.Add("CurrentPage", criteria.CurrentPage);
                p.Add("PageSize", criteria.PageSize);
                p.Add("Sort", criteria.Sort);
                p.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var pageData = new PageDataView<T>
                {
                    Items = conn.Query<T>(proName, p, commandType: CommandType.StoredProcedure).ToList(),
                    TotalNum = p.Get<int>("RecordCount")
                };
                pageData.TotalPageCount = Convert.ToInt32(Math.Ceiling(pageData.TotalNum * 1.0 / criteria.PageSize));
                pageData.CurrentPage = criteria.CurrentPage > pageData.TotalPageCount ? pageData.TotalPageCount : criteria.CurrentPage;
                return pageData;
            }
        }

    }
}

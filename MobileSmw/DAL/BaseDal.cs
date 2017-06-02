using Common.UnitOfWork;
using Model.MetadataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using IDAL;
using Model.ViewModel;

namespace DAL
{
    public class BaseDal<T> where T:class,IEntityBase,new()
    {
        protected readonly  IUnitOfWork UnitOfWork;

        public BaseDal(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        /// <summary>
        /// 增加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Insert(T entity)
        {
            using (var conn = UnitOfWork.GetDbConnection())
            {
                entity.Id=(int)conn.Insert(entity);
                return entity.Id;
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Delete(T entity)
        {
            using (var conn=UnitOfWork.GetDbConnection())
            {
                return conn.Delete(entity);
            }
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Boolean Update(T entity)
        {
            using (var conn=UnitOfWork.GetDbConnection())
            {
                return conn.Update(entity);
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Excute(string sql, object param)
        {
            using (var conn=UnitOfWork.GetDbConnection())
            {
                return conn.Execute(sql, param);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="param"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public TM GetSingleModel<TM>(string tableName, string condition,object param, string fields = "*")
        {
            using (var conn = UnitOfWork.GetDbConnection())
            {
                condition = string.IsNullOrEmpty(condition) ? "" : $"where {condition}";
                var sql = $"select {fields} from {tableName} {condition}";
                return conn.QueryFirstOrDefault<TM>(sql, param);
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="param"></param>
        /// <param name="fields"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<TM> GetModels<TM>(string tableName, string condition, object param, string fields = "*", string orderBy = "")
        {
            using (var conn = UnitOfWork.GetDbConnection())
            {
                condition = string.IsNullOrEmpty(condition) ? "" : $"where {condition}";
                var sql = $"select {fields} from {tableName} {condition} {orderBy}";
                return conn.Query<TM>(sql, param)?.ToList()??new List<TM>();
            }
        }
       
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public PageDataView<TM> GetPageData<TM>(PageCriteria criteria, object param = null)
            where TM : class, IEntityBase, new()
        {
            using (var conn =UnitOfWork.GetDbConnection())
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
                var pageData = new PageDataView<TM>
                {
                    Items = conn.Query<TM>(proName, p, commandType: CommandType.StoredProcedure).ToList(),
                    RecordCount = p.Get<int>("RecordCount")
                };
                pageData.TotalPageCount = Convert.ToInt32(Math.Ceiling(pageData.RecordCount * 1.0 / criteria.PageSize));
                pageData.CurrentPage = criteria.CurrentPage > pageData.TotalPageCount ? pageData.TotalPageCount : criteria.CurrentPage;
                return pageData;
            }
        }

        /// <summary>
        /// 执行普通的存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ExcuteProc(string procName, object param)
        {
            using (var conn=UnitOfWork.GetDbConnection())
            {
                return conn.Execute(procName, param, null, null, CommandType.StoredProcedure);
            }
        }
    }
}

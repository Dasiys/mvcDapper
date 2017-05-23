using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.UnitOfWork;
using IDAL;
using Model;
using System.Transactions;
using Common.NLog;
using IBLL;

namespace BLL
{
    public class BaseService<T>
        where T : class, new()
    {
        private  readonly IBaseDal<T> _dal;
        public ILogFactory LogFactory;

        public BaseService(IBaseDal<T> dal,ILogFactory logFactory)
        {
            _dal = dal;
            LogFactory = logFactory;
        }

        /// <summary>
        /// 插入新的对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Insert(T t)
        {
            LogFactory.Info(LogType.Sql,$"{System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return _dal.Insert(t);
        }

        /// <summary>
        /// 更新某个对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Update(T t)
        {
            return _dal.Update(t);

        }

        /// <summary>
        /// 删除某个对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Delete(T t)
        {
            return _dal.Delete(t);

        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public T GetSingleModel(T t)
        {
            return _dal.GetSingleModel(t);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<T> GetModels(T t)
        {
            return _dal.GetModels(t);
        }

        /// <summary>
        /// 获取分页对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public PageDataView<T> GetModelsByPage(PageCriteria t)
        {
            return _dal.GetPageData(t);
        }

    }
}

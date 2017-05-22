using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.UnitOfWork;
using IDAL;
using Model;
using System.Transactions;

namespace BLL
{
    public class BaseService<T> where T : class, new()
    {
        public IBaseDal<T> Dal;

        public BaseService(IBaseDal<T> dal)
        {
            Dal = dal;
        }

        /// <summary>
        /// 插入新的对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Insert(T t)
        {
            return Dal.Insert(t);

        }

        /// <summary>
        /// 更新某个对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Update(T t)
        {
            return Dal.Update(t);

        }

        /// <summary>
        /// 删除某个对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Delete(T t)
        {
            return Dal.Delete(t);

        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public T GetSingleModel(T t)
        {
            return Dal.GetSingleModel(t);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<T> GetModels(T t)
        {
            return Dal.GetModels(t);
        }

        /// <summary>
        /// 获取分页对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public PageDataView<T> GetModelsByPage(PageCriteria t)
        {
            return Dal.GetModelsByPage(t);
        }

    }
}

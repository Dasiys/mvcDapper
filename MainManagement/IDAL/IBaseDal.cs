using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;


namespace IDAL
{
    public interface IBaseDAL<T> where T : class, new()
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Insert(T t);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Update(T t);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Delete(T t);

        /// <summary>
        /// 单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T GetSingleModel(T entity);

        /// <summary>
        /// 多个实体例表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        List<T> GetModels(T entity);

        PageDataView<T> GetPageData(PageCriteria criteria, object param = null);

    }
}

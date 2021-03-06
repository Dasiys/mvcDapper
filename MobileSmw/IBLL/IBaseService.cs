﻿using Model.MetadataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ViewModel;

namespace IBLL
{
    public interface IBaseService<in T> where T:class ,IEntityBase,new()
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        int Excute(string sql, object param);
        /// <summary>
        /// 插入新对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(T entity);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Boolean Delete(T entity);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Boolean Update(T entity);
        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="param"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        TM GetSingleModel<TM>(string tableName, string condition, object param, string fields = "*");
        /// <summary>
        /// 获取多个对象
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="param"></param>
        /// <param name="fields"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        List<TM> GetModels<TM>(string tableName, string condition, object param, string fields = "*", string orderBy = "");
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        PageDataView<TM> GetPageData<TM>(PageCriteria criteria, object param = null)
            where TM : class, IEntityBase, new();
    }
}

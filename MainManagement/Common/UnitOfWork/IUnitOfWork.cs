using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Ioc;


namespace Common.UnitOfWork
{
    public interface IUnitOfWork:IDependency
    {
        /// <summary>
        /// 使用事务
        /// </summary>
        /// <param name="action"></param>
        void ExcuteTransaction(Action action);

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
    }
}

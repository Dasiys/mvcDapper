using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IOC;
using Model.Dtos;
using Model.MetadataModel;

namespace IDAL
{
    public interface IOrderDal:IBaseDal<TB_Order>,IDependency
    {

        /// <summary>
        /// Multiple Query
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        TM MultipleQuery<TM>(string sql, DynamicParameters param, Func<SqlMapper.GridReader, TM> func);
    }
}

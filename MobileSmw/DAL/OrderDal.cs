using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Common.UnitOfWork;
using Dapper;
using IDAL;
using Model.Dtos;
using Model.MetadataModel;

namespace DAL
{
    public class OrderDal:BaseDal<TB_Order>,IOrderDal
    {
        public OrderDal(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        /// <summary>
        /// Multiple Query
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public TM MultipleQuery<TM>(string sql, DynamicParameters param, Func<SqlMapper.GridReader, TM> func)
        {
            using (var conn=UnitOfWork.GetDbConnection())
            {
                using (var multi=conn.QueryMultiple(sql,param))
                {
                  return  func(multi);
                }
            }
        }
    }
}

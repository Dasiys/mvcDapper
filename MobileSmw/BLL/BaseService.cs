using IDAL;
using Model.MetadataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ViewModel;

namespace BLL
{
    public class BaseService<T> where T:class ,IEntityBase,new ()
    {
        protected readonly IBaseDal<T> Basedal;

        public BaseService(IBaseDal<T> baseDal)
        {
            Basedal = baseDal;
        }

        public virtual int Excute(string sql, object param)
        {
            return Basedal.Excute(sql, param);
        }

        public virtual int Insert(T entity)
        {
            return Basedal.Insert(entity);
        }

        public virtual Boolean Delete(T entity)
        {
            return Basedal.Delete(entity);
        }

        public virtual Boolean Update(T entity)
        {
            return Basedal.Update(entity);
        }

        public TM GetSingleModel<TM>(string tableName, string condition, object param, string fields = "*")
        {
            return Basedal.GetSingleModel<TM>(tableName, condition, param, fields);
        }

        public List<TM> GetModels<TM>(string tableName, string condition, object param, string fields = "*",
            string orderBy = "")
        {
            return Basedal.GetModels<TM>(tableName, condition, param, fields);
        }

        public PageDataView<TM> GetPageData<TM>(PageCriteria criteria, object param = null)
            where TM : class, IEntityBase, new()
        {
            return Basedal.GetPageData<TM>(criteria, param);
        }
    }
}

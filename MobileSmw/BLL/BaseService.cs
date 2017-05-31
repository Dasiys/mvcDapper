using IDAL;
using Model.MetadataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Common.NLog;
using Model;
using Model.ViewModel;
using Newtonsoft.Json;

namespace BLL
{
    public class BaseService<T> where T:class ,IEntityBase,new ()
    {
        protected readonly IBaseDal<T> Basedal;
        public ILogFactory _LogFactory;
        public BaseService(IBaseDal<T> baseDal,ILogFactory logFactory)
        {
            Basedal = baseDal;
            _LogFactory = logFactory;
        }

        public virtual int Excute(string sql, object param)
        {
            return Basedal.Excute(sql, param);
        }

        public virtual int Insert(T entity)
        {
            Validate(entity);
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

        public void ExceptionThrow(string errorName, string errormsg)
        {

            throw  new Exception("{"+$"\"ErrorName\":\"{errorName}\",\"ErrorMsg\":\"{errormsg}\"" +"}");
        }

        public virtual void Validate(T entity)
        {
            
        }
    }
}

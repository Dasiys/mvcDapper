using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Model;

namespace BLL
{
    public class BaseService<T> where T : class, new()
    {
        public IBaseDal<T> Dal;


        public int Insert(T t)
        {
            return Dal.Insert(t);

        }

        public int Update(T t)
        {
            return Dal.Update(t);

        }

        public int Delete(T t)
        {
            return Dal.Delete(t);

        }
        public T GetSingleModel(T t)
        {
            return Dal.GetSingleModel(t);
        }

        public List<T> GetModels(T t)
        {
            return Dal.GetModels(t);
        }

        public PageDataView<T> GetModelsByPage(PageCriteria t)
        {
            return Dal.GetModelsByPage(t);
        }
    }
}

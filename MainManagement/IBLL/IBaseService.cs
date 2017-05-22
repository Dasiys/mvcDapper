using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IBLL
{
    public interface IBaseService<T> where T : class, new()
    {
        int Insert(T t);

        int Update(T t);

        int Delete(T t);

        T GetSingleModel(T t);

        List<T> GetModels(T t);

        PageDataView<T> GetModelsByPage(PageCriteria t);
    }
}

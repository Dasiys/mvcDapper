using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model.MetadataModel;
using Model.ViewModel;

namespace IDAL
{
    public interface IBaseDal<in T> where T:class, IEntityBase,new()
    {
        int Excute(string sql, object param);
        int Insert(T entity);

        Boolean Delete(T entity);

        Boolean Update(T entity);

        TM GetSingleModel<TM>(string tableName, string condition, object param, string fields = "*");

        List<TM> GetModels<TM>(string tableName, string condition, object param, string fields = "*", string orderBy = "");

        PageDataView<TM> GetPageData<TM>(PageCriteria criteria, object param = null)
            where TM : class, IEntityBase, new();
    }
}

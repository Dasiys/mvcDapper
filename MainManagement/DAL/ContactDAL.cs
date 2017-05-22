using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IDAL;
using Model;
using Dapper;
using Common.UnitOfWork;


namespace DAL
{
    public class ContactDal : BaseDAL<Contact>, IContactDal
    {
        public ContactDal(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        public Contact GetSingleModel(Contact t)
        {
            var args = new DynamicParameters(new { });

            string query = "SELECT * FROM Contact WHERE  ContactID=@ContactID";
            args.Add("ContactID", t.ContactID);
            return base.Query(query, args).SingleOrDefault();
        }

        public List<Contact> GetModels(Contact t)
        {
            var args = new DynamicParameters(new { });
            string query = "SELECT * FROM Contact WHERE id > @id order by id desc ";
            args.Add("ID", t.ID);
            return base.Query(query, args);
        }


        public  PageDataView<Contact> GetModelsByPage(PageCriteria criteria )
        {
            var r = base.GetPageData<Contact>(criteria);
            return r;
        }

    }
}

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

        public int Insert(Contact t)
        {
            var i = base.Execute(t, @"insert into Contact(ContactID,Tel,ContactName)values(@ContactID,@Tel,@ContactName)");
            return i;
        }

        public int Update(Contact t)
        {
            var i = base.Execute(t, @"update Contact set Tel=@Tel,ContactName=@ContactName where ContactID=@ContactID");
            return i;
        }

        public int Delete(Contact t)
        {
            var i = base.Execute(t, @"delete from Contact  where ContactID=@ContactID");
            return i;
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

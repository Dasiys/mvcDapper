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
        public  Contact GetSingleModel(Contact t)
        {
            string query = "SELECT * FROM Contact WHERE  ContactID=@ContactID";
            var args = new DynamicParameters(new { });
            args.Add("ContactID", t.ContactID);
            return base.GetSingleModel(query,  args);
        }

        public List<Contact> GetModels(Contact t)
        {
            var args = new DynamicParameters(new { });
            string query = "SELECT * FROM Contact WHERE id > @id order by id desc ";
            args.Add("ID", t.ID);
            return base.Query(query, args);
        }

    }
}

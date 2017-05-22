using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBLL;
using Model;
using IDAL;
using Common.UnitOfWork;


namespace BLL
{
    public class ContactService:BaseService<Contact>,IContactService
    {
        IContactDal _Dal;
        IUnitOfWork _unitofwork;

        public ContactService(IContactDal dal, IUnitOfWork unitofwork)
        {
            Dal = dal;
            _Dal = dal;
            _unitofwork = unitofwork;
        }

        public void BeginTransactionInsert(Contact t)
        {
            using (_unitofwork)
            {
                var a = 5;
                _unitofwork.UseTransaction(()=>
                {
                    t.ContactID = Guid.NewGuid();
                    a=_Dal.Insert(t);
                });
                var b = a;
            }  
        }
    }
}

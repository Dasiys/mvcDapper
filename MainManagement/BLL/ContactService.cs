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
               _unitofwork.BeginTransaction();
                 t.ContactID = Guid.NewGuid();
                _Dal.Insert(t);
                 t.ContactID = Guid.NewGuid();
                _Dal.Insert(t);

                //开启事务后一定要提交工作单元....
                _unitofwork.Commit();
            }
           

            
        }
    }
}

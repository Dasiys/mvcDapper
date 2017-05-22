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
        private readonly  IUnitOfWork _unitOfWork;
        public ContactService(IContactDal dal,IUnitOfWork unitOfwork):base(dal)
        {
            _unitOfWork = unitOfwork;
        }

        public void BeginTransactionInsert(Contact t)
        {
            _unitOfWork.ExcuteTransaction(() =>
            {
                t.ContactID=new Guid();
                Dal.Insert(t);
            });
        }
    }
}

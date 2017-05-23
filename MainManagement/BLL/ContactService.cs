using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NLog;
using IBLL;
using Model;
using IDAL;
using Common.UnitOfWork;


namespace BLL
{
    public class ContactService:BaseService<Contact>,IContactService
    {
        private readonly  IUnitOfWork _unitOfWork;
        private readonly IContactDAL _dal;
        public ContactService(IContactDAL dal,IUnitOfWork unitOfwork,ILogFactory logFactory):base(dal,logFactory)
        {
            _dal = dal;
            _unitOfWork = unitOfwork;
        }

        public void BeginTransactionInsert(Contact t)
        {
            _unitOfWork.ExcuteTransaction(() =>
            {
                t.ContactID=Guid.NewGuid();
                Insert(t);
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NLog;
using IBLL;
using Model;
using IDAL;
using Common.UnitOfWork;
using DAL;

namespace BLL
{
    public class ContactService:BaseService<Contact>,IContactService
    {
        private readonly  IUnitOfWork _unitOfWork;
        private readonly IContactDal _dal;

        public ContactService(IContactDal dal,IUnitOfWork unitOfwork,ILogFactory logFactory):base(dal,logFactory)
        {
            _dal = dal;
            _unitOfWork = unitOfwork;
        }

        public void BeginTransactionInsert(Contact t)
        {
            using (var conn=_unitOfWork.GetConnection())
            {
                var transaction = _unitOfWork.BeginTransaction(conn);
                try
                {
                    t.ContactID = Guid.NewGuid();
                    _dal.Insert(t);
                    t.ContactID = Guid.NewGuid();
                    _dal.Insert(t);
                    _unitOfWork.EndTransactionCommit(transaction);
                }
                catch (Exception e)
                {
                    LogFactory.Error(LogType.Sql,System.Reflection.MethodBase.GetCurrentMethod().Name,e.Message);
                    _unitOfWork.EndTransactionRollback(transaction);
                    throw;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }
    }
}

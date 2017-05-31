using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.UnitOfWork;
using Model.MetadataModel;
using IDAL;

namespace DAL
{
    public class UserDal:BaseDal<TB_UserInfo>,IUserDal
    {
        public UserDal(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

    }
}

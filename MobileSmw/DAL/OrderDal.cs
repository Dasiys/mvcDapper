using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.UnitOfWork;
using IDAL;
using Model.MetadataModel;

namespace DAL
{
    public class OrderDal:BaseDal<TB_Order>,IOrderDal
    {
        public OrderDal(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

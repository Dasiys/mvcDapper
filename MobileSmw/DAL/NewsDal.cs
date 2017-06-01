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
    public class NewsDal:BaseDal<TB_News>,INewsDal
    {
        public NewsDal(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

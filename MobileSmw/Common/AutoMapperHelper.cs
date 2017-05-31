using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dtos;
using Model.MetadataModel;

namespace Common
{
    /// <summary>
    /// 对象之间的映射
    /// </summary>
    public class AutoMapperHelper
    {
        public static void Map()
        {
            Mapper.Initialize(map =>
            {
                map.CreateMap<CreditApplyModifyModel, TB_XiMuCreditApply>()
                    .ForMember(entity => entity.BuyUserRecordID, opt => opt.Ignore())
                    .ForMember(entity => entity.Id, opt => opt.Ignore())
                    .ForMember(entity => entity.InputTime, opt => opt.Ignore())
                    .ForMember(entity => entity.State, opt => opt.Ignore());
            });
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                
            });
        }
    }
}

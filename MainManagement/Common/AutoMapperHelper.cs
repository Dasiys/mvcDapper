using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Common
{
    /// <summary>
    /// 对象的映射
    /// </summary>
    public static class AutoMapperHelper
    {
        public static void MapObject()
        {
            Mapper.Initialize(map =>
            {
            });
        }
    }
}

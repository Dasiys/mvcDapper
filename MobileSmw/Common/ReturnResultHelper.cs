using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace Common
{
    public static  class ReturnResultHelper
    {
        public static string ReturnResult(ResultModel model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}

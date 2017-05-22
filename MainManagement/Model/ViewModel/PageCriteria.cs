using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PageCriteria
    {

        public string TableName{ get;set; }

        public string Fields {get;set; }

        public string PrimaryKey {get;set; } 

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public string Sort { get; set; }

        public string Condition { get; set; }

        public int RecordCount { get; set; }
    }
}

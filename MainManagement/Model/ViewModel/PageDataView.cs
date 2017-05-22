using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PageDataView<T> where T : class, new()
    {

        public int TotalNum { get; set; }

        public List<T> Items { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPageCount { get; set; }

    }
}

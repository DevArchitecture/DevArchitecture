using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public interface ITableGlobalSearchFilter
    {
        public int First { get; set; }
        public int Rows { get; set; }
        public string SortField { get; set; }
        public int SortOrder { get; set; }
        public string SearchText { get; set; }
        public List<string> PropertyField { get; set; }
    }
}

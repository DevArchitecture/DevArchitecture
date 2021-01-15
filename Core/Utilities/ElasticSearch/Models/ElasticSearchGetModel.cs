using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.ElasticSearch.Models
{
    public class ElasticSearchGetModel<T>
    {
        public string ElasticId { get; set; }
        public T Item { get; set; }
    }
}

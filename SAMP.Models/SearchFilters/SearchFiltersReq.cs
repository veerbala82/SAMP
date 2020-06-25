using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.SearchFilters
{
    public partial class SearchFiltersReq
    {       
        public List<Filter> Filters { get; set; }
    }

    public partial class Filter
    {     
        public string FilterName { get; set; }
     
        public string FilterValue { get; set; }
    }
}

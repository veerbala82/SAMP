using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.Common
{
    public partial class RemarksReq
    {        
        public Request Request { get; set; }
    }

    public partial class Request
    {     
        public EsRemarks EsRemarks { get; set; }
    }

    public partial class EsRemarks
    {     
        public RemarksDetails RemarksDetails { get; set; }
    }

    public partial class RemarksDetails
    {     
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }
}

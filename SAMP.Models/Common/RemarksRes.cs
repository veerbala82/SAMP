using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.Common
{
    public partial class RemarksRes
    {       
        public Response Response { get; set; }
    }

    public partial class Response
    {     
        public EsRemarks EsRemarks { get; set; }
    }

    public partial class EsRemarks
    {     
        public List<RemarksDetail> Remarks { get; set; }
    }

    public partial class RemarksDetail
    {
        public int RemarksId { get; set; }
        public string RemarksDetails { get; set; }
        public string Remarksdate { get; set; }
    }
}

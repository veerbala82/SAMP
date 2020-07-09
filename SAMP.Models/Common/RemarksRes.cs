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
        public EsRemarksRes EsRemarks { get; set; }
    }

    public partial class EsRemarksRes
    {     
        public List<RemarksDetailRes> RemarksDetails { get; set; }
    }

    public partial class RemarksDetailRes
    {
        public int RemarksId { get; set; }
        public string RemarksDetails { get; set; }
        public string Remarksdate { get; set; }
    }
}

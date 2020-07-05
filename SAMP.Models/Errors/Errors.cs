using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.Errors
{
    public partial class Errors
    {       
        public Response Response { get; set; }
    }

    public partial class Response
    {       
        public EsErrors EsErrors { get; set; }
    }

    public partial class EsErrors
    {       
        public long? ErrorCode { get; set; }
      
        public string ErrorDescription { get; set; }
       
        public string ErrorReference { get; set; }
    }
}

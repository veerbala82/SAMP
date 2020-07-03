using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.SOW
{
    //Response for Insert/Update
    public partial class SOWSaveRes
    {
        public SaveResponse Response { get; set; }
    }

    public partial class SaveResponse
    {
        public string SOWNo { get; set; }

        public object EsErrors { get; set; }
    }    
}
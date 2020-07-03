using System.Collections.Generic;

namespace SAMP.Models.SOW
{
    //Search Response
    public class SOWSearchRes
    {
        public SearchResponse Response { get; set; }
    }

    public class SearchResponse
    {
        public EsSOWs EsDeliveryNotes { get; set; }

        public object EsErrors { get; set; }
    }

    public class EsSOWs
    {
        public List<SOW> SOWs { get; set; }
    }

    public class SOW
    {
        public string SOWNo { get; set; }

        public string SOWDesc { get; set; }

        public List<Resource> Resources { get; set; }
    }

    public class Resource
    {
        public string DisplayName { get; set; }

        public string EmpID { get; set; }

        public string SecurityID { get; set; }
    }
}

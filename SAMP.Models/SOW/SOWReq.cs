using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAMP.Models.SOW
{
    public partial class SOWReq
    {        
        public Request Request { get; set; }
    }

    public partial class Request
    {        
        public EsSOWMaster EsSOWMaster { get; set; }     
        public object EsErrors { get; set; }
    }

    public partial class EsSOWMaster
    {        
        public List<SOWMaster> SOWMasters { get; set; }
    }

    public partial class SOWMaster
    {
        [Required]
        public int SOWId { get; set; }
        [Required]
        public string SOWNo { get; set; }
        [Required]
        public string SOWDesc { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string PO { get; set; }
        [Required]
        public string SOWType { get; set; }
        [Required]
        public string ActionBySPOC { get; set; }
        [Required]
        public decimal SOWAmount { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public int CustMgrId { get; set; }
        [Required]
        public int Statusid { get; set; }

        public int RevisionNumber { get; set; }
        public DateTime RevisionDate { get; set; }

        public int InvoiceId { get; set; }
        [Required]
        public bool PartialBilliing { get; set; }
        [Required]
        public bool FOC { get; set; }
        [Required]
        public string User { get; set; }
    }
}

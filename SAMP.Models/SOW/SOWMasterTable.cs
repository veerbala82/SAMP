using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.SOW
{
    public class SOWMasterTable
    {
        public int SOWId { get; set; }
        public string SOWNo { get; set; }
        public string SOWDesc { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PO { get; set; }
        public int SOWType_SystemParamId { get; set; }
        public string ActionBySPOC { get; set; }
        public int SOWAmount { get; set; }
        public int AccountId { get; set; }
        public int CustMgrId { get; set; }
        public int Status_SystemParamId { get; set; }
        public bool PartialBilliing { get; set; }
        public int FOC { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
    }
}

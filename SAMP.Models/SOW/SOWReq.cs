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
        public string SOWNo { get; set; }
        [Required]
        public string SOWDesc { get; set; }

        public string SOWStartDate { get; set; }

        public string SOWEndDate { get; set; }

        public string PO { get; set; }

        public int SOWType_SystemParamId { get; set; }

        public string ActionBySPOC { get; set; }

        public int SOWAmount { get; set; }

        public int AccountId { get; set; }

        public int CustMgrId { get; set; }

        public int Status_SystemParamId { get; set; }      

        public bool PartialBilliing { get; set; }

        public int FOC { get; set; }

        public List<ResourceUtilizationDetail> ResourceUtilizationDetails { get; set; }

        public List<PricingScheduleDetail> PricingScheduleDetails { get; set; }
    }

    public partial class ResourceUtilizationDetail
    {
        public int ResourceId { get; set; }

        public string WorkOrderStartDate { get; set; }

        public string WorkOrderEndDate { get; set; }

        public string FoCStartDate { get; set; }

        public string FoCEndDate { get; set; }
    }

    public partial class PricingScheduleDetail
    {
        public string MonthName { get; set; }

        public int BillableDays { get; set; }

        public string MilestoneName { get; set; }

        public string MilestoneValue { get; set; }             
    }
}

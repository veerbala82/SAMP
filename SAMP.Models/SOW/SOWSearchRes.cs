using System;
using System.Collections.Generic;

namespace SAMP.Models.SOW
{
    //SOW Search Response
    public partial class SOWSearchResponse
    {
        public ResponseSR Request { get; set; }
    }

    public partial class ResponseSR
    {
        public EsSOWMasterSR EsSOWMaster { get; set; }
        public object EsErrors { get; set; }
    }

    public partial class EsSOWMasterSR
    {
        public List<SOWMasterSR> SOWMasters { get; set; }
    }

    public partial class SOWMasterSR
    {
        public int SOWId { get; set; }
        public string SowNo { get; set; }
        public string SowDesc { get; set; }
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
        public List<ResourceUtilizationDetailSearchResponse> ResourceUtilizationDetails { get; set; }
        public List<PricingScheduleDetailSearchResponse> PricingScheduleDetails { get; set; }
    }

    //Add Later
    public partial class ResourceUtilizationDetailSearchResponse
    {
        public int ResourceUtilizationId { get; set; }
        public int SOWId { get; set; }
        public int ResourceId { get; set; }
        public string WorkOrderStartDate { get; set; }
        public string WorkOrderEndDate { get; set; }
        public string BenchStartDate { get; set; }
        public string BenchEndDate { get; set; }
        public string FoCStartDate { get; set; }
        public string FoCEndDate { get; set; } 
    }

    public partial class PricingScheduleDetailSearchResponse
    {
        public int PricingSchId { get; set; }
        public int SOWId { get; set; }
        public string MonthName { get; set; }
        public int BillableDays { get; set; }
        public string MilestoneName { get; set; }
        public string MilestoneValue { get; set; }
        public int RevisionNumber { get; set; }
        public string RevisionDate { get; set; }       
    }
}

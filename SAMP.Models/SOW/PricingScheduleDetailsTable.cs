using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.SOW
{
    public class PricingScheduleDetailsTable
    {
        public int PricingSchId { get; set; }
        public int SOWId { get; set; }
        public string MonthName { get; set; }
        public int BillableDays { get; set; }
        public string MilestoneName { get; set; }
        public string MilestoneValue { get; set; }
        public int RevisionNumber { get; set; }
        public DateTime RevisionDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
    }
}

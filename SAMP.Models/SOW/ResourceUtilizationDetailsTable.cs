using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.SOW
{
    public class ResourceUtilizationDetailsTable
    {
        public int ResourceUtilizationId { get; set; }
        public int SOWId { get; set; }
        public int ResourceId { get; set; }
        public DateTime WorkOrderStartDate { get; set; }
        public DateTime WorkOrderEndDate { get; set; }
        public DateTime BenchStartDate { get; set; }
        public DateTime BenchEndDate { get; set; }
        public DateTime FoCStartDate { get; set; }
        public DateTime FoCEndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
    }
}

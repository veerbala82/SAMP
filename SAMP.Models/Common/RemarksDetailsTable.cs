using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.Common
{
    public class RemarksDetailsTable
    {
        public int RemarksId { get; set; }
        public int SOWId { get; set; }
        public int ResourceId { get; set; }
        public int PricingSchId { get; set; }
        public int ResAttId { get; set; }
        public string RemarksDetails { get; set; }
        public DateTime Remarksdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
    }
}

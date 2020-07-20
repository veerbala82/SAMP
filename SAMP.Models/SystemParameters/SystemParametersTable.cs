using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.SystemParameters
{
    public class SystemParametersTable
    {
        public int SystemParamId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
    }
}

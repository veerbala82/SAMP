using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.SystemParameters
{
    public class SPRes
    {
        public Response Response { get; set; }
    }

    public partial class Response
    {
        public EsSystemParametersRes EsSystemParameters { get; set; }
    }

    public partial class EsSystemParametersRes
    {
        public List<SystemParametersRes> SystemParameters { get; set; }
    }

    public partial class SystemParametersRes
    {
        public int SystemParamId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Active { get; set; }
    }
}

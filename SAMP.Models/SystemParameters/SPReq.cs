using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.SystemParameters
{
    public class SPReq
    {
        public Request Request { get; set; }
    }

    public partial class Request
    {
        public EsSP EsSP { get; set; }
    }

    public partial class EsSP
    {
        public SystemParameters SystemParameters { get; set; }
    }

    public partial class SystemParameters
    {
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }
}

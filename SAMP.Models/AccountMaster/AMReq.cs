using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.AccountMaster
{
    public class AMReq
    {
        public Request Request { get; set; }
    }

    public partial class Request
    {
        public EsAM EsAM { get; set; }
    }

    public partial class EsAM
    {
        public AccountMaster AccountMaster { get; set; }
    }

    public partial class AccountMaster
    {
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }
}

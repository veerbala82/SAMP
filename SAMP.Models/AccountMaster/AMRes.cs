using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.AccountMaster
{
    public class AMRes
    {
        public Response Response { get; set; }
    }

    public partial class Response
    {
        public EsAccountMasterRes EsAccountMaster { get; set; }
    }

    public partial class EsAccountMasterRes
    {
        public List<AccountMasterRes> AccountMaster { get; set; }
    }

    public partial class AccountMasterRes
    {
        public int AccountId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLocation { get; set; }
        public string SOWAccountName { get; set; }
        public string DisplayName { get; set; }

    }
}

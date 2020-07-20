using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.Models.AccountMaster
{
    public class AccountMasterTable
    {
        public int AccountId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLocation { get; set; }
        public string SOWAccountName { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
    }
}

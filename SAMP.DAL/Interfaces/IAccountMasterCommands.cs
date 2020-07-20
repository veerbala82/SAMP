using SAMP.Models.AccountMaster;
using SAMP.Models.Common;
using SAMP.Models.SearchFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.DAL.Interfaces
{
    public interface IAccountMasterCommands
    {
        AMRes GetAccountMaster(SearchFiltersReq req);
    }
}

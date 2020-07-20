using SAMP.Models.AccountMaster;
using SAMP.Models.Common;
using SAMP.Models.SearchFilters;

namespace SAMP.BAL.Interfaces
{
    public interface IAccountMasterService
    {
        AMRes GetAccountMaster(SearchFiltersReq req);
    }
}
using SAMP.Models.Common;
using SAMP.Models.SearchFilters;

namespace SAMP.BAL
{
    public interface IRemarksService
    {
        RemarksRes GetRemarks(SearchFiltersReq req);
    }
}
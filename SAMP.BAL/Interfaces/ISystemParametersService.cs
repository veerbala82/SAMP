using SAMP.Models.SearchFilters;
using SAMP.Models.SystemParameters;

namespace SAMP.BAL.Interfaces
{
    public interface ISystemParametersService
    {
        SPRes GetSystemParameters(SearchFiltersReq req);
    }
}
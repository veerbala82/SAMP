using SAMP.Models.SearchFilters;
using SAMP.Models.SystemParameters;

namespace SAMP.DAL.Interfaces
{
    public interface ISystemParametersCommands
    {
        SPRes GetSystemParameters(SearchFiltersReq req);
    }
}

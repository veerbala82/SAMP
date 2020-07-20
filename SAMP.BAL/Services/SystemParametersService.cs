using SAMP.BAL.Interfaces;
using SAMP.DAL.Interfaces;
using SAMP.Models.SearchFilters;
using SAMP.Models.SystemParameters;

namespace SAMP.BAL.Services
{
    public class SystemParametersService: ISystemParametersService
    {
        private readonly ISystemParametersCommands _sPCommand;

        public SystemParametersService(ISystemParametersCommands sPCommand)
        {
            this._sPCommand = sPCommand;
        }

        public SPRes GetSystemParameters(SearchFiltersReq req)
        {
            var data = new SPRes();

            data = _sPCommand.GetSystemParameters(req);

            return data;
        }
    }
}

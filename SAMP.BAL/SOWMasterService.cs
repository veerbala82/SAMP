using SAMP.DAL;
using SAMP.Models.SearchFilters;
using SAMP.Models.SOW;

namespace SAMP.BAL
{
    public class SOWMasterService: ISOWMasterService
    {
        private readonly ISOWMasterCommands _sowMasterCommand;

        public SOWMasterService(ISOWMasterCommands sowMasterCommand)
        {
            this._sowMasterCommand = sowMasterCommand;
        }

        public SOWSearchResponse GetSOWs(SearchFiltersReq req)
        {
            return this._sowMasterCommand.GetSOWs(req);
        }

        public SOWSaveRes InsertSOWMaster(SOWReq req, string user)
        {
            return this._sowMasterCommand.InsertSOWMaster(req, user);
        }
    }
}

using SAMP.Models.SearchFilters;
using SAMP.Models.SOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.DAL
{
    public interface ISOWMasterCommands
    {
        SOWSearchResponse GetSOWs(SearchFiltersReq req);

        SOWSaveRes InsertSOWMaster(SOWReq req, string user);

        //SOWRes UpdatedSOWMaster(SOWReq req);
    }
}

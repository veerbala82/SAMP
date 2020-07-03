using SAMP.Models.SearchFilters;
using SAMP.Models.SOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.BAL
{
    public interface ISOWMasterService
    {
        SOWSearchRes GetSOWs(SearchFiltersReq req);        

        SOWSaveRes InsertSOWMaster(SOWReq req, string user);

        //SOWRes UpdatedSOWMaster(SOWReq req);
    }
}

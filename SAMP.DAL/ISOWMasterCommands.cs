using SAMP.Models.SearchFilters;
using SAMP.Models.SOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.DAL
{
    public interface ISOWMasterCommands
    {
        SOWRes GetSOWMaster(SearchFiltersReq req);

        SOWRes InsertSOWMaster(SOWReq req);

        SOWRes UpdatedSOWMaster(SOWReq req);
    }
}

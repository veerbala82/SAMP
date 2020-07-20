using SAMP.Models.Common;
using SAMP.Models.SearchFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.DAL.Interfaces
{
    public interface IRemarksCommands
    {
        RemarksRes GetRemarks(SearchFiltersReq req);
    }
}

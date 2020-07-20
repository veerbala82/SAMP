using SAMP.DAL.Interfaces;
using SAMP.Models.Common;
using SAMP.Models.SearchFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.BAL
{
    public class RemarksService: IRemarksService
    {
        private readonly IRemarksCommands _remarksCommand;

        public RemarksService(IRemarksCommands remarksCommand)
        {
            this._remarksCommand = remarksCommand;
        }

        public RemarksRes GetRemarks(SearchFiltersReq req)
        {
            var data = new RemarksRes();

            data = this._remarksCommand.GetRemarks(req);

            return data;
        }
    }
}

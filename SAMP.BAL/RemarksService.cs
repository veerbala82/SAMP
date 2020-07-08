using SAMP.DAL.Interfaces;
using SAMP.Models.Common;
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

        public RemarksRes GetRemarks(RemarksReq req)
        {
            var data = new RemarksRes();

            data = this._remarksCommand.GetRemarks(req);

            return data;
        }
    }
}

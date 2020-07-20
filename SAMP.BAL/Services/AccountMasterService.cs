using SAMP.BAL.Interfaces;
using SAMP.DAL.Interfaces;
using SAMP.Models.AccountMaster;
using SAMP.Models.SearchFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.BAL.Services
{
    public class AccountMasterService: IAccountMasterService
    {
        private readonly IAccountMasterCommands _aMCommand;

        public AccountMasterService(IAccountMasterCommands aMCommand)
        {
            this._aMCommand = aMCommand;
        }

        public AMRes GetAccountMaster(SearchFiltersReq req)
        {
            var data = new AMRes();

            data = _aMCommand.GetAccountMaster(req);

            return data;
        }
    }
}

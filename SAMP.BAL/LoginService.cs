using SAMP.DAL;
using SAMP.Models.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.BAL
{
    public class LoginService: ILoginService
    {
        private readonly ILoginCommands _loginCommand;

        public LoginService(ILoginCommands loginCommand)
        {
            this._loginCommand = loginCommand;
        }

        public LoginRes ValidateLoginCredentials(LoginReq req)
        {
            var data = new LoginRes();

            data = this._loginCommand.ValidateLoginCredentials(req);

            return data;
        }
    }
}

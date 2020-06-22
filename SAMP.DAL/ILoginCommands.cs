using SAMP.Models.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.DAL
{
    public interface ILoginCommands
    {
        LoginRes ValidateLoginCredentials(LoginReq req);
    }
}

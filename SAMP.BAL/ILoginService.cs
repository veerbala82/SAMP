using SAMP.Models.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAMP.BAL
{
    public interface ILoginService
    {       
        LoginRes ValidateLoginCredentials(LoginReq req);
    }
}

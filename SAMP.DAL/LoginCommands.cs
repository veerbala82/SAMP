using Dapper;
using SAMP.Models.Login;
using static System.Data.CommandType;
using System.Data;

namespace SAMP.DAL
{
    public class LoginCommands : BaseCommand, ILoginCommands
    {
        public LoginRes ValidateLoginCredentials(LoginReq req)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@EmailAddress", req.EmailAddress);
            queryParameters.Add("@Password", req.Password);

            queryParameters.Add("@ValidUser", dbType: DbType.Int32, direction: ParameterDirection.Output);
            queryParameters.Add("@Email", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            queryParameters.Add("@FirstName", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            queryParameters.Add("@LastName", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);

            var data = new LoginRes();                        

            SqlMapper.Execute(con, "[dbo].[ValidateLoginCredentials]", queryParameters, commandType: StoredProcedure);

            data.ValidUser = queryParameters.Get<int>("@ValidUser");
            data.Email = queryParameters.Get<string>("@Email");
            data.FirstName = queryParameters.Get<string>("@FirstName");
            data.LastName = queryParameters.Get<string>("@LastName");
            data.Token = string.Empty;

            return data;
        }
    }
}

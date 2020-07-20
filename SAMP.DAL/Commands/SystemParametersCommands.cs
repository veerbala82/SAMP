using Dapper;
using static System.Data.CommandType;
using SAMP.DAL.Interfaces;
using System.Linq;
using System.Collections.Generic;
using SAMP.Models.SystemParameters;
using SAMP.Models.SearchFilters;

namespace SAMP.DAL.Commands
{
    public class SystemParametersCommands : BaseCommand, ISystemParametersCommands
    {
        public SPRes GetSystemParameters(SearchFiltersReq req)
        {
            var length = req.Filters.Count;
            var dynamicWhereCondition = string.Empty;

            for (var i = 0; i < length; i++)
            {
                var filterName = req.Filters[i].FilterName;
                var filterValue = req.Filters[i].FilterValue;
                var filterValueMatch = req.Filters[i].FilterValueMatch;

                if (string.IsNullOrEmpty(filterName))
                    continue;

                dynamicWhereCondition = dynamicWhereCondition + " AND " + "[" + filterName + "]";
                if (filterValueMatch == "Exact")
                {
                    dynamicWhereCondition = dynamicWhereCondition + " = '" + filterValue + "'";
                }
                else if (filterValueMatch == "LikeBefore")
                {
                    dynamicWhereCondition = dynamicWhereCondition + " LIKE '%" + filterValue + "'";
                }
                else if (filterValueMatch == "LikeAfter")
                {
                    dynamicWhereCondition = dynamicWhereCondition + " LIKE '" + filterValue + "%'";
                }
                else if (filterValueMatch == "LikeBoth")
                {
                    dynamicWhereCondition = dynamicWhereCondition + " LIKE '%" + filterValue + "%'";
                }
            }

            var queryParameters = new DynamicParameters();

            queryParameters.Add("@WhereConditions", dynamicWhereCondition);

            var reader = SqlMapper.QueryMultiple(con, "dbo.sp_SystemParametersSelect", queryParameters, commandType: StoredProcedure);

            var SystemParametersTableList = reader.Read<SystemParametersTable>().ToList();

            var aMCount = SystemParametersTableList.Count;

            SPRes SPResponse = new SPRes()
            {
                Response = new Response() { EsSystemParameters = new EsSystemParametersRes() { SystemParameters = new List<SystemParametersRes>(aMCount) } }
            };

            foreach (var item in SystemParametersTableList)
            {
                SPResponse.Response.EsSystemParameters.SystemParameters.Add(
                    new SystemParametersRes
                    {
                        SystemParamId = item.SystemParamId,
                        Code = item.Code,
                        Name = item.Name,
                        Description = item.Description,
                        Active = item.Active
                    });
            }

            var data = SPResponse;

            return data;
        }
    }
}

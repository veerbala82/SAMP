using Dapper;
using static System.Data.CommandType;
using SAMP.DAL.Interfaces;
using System.Linq;
using System.Collections.Generic;
using SAMP.Models.AccountMaster;
using SAMP.Models.SearchFilters;

namespace SAMP.DAL.Commands
{
    public class AccountMasterCommands : BaseCommand, IAccountMasterCommands
    {
        public AMRes GetAccountMaster(SearchFiltersReq req)
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

            var reader = SqlMapper.QueryMultiple(con, "dbo.sp_AccountMasterSelect", queryParameters, commandType: StoredProcedure);

            var AccountMasterTableList = reader.Read<AccountMasterTable>().ToList();

            var aMCount = AccountMasterTableList.Count;

            AMRes aMResponse = new AMRes()
            {
                Response = new Response() { EsAccountMaster = new EsAccountMasterRes() { AccountMaster = new List<AccountMasterRes>(aMCount) } }
            };

            foreach (var item in AccountMasterTableList)
            {
                aMResponse.Response.EsAccountMaster.AccountMaster.Add(
                    new AccountMasterRes
                    {
                        AccountId = item.AccountId,
                        CustomerName = item.CustomerName,
                        CustomerLocation = item.CustomerLocation,
                        SOWAccountName = item.SOWAccountName,
                        DisplayName = item.DisplayName
                    });
            }

            var data = aMResponse;

            return data;
        }
    }
}

﻿using Dapper;
using static System.Data.CommandType;
using System.Data;
using SAMP.Models.Common;
using SAMP.DAL.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using SAMP.Models.SearchFilters;

namespace SAMP.DAL.Commands
{
    public class RemarksCommands : BaseCommand, IRemarksCommands
    {
        public RemarksRes GetRemarks(SearchFiltersReq req)
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

            var reader = SqlMapper.QueryMultiple(con, "dbo.sp_RemarksDetailsSelect", queryParameters, commandType: StoredProcedure);

            var RemarksTableList = reader.Read<RemarksDetailsTable>().ToList();

            var remarksCount = RemarksTableList.Count;

            RemarksRes remarksResponse = new RemarksRes()
            {
                Response = new Response() { EsRemarks = new EsRemarksRes() { RemarksDetails = new List<RemarksDetailRes>(remarksCount) } }
            };

            foreach (var item in RemarksTableList)
            {
                remarksResponse.Response.EsRemarks.RemarksDetails.Add(
                    new RemarksDetailRes
                    {
                        RemarksId = item.RemarksId,
                        RemarksDetails = item.RemarksDetails,
                        Remarksdate = item.Remarksdate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                    });
            }

            var data = remarksResponse;

            return data;
        }
    }
}

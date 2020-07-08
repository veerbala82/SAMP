using Dapper;
using static System.Data.CommandType;
using System.Data;
using SAMP.Models.Common;
using SAMP.DAL.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace SAMP.DAL.Commands
{
    public class RemarksCommands : BaseCommand, IRemarksCommands
    {
        public RemarksRes GetRemarks(RemarksReq req)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@ColumnName", req.Request.EsRemarks.RemarksDetails.ColumnName);
            queryParameters.Add("@ColumnValue", req.Request.EsRemarks.RemarksDetails.ColumnValue);

            var data = new RemarksRes();

            var reader = SqlMapper.QueryMultiple(con, "dbo.sp_RemarksDetailsSelect", queryParameters, commandType: StoredProcedure);

            var RemarksTableList = reader.Read<RemarksDetailsTable>().ToList();

            var remarksCount = RemarksTableList.Count;

            RemarksRes remarksResponse = new RemarksRes()
            {
                Response = new Response() { EsRemarks = new EsRemarks() { Remarks = new List<RemarksDetail>(remarksCount) } }
            };

            foreach (var item in RemarksTableList)
            {
                remarksResponse.Response.EsRemarks.Remarks.Add(
                    new RemarksDetail
                    {
                        RemarksId = item.RemarksId,
                        RemarksDetails = item.RemarksDetails,
                        Remarksdate = item.Remarksdate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                    });
            }

            return data;
        }
    }
}

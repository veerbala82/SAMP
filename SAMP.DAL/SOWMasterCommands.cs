using Dapper;
using SAMP.Models.Login;
using static System.Data.CommandType;
using System.Data;
using SAMP.Models.SOW;
using SAMP.Models.SearchFilters;
using System;
using System.Globalization;

namespace SAMP.DAL
{
    public class SOWMasterCommands : BaseCommand, ISOWMasterCommands
    {
        public SOWSaveRes InsertSOWMaster(SOWReq req, string user)
        {
            //var queryParameters = new DynamicParameters();
            DynamicParameters queryParameters = new DynamicParameters();

            //'SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.' .ToString("yyyy-MM-dd HH:mm:ss")

            //SOWMaster Table
            queryParameters.Add("@SOWNo", req.Request.EsSOWMaster.SOWMasters[0].SOWNo);
            queryParameters.Add("@SOWDesc", req.Request.EsSOWMaster.SOWMasters[0].SOWDesc);
            queryParameters.Add("@StartDate", DateTime.ParseExact(req.Request.EsSOWMaster.SOWMasters[0].SOWStartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
            queryParameters.Add("@EndDate", DateTime.ParseExact(req.Request.EsSOWMaster.SOWMasters[0].SOWEndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
            queryParameters.Add("@PO", req.Request.EsSOWMaster.SOWMasters[0].PO);
            queryParameters.Add("@SOWType_SystemParamId", req.Request.EsSOWMaster.SOWMasters[0].SOWType_SystemParamId);
            queryParameters.Add("@ActionBySPOC", req.Request.EsSOWMaster.SOWMasters[0].ActionBySPOC);
            queryParameters.Add("@SOWAmount", req.Request.EsSOWMaster.SOWMasters[0].SOWAmount);
            queryParameters.Add("@AccountId", req.Request.EsSOWMaster.SOWMasters[0].AccountId);
            queryParameters.Add("@CustMgrId", req.Request.EsSOWMaster.SOWMasters[0].CustMgrId);
            queryParameters.Add("@Status_SystemParamId", req.Request.EsSOWMaster.SOWMasters[0].Status_SystemParamId);            
            queryParameters.Add("@PartialBilliing", req.Request.EsSOWMaster.SOWMasters[0].PartialBilliing);
            queryParameters.Add("@FOC", req.Request.EsSOWMaster.SOWMasters[0].FOC);
            queryParameters.Add("@CreatedUser", user);

            ////ResourceUtilizationDetails Table
            var resourceUtilizationDetailsList = req.Request.EsSOWMaster.SOWMasters[0].ResourceUtilizationDetails;
            var resourceUtilizationDetailsTable = new DataTable();
            resourceUtilizationDetailsTable.Columns.Add("ResourceId", typeof(int));
            resourceUtilizationDetailsTable.Columns.Add("WorkOrderStartDate", typeof(string));
            resourceUtilizationDetailsTable.Columns.Add("WorkOrderEndDate", typeof(string));
            resourceUtilizationDetailsTable.Columns.Add("FoCStartDate", typeof(string));
            resourceUtilizationDetailsTable.Columns.Add("FoCEndDate", typeof(string));

            foreach (var item in resourceUtilizationDetailsList)
            {
                DataRow dr = resourceUtilizationDetailsTable.NewRow();
                dr["ResourceId"] = item.ResourceId;
                dr["WorkOrderStartDate"] = string.IsNullOrEmpty(item.WorkOrderStartDate) ? (object)DBNull.Value : DateTime.ParseExact(item.WorkOrderStartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                dr["WorkOrderEndDate"] = string.IsNullOrEmpty(item.WorkOrderEndDate) ? (object)DBNull.Value : DateTime.ParseExact(item.WorkOrderEndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                dr["FoCStartDate"] = string.IsNullOrEmpty(item.FoCStartDate) ? (object)DBNull.Value : DateTime.ParseExact(item.FoCStartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                dr["FoCEndDate"] = string.IsNullOrEmpty(item.FoCEndDate) ? (object)DBNull.Value : DateTime.ParseExact(item.FoCEndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                resourceUtilizationDetailsTable.Rows.Add(dr);
            }

            queryParameters.Add("@RUD", resourceUtilizationDetailsTable.AsTableValuedParameter("dbo.ResourceUtilizationDetailsType"));

            ////PricingScheduleDetails Table
            var pricingScheduleDetailList = req.Request.EsSOWMaster.SOWMasters[0].PricingScheduleDetails;
            var pricingScheduleDetailTable = new DataTable();
            pricingScheduleDetailTable.Columns.Add("MonthName", typeof(string));
            pricingScheduleDetailTable.Columns.Add("BillableDays", typeof(int));
            pricingScheduleDetailTable.Columns.Add("MilestoneName", typeof(string));
            pricingScheduleDetailTable.Columns.Add("MilestoneValue", typeof(string));

            foreach (var item in pricingScheduleDetailList)
            {
                DataRow dr = pricingScheduleDetailTable.NewRow();
                dr["MonthName"] = item.MonthName;
                dr["BillableDays"] = item.BillableDays;
                dr["MilestoneName"] = item.MilestoneName;
                dr["MilestoneValue"] = item.MilestoneValue;
                pricingScheduleDetailTable.Rows.Add(dr);
            }

            queryParameters.Add("@PSD", pricingScheduleDetailTable.AsTableValuedParameter("dbo.PricingScheduleDetailsType"));

            //Output from SP
            queryParameters.Add("@SavedSOWNo", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);            

            SqlMapper.Execute(con, "dbo.sp_SOWMasterInsert", queryParameters, commandType: StoredProcedure);

            var data = queryParameters.Get<string>("@SavedSOWNo");

            SOWSaveRes saveRes = new SOWSaveRes();

            saveRes.Response = new SaveResponse();

            saveRes.Response.SOWNo = data;
            saveRes.Response.EsErrors = null;

            return saveRes;
        }

        public SOWSearchRes GetSOWs(SearchFiltersReq req)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@FilterName", req.Filters[0].FilterName);
            queryParameters.Add("@FilterValue", req.Filters[0].FilterValue);

            var data = new SOWSearchRes();

            return data;
        }
    }
}

using Dapper;
using SAMP.Models.Login;
using static System.Data.CommandType;
using System.Data;
using SAMP.Models.SOW;
using SAMP.Models.SearchFilters;
using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

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

            SOWSaveRes saveRes = new SOWSaveRes
            {
                Response = new SaveResponse()
            };

            saveRes.Response.SOWNo = data;
            saveRes.Response.EsErrors = null;

            return saveRes;
        }

        public SOWSearchResponse GetSOWs(SearchFiltersReq req)
        {
            var length = req.Filters.Count;
            var dynamicWhereCondition = string.Empty;

            //Testing Code
            //int ii = 1, ji = 0;
            //var result = ii / ji;

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

            var reader = SqlMapper.QueryMultiple(con, "dbo.sp_SOWMasterSelect", queryParameters, commandType: StoredProcedure);

            var SOWMastersTableList = reader.Read<SOWMasterTable>().ToList();
            var ResourceUtilizationDetailsTableList = reader.Read<ResourceUtilizationDetailsTable>().ToList();
            var PricingScheduleDetailsTableList = reader.Read<PricingScheduleDetailsTable>().ToList();

            var sowCount = SOWMastersTableList.Count;

            SOWSearchResponse sOWSearchResponse = new SOWSearchResponse()
            {
                Request = new ResponseSR() { EsSOWMaster = new EsSOWMasterSR() { SOWMasters = new List<SOWMasterSR>(sowCount) } }
            };

            var listRow = 0;
            foreach (var item in SOWMastersTableList)
            {
                sOWSearchResponse.Request.EsSOWMaster.SOWMasters.Add(
                    new SOWMasterSR
                    {
                        SOWId = item.SOWId,
                        SowNo = item.SOWNo,
                        SowDesc = item.SOWDesc,
                        SOWStartDate = item.StartDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                        SOWEndDate = item.EndDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                        PO = item.PO,
                        SOWType_SystemParamId = item.SOWType_SystemParamId,
                        ActionBySPOC = item.ActionBySPOC,
                        SOWAmount = item.SOWAmount,
                        AccountId = item.AccountId,
                        CustMgrId = item.CustMgrId,
                        Status_SystemParamId = item.Status_SystemParamId,
                        PartialBilliing = item.PartialBilliing,
                        FOC = item.FOC
                    });

                sOWSearchResponse.Request.EsSOWMaster.SOWMasters[listRow].ResourceUtilizationDetails = new List<ResourceUtilizationDetailSearchResponse>();

                foreach (var RUD in ResourceUtilizationDetailsTableList)
                {
                    if (item.SOWId == RUD.SOWId)
                    {
                        sOWSearchResponse.Request.EsSOWMaster.SOWMasters[listRow].ResourceUtilizationDetails.Add(
                            new ResourceUtilizationDetailSearchResponse
                            {
                                ResourceUtilizationId = RUD.ResourceUtilizationId,
                                SOWId = RUD.SOWId,
                                ResourceId = RUD.ResourceId,
                                WorkOrderStartDate = RUD.WorkOrderStartDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                                WorkOrderEndDate = RUD.WorkOrderEndDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                                BenchStartDate = RUD.BenchStartDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                                BenchEndDate = RUD.BenchEndDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                                FoCStartDate = RUD.FoCStartDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                                FoCEndDate = RUD.FoCEndDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                            });
                    }
                }

                sOWSearchResponse.Request.EsSOWMaster.SOWMasters[listRow].PricingScheduleDetails = new List<PricingScheduleDetailSearchResponse>();

                foreach (var PSD in PricingScheduleDetailsTableList)
                {
                    if (item.SOWId == PSD.SOWId)
                    {
                        sOWSearchResponse.Request.EsSOWMaster.SOWMasters[listRow].PricingScheduleDetails.Add(
                            new PricingScheduleDetailSearchResponse
                            {
                                PricingSchId = PSD.PricingSchId,
                                SOWId = PSD.SOWId,
                                MonthName = PSD.MonthName,
                                BillableDays = PSD.BillableDays,
                                MilestoneName = PSD.MilestoneName,
                                MilestoneValue = PSD.MilestoneValue,
                                RevisionNumber = PSD.RevisionNumber,
                                RevisionDate = PSD.RevisionDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                            });
                    }
                }

                listRow++;
            }

            var data = sOWSearchResponse;

            return data;
        }
    }
}

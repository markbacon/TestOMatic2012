using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {

	enum SqlTemplateId {

		CheckIfCouponRecordsExist,
		DeleteBrinkOrder,
		DeleteBrinkOrderData,
		DeleteEpassDepositDetail,
		DeleteOrder,
		DeleteFrachiseTimeCardRecords,
		ExecuteCashOverShortUpdate,
		Get2ferSales,
		GetActiveInventoryItems,
		GetBadFiles,
		GetBrinkGiftCardOrders,
		GetBrinkMenuMixSold,
		GetBrinkNetSales,
		GetBrinkPaidInOuts,
		GetCaCorporateUnitNumbersByEffDate,
		GetCalendarDayNetSales,
		GetCarlsCompanyStores,
		GetCarlsStarPosUnitList,
		GetCarlsValidation,
		GetCkeTimePollEmployee,
		GetCkeTimePollSsnDataList,
		GetColumns,
		GetCouponId,
		GetCrunchTimeInvoices,
		GetCrunchTimeManagerTimeCardData,
		GetCrunchTimeMenuMixSold,
		GetCrunchTimeNetSales,
		GetCrunchTimePunchData,
		GetCrunchTimeSalesData,
		GetCrunchTimeShamrockProducts,
		GetCrunchTimeTimeCardData,
		GetCT_EmployeePunchData,
		GetDailySalesTotal,
		GetDepositDetail,
		GetDepositDtlDim,
        GetDonationsForBrinkUnits,
        GetDriveThruTimerReportData,
		GetDuplicateCostWarehouseXref,
		GetDuplicateOrderId,
		GetDuplicateOrderIdFields,
		GetDuplicateOrders,
		GetEmplOutData,
		GetEmployeeData,
		GetEmployeePunchData,
		GetEpassDepositDetail,
		GetEpassPaidInOuts,
		GetEpassShamrockProducts,
		GetEpassTimeCardData,
		GetExistingDeposits,
		GetFuseboxReconDetail,
		GetGMTimeCardHeaderData,
		GetHardeesStarPosUnitList,
		GetHardeesValidation,
		GetIdealUsageJrList,
		GetInternationalUnitNumbers,
		GetInvalidEmployees,
		GetLegacyTimeClockData,
		GetMbmRcptDetailWithNegativeCost,
		GetMissingBrinkData,
		GetMissingBrinkUnitDates,
		GetMissingUnitDates,
		GetNewNewYorkItems,
		GetOpenCarlsUnits,
		GetOrderIdField,
		GetOrderItem,
		GetOrderItemModifier,
		GetPosFactId,
		GetPosOrder,
		GetProductionDeposits,
		GetProductsForMenuItem,
		GetprojectXReceipts,
		GetRbiUnitSales,
		GetRedNetSalesSummaries,
		GetScannedCouponDates,
		GetScannedCouponUnitData,
		GetScannedCouponUnits,
		GetStarPosGiftCardOrders,
		GetTimeCardData,
		GetTimeCardReportWeekEndCount,
		GetTransponderSales,
		GetTrecsDetailData,
		GetUnitData,
		GetUnitMasterData,
		GetUnitsWithDeletedDeposits,
		GetUnitWeekEndDayFromRED,
		InsertBrinkUnitTemplate,
		InsertDepositDtlFact,
		InsertEmployeeClock,
		InsertEpassCouponDetailFactRecord,
		InsertOrderItem,
		InsertOrderItemModifier,
		InsertPosOrder,
		InsertPsEmployeeData,
		InsertTimeFileData,
        UpdateBrinkCashOverShort,
        UpdateCkeVenorXref,
		UpdateDstOrder,
		UpdateElavonUnitData,
		UpdateEmployeeId,
		UpdateMbmDtlCost,
		UpdateOrderTimes,
		UpdatePosFact,
		UpdateRecipeDetail
	}

	class SqlTemplateBroker {

		public static string Load(SqlTemplateId templateId) {

			string templateName = "";

			switch (templateId) {

				case SqlTemplateId.CheckIfCouponRecordsExist:
					templateName = "TestOMatic2012.SQL.CheckIfCouponRecordsExist.sql";
					break;

				case SqlTemplateId.DeleteBrinkOrder:
					templateName = "TestOMatic2012.SQL.DeleteBrinkOrder.sql";
					break;

				case SqlTemplateId.DeleteBrinkOrderData:
					templateName = "TestOMatic2012.SQL.DeleteBrinkOrderData.sql";
					break;

				case SqlTemplateId.DeleteEpassDepositDetail:
					templateName = "TestOMatic2012.SQL.DeleteEpassDepositDetail.sql";
					break;

				case SqlTemplateId.DeleteFrachiseTimeCardRecords:
					templateName = "TestOMatic2012.SQL.DeleteFrachiseTimeCardRecords.sql";
					break;

				case SqlTemplateId.DeleteOrder:
					templateName = "TestOMatic2012.SQL.DeleteOrder.sql";
					break;

				case SqlTemplateId.ExecuteCashOverShortUpdate:
					templateName = "TestOMatic2012.SQL.ExecuteCashOverShortUpdate.sql";
					break;

				case SqlTemplateId.Get2ferSales:
					templateName = "TestOMatic2012.SQL.Get2ferSales.sql";
					break;

				case SqlTemplateId.GetActiveInventoryItems:
					templateName = "TestOMatic2012.SQL.GetActiveInventoryItems.sql";
					break;

				case SqlTemplateId.GetBrinkGiftCardOrders:
					templateName = "TestOMatic2012.SQL.GetBrinkGiftCardOrders.sql";
					break;

				case SqlTemplateId.GetBadFiles:
					templateName = "TestOMatic2012.SQL.GetBadFiles.sql";
					break;

				case SqlTemplateId.GetBrinkMenuMixSold:
					templateName = "TestOMatic2012.SQL.GetBrinkMenuMixSold.sql";
					break;

				case SqlTemplateId.GetBrinkPaidInOuts:
					templateName = "TestOMatic2012.SQL.GetBrinkPaidInOuts.sql";
					break;

				case SqlTemplateId.GetBrinkNetSales:
					templateName = "TestOMatic2012.SQL.GetBrinkNetSales.sql";
					break;

				case SqlTemplateId.GetCaCorporateUnitNumbersByEffDate:
					templateName = "TestOMatic2012.SQL.GetCaCorporateUnitNumbersByEffDate.sql";
					break;

				case SqlTemplateId.GetCalendarDayNetSales:
					templateName = "TestOMatic2012.SQL.GetCalendarDayNetSales.sql";
					break;

				case SqlTemplateId.GetCarlsCompanyStores:
					templateName = "TestOMatic2012.SQL.GetCarlsCompanyStores.sql";
					break;

				case SqlTemplateId.GetCarlsStarPosUnitList:
					templateName = "TestOMatic2012.SQL.GetCarlsStarPosUnitList.sql";
					break;

				case SqlTemplateId.GetCarlsValidation:
					templateName = "TestOMatic2012.SQL.GetCarlsValidation.sql";
					break;


				case SqlTemplateId.GetCkeTimePollEmployee:
					templateName = "TestOMatic2012.SQL.GetCkeTimePollEmployee.sql";
					break;

				case SqlTemplateId.GetCkeTimePollSsnDataList:
					templateName = "TestOMatic2012.SQL.GetCkeTimePollSsnDataList.sql";
					break;

				case SqlTemplateId.GetColumns:
					templateName = "TestOMatic2012.SQL.GetColumns.sql";
					break;

				case SqlTemplateId.GetCouponId:
					templateName = "TestOMatic2012.SQL.GetCouponId.sql";
					break;

				case SqlTemplateId.GetCrunchTimeInvoices:
					templateName = "TestOMatic2012.SQL.GetCrunchTimeInvoices.sql";
					break;

				case SqlTemplateId.GetCrunchTimeManagerTimeCardData:
					templateName = "TestOMatic2012.SQL.GetCrunchTimeManagerTimeCardData.sql";
					break;

				case SqlTemplateId.GetCrunchTimeMenuMixSold:
					templateName = "TestOMatic2012.SQL.GetCrunchTimeMenuMixSold.sql";
					break;

				case SqlTemplateId.GetCrunchTimeNetSales:
					templateName = "TestOMatic2012.SQL.GetCrunchTimeNetSales.sql";
					break;

				case SqlTemplateId.GetCrunchTimeShamrockProducts:
					templateName = "TestOMatic2012.SQL.GetCrunchTimeShamrockProducts.sql";
					break;

				case SqlTemplateId.GetCrunchTimePunchData:
					templateName = "TestOMatic2012.SQL.GetCrunchTimePunchData.sql";
					break;

				case SqlTemplateId.GetCrunchTimeSalesData:
					templateName = "TestOMatic2012.SQL.GetCrunchTimeSalesData.sql";
					break;

				case SqlTemplateId.GetCrunchTimeTimeCardData:
					templateName = "TestOMatic2012.SQL.GetCrunchTimeTimeCardData.sql";
					break;


				case SqlTemplateId.GetCT_EmployeePunchData:
					templateName = "TestOMatic2012.SQL.GetCT_EmployeePunchData.sql";
					break;

				case SqlTemplateId.GetDailySalesTotal:
					templateName = "TestOMatic2012.SQL.GetDailySalesTotal.sql";
					break;

				case SqlTemplateId.GetDepositDetail:
					templateName = "TestOMatic2012.SQL.GetDepositDetail.sql";
					break;

				case SqlTemplateId.GetDepositDtlDim:
                    templateName = "TestOMatic2012.SQL.GetDepositDtlDim.sql";
                    break;

                case SqlTemplateId.GetDonationsForBrinkUnits:
                    templateName = "TestOMatic2012.SQL.GetDonationsForBrinkUnits.sql";
                    break;

                case SqlTemplateId.GetDriveThruTimerReportData:
                    templateName = "TestOMatic2012.SQL.GetDriveThruTimerReportData.sql";
                    break;

                case SqlTemplateId.GetDuplicateCostWarehouseXref:
					templateName = "TestOMatic2012.SQL.GetDuplicateCostWarehouseXref.sql";
					break;

				case SqlTemplateId.GetDuplicateOrderId:
					templateName = "TestOMatic2012.SQL.GetDuplicateOrderId.sql";
					break;

				case SqlTemplateId.GetDuplicateOrderIdFields:
					templateName = "TestOMatic2012.SQL.GetDuplicateOrderIdFields.sql";
					break;

				case SqlTemplateId.GetDuplicateOrders:
					templateName = "TestOMatic2012.SQL.GetDuplicateOrders.sql";
					break;

				case SqlTemplateId.GetEmplOutData:
					templateName = "TestOMatic2012.SQL.GetEmplOutData.sql";
					break;

				case SqlTemplateId.GetEmployeeData:
					templateName = "TestOMatic2012.SQL.GetEmployeeData.sql";
					break;

				case SqlTemplateId.GetEmployeePunchData:
					templateName = "TestOMatic2012.SQL.GetEmployeePunchData.sql";
					break;

				case SqlTemplateId.GetEpassDepositDetail:
					templateName = "TestOMatic2012.SQL.GetEpassDepositDetail.sql";
					break;

				case SqlTemplateId.GetEpassPaidInOuts:
					templateName = "TestOMatic2012.SQL.GetEpassPaidInOuts.sql";
					break;

				case SqlTemplateId.GetEpassShamrockProducts:
					templateName = "TestOMatic2012.SQL.GetEpassShamrockProducts.sql";
					break;

				case SqlTemplateId.GetEpassTimeCardData:
					templateName = "TestOMatic2012.SQL.GetEpassTimeCardData.sql";
					break;

				case SqlTemplateId.GetExistingDeposits:
					templateName = "TestOMatic2012.SQL.GetExistingDeposits.sql";
					break;

				case SqlTemplateId.GetFuseboxReconDetail:
					templateName = "TestOMatic2012.SQL.GetFuseboxReconDetail.sql";
					break;

				case SqlTemplateId.GetGMTimeCardHeaderData:
					templateName = "TestOMatic2012.SQL.GetGMTimeCardHeaderData.sql";
					break;

				case SqlTemplateId.GetHardeesStarPosUnitList:
					templateName = "TestOMatic2012.SQL.GetHardeesStarPosUnitList.sql";
					break;

				case SqlTemplateId.GetHardeesValidation:
					templateName = "TestOMatic2012.SQL.GetHardeesValidation.sql";
					break;

				case SqlTemplateId.GetIdealUsageJrList:
					templateName = "TestOMatic2012.SQL.GetIdealUsageJrList.sql";
					break;

				case SqlTemplateId.GetInternationalUnitNumbers:
					templateName = "TestOMatic2012.SQL.GetInternationalUnitNumbers.sql";
					break;

				case SqlTemplateId.GetInvalidEmployees:
					templateName = "TestOMatic2012.SQL.GetInvalidEmployees.sql";
					break;

				case SqlTemplateId.GetMbmRcptDetailWithNegativeCost:
					templateName = "TestOMatic2012.SQL.GetMbmRcptDetailWithNegativeCost.sql";
					break;

				case SqlTemplateId.GetMissingBrinkData:
					templateName = "TestOMatic2012.SQL.GetMissingBrinkData.sql";
					break;

				case SqlTemplateId.GetMissingBrinkUnitDates:
					templateName = "TestOMatic2012.SQL.GetMissingBrinkUnitDates.sql";
					break;

				case SqlTemplateId.GetMissingUnitDates:
					templateName = "TestOMatic2012.SQL.GetMissingUnitDates.sql";
					break;

				case SqlTemplateId.GetNewNewYorkItems:
					templateName = "TestOMatic2012.SQL.GetNewNewYorkItems.sql";
					break;

				case SqlTemplateId.GetOpenCarlsUnits:
					templateName = "TestOMatic2012.SQL.GetOpenCarlsUnits.sql";
					break;

				case SqlTemplateId.GetOrderIdField:
					templateName = "TestOMatic2012.SQL.GetOrderIdField.sql";
					break;

				case SqlTemplateId.GetOrderItem:
					templateName = "TestOMatic2012.SQL.GetOrderItem.sql";
					break;

				case SqlTemplateId.GetOrderItemModifier:
					templateName = "TestOMatic2012.SQL.GetOrderItemModifier.sql";
					break;


				case SqlTemplateId.GetPosFactId:
					templateName = "TestOMatic2012.SQL.GetPosFactId.sql";
					break;

				case SqlTemplateId.GetPosOrder:
					templateName = "TestOMatic2012.SQL.GetPosOrder.sql";
					break;

				case SqlTemplateId.GetProductionDeposits:
					templateName = "TestOMatic2012.SQL.GetProductionDeposits.sql";
					break;

				case SqlTemplateId.GetProductsForMenuItem:
					templateName = "TestOMatic2012.SQL.GetProductsForMenuItem.sql";
					break;

				case SqlTemplateId.GetprojectXReceipts:
					templateName = "TestOMatic2012.SQL.GetProjectXReceipts.sql";
					break;

				case SqlTemplateId.GetRbiUnitSales:
					templateName = "TestOMatic2012.SQL.GetRbiUnitSales.sql";
					break;

				case SqlTemplateId.GetRedNetSalesSummaries:
					templateName = "TestOMatic2012.SQL.GetRedNetSalesSummaries.sql";
					break;

				case SqlTemplateId.GetScannedCouponDates:
					templateName = "TestOMatic2012.SQL.GetScannedCouponDates.sql";
					break;

				case SqlTemplateId.GetScannedCouponUnitData:
					templateName = "TestOMatic2012.SQL.GetScannedCouponUnitData.sql";
					break;

				case SqlTemplateId.GetScannedCouponUnits:
					templateName = "TestOMatic2012.SQL.GetScannedCouponUnits.sql";
					break;

				case SqlTemplateId.GetStarPosGiftCardOrders:
					templateName = "TestOMatic2012.SQL.GetStarPosGiftCardOrders.sql";
					break;


				case SqlTemplateId.GetTimeCardData:
					templateName = "TestOMatic2012.SQL.GetTimeCardData.sql";
					break;

				case SqlTemplateId.GetTimeCardReportWeekEndCount:
					templateName = "TestOMatic2012.SQL.GetTimeCardReportWeekEndCount.sql";
					break;

				case SqlTemplateId.GetTransponderSales:
					templateName = "TestOMatic2012.SQL.GetTransponderSales.sql";
					break;

				case SqlTemplateId.GetTrecsDetailData:
					templateName = "TestOMatic2012.SQL.GetTrecsDetailData.sql";
					break;


				case SqlTemplateId.GetUnitData:
					templateName = "TestOMatic2012.SQL.GetUnitData.sql";
					break;

				case SqlTemplateId.GetUnitMasterData:
					templateName = "TestOMatic2012.SQL.GetUnitMasterData.sql";
					break;

				case SqlTemplateId.GetUnitsWithDeletedDeposits:
					templateName = "TestOMatic2012.SQL.GetUnitsWithDeletedDeposits.sql";
					break;

				case SqlTemplateId.GetUnitWeekEndDayFromRED:
					templateName = "TestOMatic2012.SQL.GetUnitWeekEndDayFromRED.sql";
					break;

				case SqlTemplateId.GetLegacyTimeClockData:
					templateName = "TestOMatic2012.SQL.GetLegacyTimeClockData.sql";
					break;

				case SqlTemplateId.InsertBrinkUnitTemplate:
					templateName = "TestOMatic2012.SQL.InsertBrinkUnitTemplate.sql";
					break;

				case SqlTemplateId.InsertDepositDtlFact:
					templateName = "TestOMatic2012.SQL.InsertDepositDetailFact.sql";
					break;

				case SqlTemplateId.InsertEmployeeClock:
					templateName = "TestOMatic2012.SQL.InsertEmployeeClock.sql";
					break;

				case SqlTemplateId.InsertEpassCouponDetailFactRecord:
					templateName = "TestOMatic2012.SQL.InsertEpassCouponDetailFactRecord.sql";
					break;

				case SqlTemplateId.InsertOrderItem:
					templateName = "TestOMatic2012.SQL.InsertOrderItem.sql";
					break;
				case SqlTemplateId.InsertOrderItemModifier:
					templateName = "TestOMatic2012.SQL.InsertOrderItemModifier.sql";
					break;

				case SqlTemplateId.InsertPosOrder:
					templateName = "TestOMatic2012.SQL.InsertPosOrder.sql";
					break;

				case SqlTemplateId.InsertPsEmployeeData:
					templateName = "TestOMatic2012.SQL.InsertPsEmployeeData.sql";
					break;

				case SqlTemplateId.InsertTimeFileData:
					templateName = "TestOMatic2012.SQL.InsertTimeFileData.sql";
					break;

                case SqlTemplateId.UpdateBrinkCashOverShort:
                    templateName = "TestOMatic2012.SQL.UpdateBrinkCashOverShort.sql";
                    break;

                case SqlTemplateId.UpdateCkeVenorXref:
                    templateName = "TestOMatic2012.SQL.UpdateCkeVendorXref.sql";
                    break;

				case SqlTemplateId.UpdateDstOrder:
					templateName = "TestOMatic2012.SQL.UpdateDstOrder.sql";
					break;

				case SqlTemplateId.UpdateElavonUnitData:
					templateName = "TestOMatic2012.SQL.UpdateElavonUnitData.sql";
					break;

				case SqlTemplateId.UpdateEmployeeId:
					templateName = "TestOMatic2012.SQL.UpdateEmployeeId.sql";
					break;

				case SqlTemplateId.UpdateMbmDtlCost:
					templateName = "TestOMatic2012.SQL.UpdateMbmDtlCost.sql";
					break;

				case SqlTemplateId.UpdateOrderTimes:
					templateName = "TestOMatic2012.SQL.UpdateOrderTimes.sql";
					break;

				case SqlTemplateId.UpdatePosFact:
					templateName = "TestOMatic2012.SQL.UpdatePosFact.sql";
					break;

				case SqlTemplateId.UpdateRecipeDetail:
					templateName = "TestOMatic2012.SQL.UpdateRecipeDetail.sql";
					break;

			}

			return LoadTemplate(templateName);
		}
		//---------------------------------------------------------------------------------------------------
		private static string LoadTemplate(string templateName) {

			string template = "";

			System.Reflection.Assembly thisExe = System.Reflection.Assembly.GetExecutingAssembly();

			Stream s = thisExe.GetManifestResourceStream(templateName);

			using (StreamReader sr = new StreamReader(s)) {

				template = sr.ReadToEnd();
			}

			return template;
		}
	}
}

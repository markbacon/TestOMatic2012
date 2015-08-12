using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {

    enum SqlTemplateId {

		CheckIfCouponRecordsExist,
		DeleteEpassDepositDetail,
		DeleteOrder,
		GetCarlsCompanyStores,
		GetCarlsValidation,
		GetCkeTimePollEmployee,
		GetCkeTimePollSsnDataList,
		GetCouponId,
		GetDepositDetail,
		GetDepositDtlDim,
		GetDuplicateOrderId,
        GetDuplicateOrders,
		GetEmployeeData,
		GetEpassDepositDetail,
		GetHardeesValidation,
		GetIdealUsageJrList,
		GetMbmRcptDetailWithNegativeCost,
		GetOrderItem,
		GetOrderItemModifier,
		GetPosFactId,
		GetPosOrder,
		GetProductsForMenuItem,
		GetScannedCouponDates,
		GetScannedCouponUnitData,
		GetScannedCouponUnits,
		GetUnitsWithDeletedDeposits,
		GetLegacyTimeClockData,
		InsertDepositDtlFact,
		InsertEmployeeClock,
		InsertOrderItem,
		InsertOrderItemModifier,
		InsertPosOrder,
		InsertEpassCouponDetailFactRecord,
		UpdateMbmDtlCost,
		UpdatePosFact
}

    class SqlTemplateBroker {

        public static string Load(SqlTemplateId templateId) {

            string templateName = "";

            switch (templateId) {

				case SqlTemplateId.CheckIfCouponRecordsExist:
					templateName = "TestOMatic2012.SQL.CheckIfCouponRecordsExist.sql";
					break;

				case SqlTemplateId.DeleteEpassDepositDetail:
					templateName = "TestOMatic2012.SQL.DeleteEpassDepositDetail.sql";
					break;

				case SqlTemplateId.DeleteOrder:
					templateName = "TestOMatic2012.SQL.DeleteOrder.sql";
					break;

				case SqlTemplateId.GetCarlsCompanyStores:
					templateName = "TestOMatic2012.SQL.GetCarlsCompanyStores.sql";
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

				case SqlTemplateId.GetCouponId:
					templateName = "TestOMatic2012.SQL.GetCouponId.sql";
					break;

				case SqlTemplateId.GetDepositDetail:
					templateName = "TestOMatic2012.SQL.GetDepositDetail.sql";
					break;

				case SqlTemplateId.GetDepositDtlDim:
					templateName = "TestOMatic2012.SQL.GetDepositDtlDim.sql";
					break;

				case SqlTemplateId.GetDuplicateOrderId:
					templateName = "TestOMatic2012.SQL.GetDuplicateOrderId.sql";
					break;

				case SqlTemplateId.GetDuplicateOrders:
					templateName = "TestOMatic2012.SQL.GetDuplicateOrders.sql";
					break;

				case SqlTemplateId.GetEmployeeData:
					templateName = "TestOMatic2012.SQL.GetEmployeeData.sql";
					break;

				case SqlTemplateId.GetEpassDepositDetail:
					templateName = "TestOMatic2012.SQL.GetEpassDepositDetail.sql";
					break;

				case SqlTemplateId.GetHardeesValidation:
					templateName = "TestOMatic2012.SQL.GetHardeesValidation.sql";
					break;
					
				case SqlTemplateId.GetIdealUsageJrList:
					templateName = "TestOMatic2012.SQL.GetIdealUsageJrList.sql";
					break;

				case SqlTemplateId.GetMbmRcptDetailWithNegativeCost:
					templateName = "TestOMatic2012.SQL.GetMbmRcptDetailWithNegativeCost.sql";
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

				case SqlTemplateId.GetProductsForMenuItem:
					templateName = "TestOMatic2012.SQL.GetProductsForMenuItem.sql";
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

				case SqlTemplateId.GetUnitsWithDeletedDeposits:
					templateName = "TestOMatic2012.SQL.GetUnitsWithDeletedDeposits.sql";
					break;

				case SqlTemplateId.GetLegacyTimeClockData:
					templateName = "TestOMatic2012.SQL.GetLegacyTimeClockData.sql";
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

				case SqlTemplateId.UpdateMbmDtlCost:
					templateName = "TestOMatic2012.SQL.UpdateMbmDtlCost.sql";
					break;

				case SqlTemplateId.UpdatePosFact:
					templateName = "TestOMatic2012.SQL.UpdatePosFact.sql";
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

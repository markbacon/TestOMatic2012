using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {
	
	class DailyInventorySummary {

		public void Run(DateTime businessDate) {

			DateTime lastWeekEndDate = GetLastWeekEndDate(businessDate);

			_inventoryDetailList = GetActiveInventoryItems();






		}
		//---------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------
		private ImsCkeData _data = new ImsCkeData();
		private List<InventoryDetail> _inventoryDetailList;

		private List<InventoryDetail> GetActiveInventoryItems() {

			List<InventoryDetail> inventoryDetailList = new List<InventoryDetail>();

			DataTable dt = _data.GetActiveInventoryItems();

			foreach (DataRow dr in dt.Rows) {

				InventoryDetail isd = new InventoryDetail();
				isd.ItemNumber = dr["ItemNumber"].ToString();
				isd.ClassCode = dr["ClassCode"].ToString();
				isd.ActualConversionFactor = Convert.ToDecimal(dr["ActualConversionFactor"]);
				isd.IdealConversionFactor = Convert.ToDecimal(dr["IdealConversionFactor"]);

				inventoryDetailList.Add(isd);
			}

			return inventoryDetailList;
		}
		//---------------------------------------------------------------------------------------------------
		private DateTime GetLastWeekEndDate(DateTime businessDate) {

			DayOfWeek weekEndDayOfWeek = GetWeekEndDayOfWeek();
			
			DateTime weekEndDate = businessDate;

			while (weekEndDate.DayOfWeek != weekEndDayOfWeek) {
				weekEndDate = weekEndDate.AddDays(-1);
			}

			return weekEndDate;
		}
		//---------------------------------------------------------------------------------------------------
		private DayOfWeek GetWeekEndDayOfWeek() {

			//-- TODO Add logic to select the franchisees day of week
			return DayOfWeek.Monday;
		}
	}
}

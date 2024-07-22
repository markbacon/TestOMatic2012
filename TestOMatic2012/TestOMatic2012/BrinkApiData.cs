using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	class BrinkApiData {


		public bool DeleteBrinkOrder(int brinkOrderId) {

			try {
				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("DeleteBrinkOrder starting for BrinkOrderId: " + brinkOrderId.ToString());


				StringBuilder sb = new StringBuilder();
				sb.Append(SqlTemplateBroker.Load(SqlTemplateId.DeleteBrinkOrder));

				sb.Replace("!BRINK_ORDER_ID", brinkOrderId.ToString());

				DataAccess dac = new DataAccess(AppSettings.BrinkApiDataConnectionString);
				int rowsAffected = dac.ExecuteActionQuery(sb.ToString());


				Logger.Write("DeleteBrinkOrder has compled. Rows affected: " + rowsAffected.ToString() + ". Elapsed time: " + sw1.Elapsed.ToString());

				return true;
			}
			catch (Exception ex) {
				Logger.Write("An exception occurred in DeleteBrinkOrder.  Please see error log for details.");
				Logger.WriteError(ex);

				return false;
			}
		}



		public int GetBrinkOrderId(long idField) {

			using (BrinkApiDataEntities dataEntities = new BrinkApiDataEntities()) {

				return dataEntities.BrinkOrders.Where(o => o.IdField == idField).OrderByDescending(o => o.BrinkOrderId).Select(o => o.BrinkOrderId).FirstOrDefault();
			}

		}

		public DataTable GetBrinkNetSales(DateTime businessDate) {

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetBrinkNetSales));

			sb.Replace("!BUSINESS_DATE", businessDate.ToString("yyyy-MM-dd"));

			DataAccess dac = new DataAccess(AppSettings.BrinkApiDataConnectionString);

			DataTable dt = dac.ExecuteQuery(sb.ToString());

			return dt;
		}

		public decimal GetBrinkNetSales(string unitNumber, DateTime businessDate) {

			decimal netSales = 0;

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetBrinkNetSales));


			sb.Replace("!UNIT_NUMBER", unitNumber);
			sb.Replace("!BUSINESS_DATE", businessDate.ToString("yyyy-MM-dd"));

			DataAccess dac = new DataAccess(AppSettings.BrinkApiDataConnectionString);

			DataTable dt = dac.ExecuteQuery(sb.ToString());

			if (dt.Rows.Count > 0) {
				netSales = Convert.ToDecimal(dt.Rows[0]["netSales"]);
			}

			return netSales;
		}


		public List<BrinkOrder> GetBrinkOrders(string unitNumber, DateTime businessDate) {

			using (BrinkApiDataEntities dataEntities = new BrinkApiDataEntities()) {

				return dataEntities.BrinkOrders.Where(u => u.UnitNumber == unitNumber && u.BusinessDate == businessDate).ToList();
			}



		}
		public List<MenuItemPricing> GetMenuItemPricingListProd(string unitNumber) {

			using (BrinkApiDataEntities dataEntities = new BrinkApiDataEntities()) {
				dataEntities.Database.Connection.ConnectionString = "data source=skysql03;initial catalog=BrinkApiData;persist security info=True;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework";

				return dataEntities.MenuItemPricings.Where(m => m.UnitNumber == unitNumber).ToList();
			}
		}

		public List<MenuItemPricing> GetMenuItemPricingList(string unitNumber) {

			using (BrinkApiDataEntities dataEntities = new BrinkApiDataEntities()) {
				return dataEntities.MenuItemPricings.Where(m => m.UnitNumber == unitNumber).ToList();
			}
		}

		public BrinkPromo GetBrinkPromo(string promoName) {

			using (BrinkApiDataEntities dataEntities = new BrinkApiDataEntities()) {

				return dataEntities.BrinkPromoes.Where(p => p.Name == promoName).FirstOrDefault();
			}
		}

		public void GetTransponderSales(string unitNumber, DateTime businessDate, out decimal amount, out int quantity) {

			quantity = 0;
			amount = 0;

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetTransponderSales));

			sb.Replace("!UNIT_NUMBER", unitNumber);
			sb.Replace("!BUSINESS_DATE", businessDate.ToString("yyyy-MM-dd"));

			DataAccess dac = new DataAccess(AppSettings.BrinkApiDataConnectionString);

			DataTable dt = dac.ExecuteQuery(sb.ToString());

			if (dt.Rows.Count > 0) {
				amount = Convert.ToDecimal(dt.Rows[0]["NetSales"]);
				quantity = Convert.ToInt32(dt.Rows[0]["QuantitySold"]);
			}
		}

		public BrinkUnit GetBrinkUnit(string unitNumber) {

			using (BrinkApiDataEntities dataEntities = new BrinkApiDataEntities()) {

				return dataEntities.BrinkUnits.Where(b => b.UnitNumber == unitNumber).SingleOrDefault();
			}

		}

		public List<BrinkUnit> GetBrinkUnits() {

			using (BrinkApiDataEntities dataEntities = new BrinkApiDataEntities()) {

				return dataEntities.BrinkUnits.ToList();
			}

		}


		public List<string> GetBrinkUnitList() {

			using (BrinkApiDataEntities dataEntities = new BrinkApiDataEntities()) {

				return dataEntities.BrinkUnits.Select(b => b.UnitNumber).ToList();
			}
		}


		public List<CalendarDayNetSales> GetCalendarDayNetSales(DateTime businessDate) {

			List<CalendarDayNetSales> netSalesList = new List<CalendarDayNetSales>();

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetCalendarDayNetSales));

			sb.Replace("!BUSINESS_DATE", businessDate.ToString("yyyy-MM-dd"));

			DataAccess dac = new DataAccess(AppSettings.BrinkApiDataConnectionString);

			DataTable dt = dac.ExecuteQuery(sb.ToString());

			foreach (DataRow dr in dt.Rows) {

				CalendarDayNetSales netSales = new CalendarDayNetSales() {
					BusinessDate = Convert.ToDateTime(dr["BusinessDate"]),
					NetSales = Convert.ToDecimal(dr["NetSales"]),
					UnitNumber = dr["UnitNumber"].ToString()
				};

				netSalesList.Add(netSales);

			}


			return netSalesList;

		}

	}
}

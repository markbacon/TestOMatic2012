using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	class DataAnalysisData {

		public List<DateTime> GetScannedCouponDates(string unitNumber) {

			List<DateTime> scannedCouponDates = new List<DateTime>();

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetScannedCouponDates));
			sb.Replace("!RESTAURANT_NO", unitNumber);


			DataAccess dac = new DataAccess(AppSettings.DataAnalysisConnectionString);
			DataTable dt = dac.ExecuteQuery(sb.ToString());

			foreach (DataRow dr in dt.Rows) {

				scannedCouponDates.Add(Convert.ToDateTime(dr["BusinessDates"]));
			}

			return scannedCouponDates;
		}
		//---------------------------------------------------------------------------------------------------------
		public DataTable GetScannedCouponUnitData(string unitNumber) {


			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetScannedCouponUnitData));
			sb.Replace("!RESTAURANT_NO", unitNumber);


			DataAccess dac = new DataAccess(AppSettings.DataAnalysisConnectionString);
			DataTable dt = dac.ExecuteQuery(sb.ToString());

			return dt;
		}
		//---------------------------------------------------------------------------------------------------------
		public List<string> GetScannedCouponUnits() {

			List<string> scannedCouponUnits = new List<string>();

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetScannedCouponUnits);
			DataAccess dac = new DataAccess(AppSettings.DataAnalysisConnectionString);
			DataTable dt = dac.ExecuteQuery(sql);

			foreach (DataRow dr in dt.Rows) {

				scannedCouponUnits.Add(dr["RestaurantNumber"].ToString());
			}

			return scannedCouponUnits;
		}
	}
}

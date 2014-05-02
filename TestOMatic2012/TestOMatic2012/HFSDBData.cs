using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	class HFSDBData {
		public HFSDBData() {

			string connectionString = @"Data Source=localhost\MSSQLSERVER2012;Initial Catalog=HFSDB;Integrated Security=True";
			_dac = new DataAccess(connectionString);
		}
		//---------------------------------------------------------------------------------------------------------
		public int GetDuplicateOrderId(int orderNumber, DateTime businessDate) {

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetDuplicateOrderId));

			sb.Replace("!ORDER_NUMBER", orderNumber.ToString());
			sb.Replace("!BUSINESS_DATE", businessDate.ToString("yyyy-MM-dd"));

			DataTable dt = _dac.ExecuteQuery(sb.ToString());

			return Convert.ToInt32(dt.Rows[0][0]);
		}
		//---------------------------------------------------------------------------------------------------------
		public DataTable GetDuplicateOrders() {

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetDuplicateOrders);

			return _dac.ExecuteQuery(sql);
		}
		//---------------------------------------------------------------------------------------------------------
		public DataTable GetLegacyTimeClockData() {

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetLegacyTimeClockData);

			string connectionString = @"Data Source=localhost;Initial Catalog=HFSDB_Clock;Integrated Security=True";
			DataAccess dac = new DataAccess(connectionString);

			return dac.ExecuteQuery(sql);
		}


		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private DataAccess _dac;

	}
}

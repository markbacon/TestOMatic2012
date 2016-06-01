using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	class HFSDBData {
		public HFSDBData() {

			string connectionString = @"Data Source=localhost\SQL2012;Initial Catalog=HFSDB;Integrated Security=True";
			_dac = new DataAccess(connectionString);
		}
		//---------------------------------------------------------------------------------------------------
		public short GetBusinessWeekStartDay() {

			HFSDBDataContext dataContext = new HFSDBDataContext(ServiceSettings.HFSDBConnectionString);

			short? dayNum = dataContext.StoreInfos.Select(s => s.WeekStartDaySystem).FirstOrDefault();

			if (dayNum == null) {
				return DEFAULT_DAY_NUM;
			}
			else {
				return Convert.ToInt16(dayNum);
			}
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
		public DataTable GetOrderItem(DateTime businessDate) {

			StringBuilder sb = new StringBuilder();

			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetOrderItem));
			sb.Replace("!BUSINESS_DATE", businessDate.ToString());

			return _dac.ExecuteQuery(sb.ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		public DataTable GetOrderItemModifier(DateTime businessDate) {

			StringBuilder sb = new StringBuilder();

			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetOrderItemModifier));
			sb.Replace("!BUSINESS_DATE", businessDate.ToString());

			return _dac.ExecuteQuery(sb.ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		public DataTable GetPosOrder(DateTime businessDate) {

			StringBuilder sb = new StringBuilder();

			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetPosOrder));
			sb.Replace("!BUSINESS_DATE", businessDate.ToString());

			return _dac.ExecuteQuery(sb.ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private DataAccess _dac;
		private const short DEFAULT_DAY_NUM = 3;

	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	class ImsData {

		public DataTable GetIdealUsageJrList(DateTime weekEndDate) {


			StringBuilder sb = new StringBuilder();

			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetIdealUsageJrList));
			sb.Replace("!WEEK_END_DATE", weekEndDate.ToString("MM/dd/yyyy"));

			return _dac.ExecuteQuery(sb.ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		public DataTable GetProductsForMenuItem(string menuItemId) {

			StringBuilder sb = new StringBuilder();

			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetProductsForMenuItem));
			sb.Replace("!MENU_ITEM_ID", menuItemId);

			return _dac.ExecuteQuery(sb.ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private DataAccess _dac = new DataAccess(AppSettings.IMSConnectionString);
	
	}
}

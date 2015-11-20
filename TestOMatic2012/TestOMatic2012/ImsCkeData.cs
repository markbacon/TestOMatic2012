using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {

	class ImsCkeData {

		public DataTable GetActiveInventoryItems() {

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetActiveInventoryItems);
			
			DataAccess dac = new DataAccess(AppSettings.IMSConnectionString);

			return dac.ExecuteQuery(sql);
		}
	}
}

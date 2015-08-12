using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {
	
	
	
	class INFO2000Data {

		public DataTable GetCarlsValidationData(DateTime businessDate) {

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetCarlsValidation));
			sb.Replace("!BUSINESS_DATE", businessDate.ToString("yyyy-MM-dd"));

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionStringProd);

			return dac.ExecuteQuery(sb.ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		public DataTable GetHardeesValidationData(DateTime businessDate) {

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetHardeesValidation));
			sb.Replace("!BUSINESS_DATE", businessDate.ToString("yyyy-MM-dd"));

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionStringProd);

			return dac.ExecuteQuery(sb.ToString());
		}
	}
}

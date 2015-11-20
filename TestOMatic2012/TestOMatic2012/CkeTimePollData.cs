using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {
	
	
	class CkeTimePollData {


		public string GetEmployeeSummary() {

			StringBuilder sb = new StringBuilder();

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetCkeTimePollSsnDataList);

			DataAccess dac = new DataAccess(AppSettings.CkeTimePollDataConnectionString);

			DataTable dt = dac.ExecuteQuery(sql);

			foreach (DataRow dr in dt.Rows) {

				string ssn = dr["SSN"].ToString();

				StringBuilder sbQuery = new StringBuilder();
				sbQuery.Append(SqlTemplateBroker.Load(SqlTemplateId.GetCkeTimePollEmployee));
				sbQuery.Replace("!SSN", ssn);

				DataTable dtEmployee = dac.ExecuteQuery(sbQuery.ToString());

				sb.Append(dtEmployee.Rows[0]["EmployeeName"].ToString());
				sb.Append('\t');
				sb.Append(dtEmployee.Rows[0]["LastName"].ToString());
				sb.Append('\t');
				sb.Append(dtEmployee.Rows[0]["FirstName"].ToString());
				sb.Append('\t');
				sb.Append(ssn);
				sb.Append('\t');

				sb.Append(dr["JobCode"].ToString());
				sb.Append('\t');
				sb.Append(Convert.ToDateTime(dr["BeginDate"]).ToString("MM/dd/yyyy"));
				sb.Append('\t');
				sb.Append(Convert.ToDateTime(dr["EndDate"]).ToString("MM/dd/yyyy"));
				sb.Append('\t');
				sb.Append(dr["TimeCardCount"].ToString());
				sb.Append("\r\n");
			}

			return sb.ToString();
		}
		//---------------------------------------------------------------------------------------------------------
		public DataTable GetGMTimeCardHeaderData() {

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetGMTimeCardHeaderData);

			DataAccess dac = new DataAccess(AppSettings.CkeTimePollDataConnectionString);

			return dac.ExecuteQuery(sql);

		}
		//---------------------------------------------------------------------------------------------------------
		public void InsertTimeFileData(string timeFileLine) {

			StringBuilder sb = new StringBuilder();

			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.InsertTimeFileData));
			sb.Replace("!TIME_CARD_LINE", timeFileLine);

			DataAccess dac = new DataAccess(AppSettings.CkeTimePollDataConnectionString);
			dac.ExecuteActionQuery(sb.ToString());
		}
	}
}

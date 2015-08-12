using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form15 : Form {
		public Form15() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DataAccess dacSource = new DataAccess(AppSettings.INFO2000ConnectionString);
			//DataAccess dacDest = new DataAccess(AppSettings.INFO2000ConnectionStringProd);


			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetDepositDtlDim);

			DataTable dt = dacSource.ExecuteQuery(sql);

			StringBuilder sb = new StringBuilder();

			foreach (DataRow dr in dt.Rows) {

				sb.Append(SqlTemplateBroker.Load(SqlTemplateId.InsertDepositDtlFact));

				sb.Replace("!DISCOUNT_DTL_ID", dr["discount_dtl_id"].ToString());
				sb.Replace("!DISCOUNT_ID", dr["discount_id"].ToString());
				sb.Replace("!POD_ID", dr["pod_id"].ToString());
				sb.Replace("!POS_FACT_ID", dr["pos_fact_id"].ToString());
				sb.Replace("!SYSTEM_ID", dr["system_id"].ToString());
				sb.Replace("!RESTAURANT_NO", dr["restaurant_no"].ToString());
				sb.Replace("!CAL_DATE", Convert.ToDateTime(dr["cal_date"]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
				sb.Replace("!TOTAL_AMT", Convert.ToDecimal(dr["total_amt"]).ToString("0.00"));
				sb.Replace("!TOTAL_CNT", dr["total_cnt"].ToString());
				sb.Replace("!CREATE_DATE", Convert.ToDateTime(dr["create_date"]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
				sb.Replace("!CREATE_BY", dr["create_by"].ToString());
				sb.Replace("!LAST_CHG_DATE", Convert.ToDateTime(dr["last_chg_date"]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
				sb.Replace("!LAST_CHG_BY", dr["last_chg_by"].ToString());
				sb.Replace("!SOURCE", dr["source"].ToString());

				//dacDest.ExecuteActionQuery(sb.ToString());

				sb.Append("\r\n\r\n");

			}

			textBox1.Text = sb.ToString();

			button1.Enabled = true;


		}

		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			//string dir;




			button2.Enabled = true;
		}
	}
}

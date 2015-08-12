using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form30 : Form {
		public Form30() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			
			button1.Enabled = false;

			string sql = "DBCC CHECKDB('HFSDB') WITH NO_INFOMSGS, ALL_ERRORMSGS, TABLERESULTS";

			DataAccess dac = new DataAccess(AppSettings.IMSConnectionString);
			DataTable dt = dac.ExecuteQuery(sql);

			StringBuilder sb = new StringBuilder();

			foreach (DataRow dr in dt.Rows) {

				sb.Append(dr["MessageText"].ToString());
				sb.Append("\r\n");
			}


			//foreach (DataColumn dc in dt.Columns) {

			//	sb.Append(dc.ColumnName.ToString());
			//	sb.Append("\t");
			//}

			//sb.Append("\r\n");



			//foreach (DataRow dr in dt.Rows) {

			//	foreach (DataColumn dc in dt.Columns) {

			//		sb.Append(dr[dc.ColumnName].ToString());
			//		sb.Append("\t");
			//	}

			//	sb.Append("\r\n");
			//}

			textBox1.Text = sb.ToString();

			button1.Enabled = true;
		}

		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;


			CkeTimePollData data = new CkeTimePollData();
			textBox1.Text = data.GetEmployeeSummary();





			button2.Enabled = true;
		}
	}
}

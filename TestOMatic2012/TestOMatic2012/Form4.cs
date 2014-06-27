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
	public partial class Form4 : Form {
		public Form4() {
			InitializeComponent();

			Logger.LoggerWrite += form4_OnLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private DataAccess _dac = new DataAccess(AppSettings.DataAnalysisConnectionString);
		private DataAccess _epassDac = new DataAccess(AppSettings.INFO2000ConnectionString);


		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DataTable dt = GetUnitsWithDeletedDeposits();

			foreach (DataRow dr in dt.Rows) {

				DateTime businessDate = Convert.ToDateTime(dr["DepositDate"]);

				if (businessDate >= new DateTime(2014, 5, 2)) {

					int restauranNo = Convert.ToInt32(dr["UnitNumber"].ToString().Substring(1));
					int deletedDepositCount = Convert.ToInt32(dr["DeletedDepositCount"]);

					ProcessUnitData(restauranNo, businessDate, deletedDepositCount);
				}
			}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void form4_OnLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();

		}
		//---------------------------------------------------------------------------------------------------------
		private void ArchiveEpassDepositDetail(DataTable dt) {

			DataAnalysisDataContext dataContxt = new DataAnalysisDataContext(AppSettings.DataAnalysisConnectionString);

			foreach (DataRow dr in dt.Rows) {

				EpassDepositDetailFactArchive depDetail = new EpassDepositDetailFactArchive();

				depDetail.Cal_Date = Convert.ToDateTime(dr["cal_date"]);
				depDetail.create_by = dr["create_by"].ToString();
				depDetail.create_date = Convert.ToDateTime(dr["create_date"]);
				depDetail.Deposit_amt = Convert.ToDecimal(dr["deposit_amt"]);
				depDetail.deposit_dtl_id = Convert.ToInt32(dr["deposit_dtl_id"]);
				depDetail.deposit_id = Convert.ToInt32(dr["deposit_id"]);
				depDetail.last_chg_by = dr["last_chg_by"].ToString();
				depDetail.last_chg_date =  Convert.ToDateTime(dr["last_chg_date"]);
				depDetail.POS_fact_id = Convert.ToInt32(dr["pos_fact_id"]);
				depDetail.Restaurant_no = Convert.ToInt32(dr["restaurant_no"]);
				depDetail.source = dr["source"].ToString();
				depDetail.System_id = Convert.ToInt32(dr["system_id"]);

				dataContxt.EpassDepositDetailFactArchives.InsertOnSubmit(depDetail);
				dataContxt.SubmitChanges();
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void DeleteEpassDepositDetail(DataTable dt) {

			foreach (DataRow dr in dt.Rows) {

				int depositDetailId = Convert.ToInt32(dr["deposit_dtl_id"]);
				Logger.Write("Deleting record with deposit_dtl_id: " + depositDetailId.ToString());

				DeleteEpassDepositDetailRecord(depositDetailId);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void DeleteEpassDepositDetailRecord(int depositDetailId) {

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.DeleteEpassDepositDetail));

			sb.Replace("!DEPOSIT_DTL_ID", depositDetailId.ToString());

			_epassDac.ExecuteActionQuery(sb.ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		private List<deposit_dim> GetDepositDim(int restaurantNo, INFO2000DataContext dataContext) {

			List<deposit_dim> depositDimList =
				(from d in dataContext.deposit_dims
				 where d.Restaurant_no == restaurantNo
					&& d.deposit_descr == "Cash Deposit"
				 orderby d.deposit_type
				 select d).ToList();

			return depositDimList;
		}
		//---------------------------------------------------------------------------------------------------------
		private DataTable GetEpassDepositDetail(int restuarantNo, DateTime businessDate) {

			DataAccess epassDac = new DataAccess(AppSettings.INFO2000ConnectionString);

			StringBuilder sb = new StringBuilder();

			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetEpassDepositDetail));

			sb.Replace("!RESTAURANT_NO", restuarantNo.ToString());
			sb.Replace("!CAL_DATE", businessDate.ToString("yyyy-MM-dd"));

			return epassDac.ExecuteQuery(sb.ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		private DataTable GetDepositDetail(int restuarantNo, DateTime businessDate) {

			StringBuilder sb = new StringBuilder();

			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetDepositDetail));

			sb.Replace("!UNIT_NUMBER", restuarantNo.ToString());
			sb.Replace("!DEPOSIT_DATE", businessDate.ToString("yyyy-MM-dd"));


			return _dac.ExecuteQuery(sb.ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		private DataTable GetUnitsWithDeletedDeposits() {

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetUnitsWithDeletedDeposits);

			return _dac.ExecuteQuery(sql);
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDepositDetail(int restaurantNo, DateTime businessDate, int posFactId, DataTable dt) {

			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);

			List<deposit_dim> depositDimList = GetDepositDim(restaurantNo, dataContext);

			for (int i = 0; i < dt.Rows.Count; i++) {

				DataRow dr = dt.Rows[i];

				deposit_dtl_fact depDetailFact = new deposit_dtl_fact();

				depDetailFact.Cal_Date = businessDate;
				depDetailFact.create_by = "ckrcorp\\mbacon";
				depDetailFact.create_date = DateTime.Now;
				depDetailFact.Deposit_amt = Convert.ToDecimal(dr["Amount"]);
				depDetailFact.deposit_id = depositDimList[i].deposit_id;
				depDetailFact.last_chg_by = "";
				depDetailFact.last_chg_date = DateTime.Now;
				depDetailFact.POS_fact_id = posFactId;
				depDetailFact.Restaurant_no = restaurantNo;
				depDetailFact.source = "ML";
				depDetailFact.System_id = 2;
				
				dataContext.deposit_dtl_facts.InsertOnSubmit(depDetailFact);
				dataContext.SubmitChanges();

				Logger.Write("deposit_dtl_fact record added for restaurant: " + restaurantNo.ToString() + " on " + businessDate.ToString("MM/dd/yyyy") + " for " + Convert.ToDecimal(depDetailFact.Deposit_amt).ToString("$0.00"));

			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessUnitData(int restaurantNo, DateTime businessDate, int deletedDepositCount) {

			Logger.Write("Begin processing data for restaurant: " + restaurantNo.ToString() + " and business date: " + businessDate.ToString("mm/dd/yyyy"));

			
			DataTable dtEpassDepositDetail = GetEpassDepositDetail(restaurantNo, businessDate);

			//-- If there is only one deposit then we will assume that they did not get the fix
			//-- TODO Revisist to check historical data
			if (dtEpassDepositDetail.Rows.Count > 1) {
				Logger.Write("Archiving records.");
				ArchiveEpassDepositDetail(dtEpassDepositDetail);

				int posFactId = Convert.ToInt32(dtEpassDepositDetail.Rows[0]["pos_fact_id"]);
				Logger.Write("pos_fact_id = " + posFactId.ToString());

				Logger.Write("Deleting records.");
				DeleteEpassDepositDetail(dtEpassDepositDetail);

				DataTable dtDepositDetail = GetDepositDetail(restaurantNo, businessDate);

				Logger.Write("Begin processing deposit detail");
				ProcessDepositDetail(restaurantNo, businessDate, posFactId, dtDepositDetail);


			}
			else {
				Logger.Write("Only one Epass deposit record found for restaurant: " + restaurantNo.ToString() + " and business date: " + businessDate.ToString("MM/dd/yyyy") + ". Data will not be changed.");

			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void textBox1_TextChanged(object sender, EventArgs e) {

			if (textBox1.Text.Length > 0) {
				textBox1.SelectionStart = textBox1.Text.Length - 1;
				textBox1.ScrollToCaret();
				Application.DoEvents();
			}

		}
	}
}

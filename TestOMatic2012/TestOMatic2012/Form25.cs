using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form25 : Form {
		public Form25() {

			InitializeComponent();

			Logger.StartLogSession();
		}
		//--------------------------------------------------------------------------------------------------
		//-- Private Members
		//--------------------------------------------------------------------------------------------------
		private INFO2000DataContext _dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);
		private DataAccess _dac = new DataAccess(AppSettings.INFO2000ConnectionString);
		
		//--------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;
			
			DateTime startTime = DateTime.Now;
			Logger.Write("Begin recalculating costs...");
			

			DataTable dt = GetMbmRcptDetailWithNegativeCost();

			foreach (DataRow dr in dt.Rows) {


				ProcessRcptDetail(dr);
			}

			Logger.Write("Recalculating costs completed. Elapsed time:  " + (DateTime.Now -startTime).ToString());

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form25_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}
		//--------------------------------------------------------------------------------------------------
		private decimal GetInvPieceCount(int systemId, int invRecordId) {

			decimal invPieceCount = -1;

			if (systemId == 1) {
				inv_item_cke_dim invItem =
					(from i in _dataContext.inv_item_cke_dims
					 where i.inv_item_id == invRecordId
					 select i).SingleOrDefault();


				if (invItem != null) {

					invPieceCount = Convert.ToDecimal(invItem.inv_piece_count);
				}
			}
			else {
				inv_item_xref invXref =
					(from i in _dataContext.inv_item_xrefs
					 where i.inv_xref_id == invRecordId
					 select i).SingleOrDefault();

				if (invXref != null) {

					invPieceCount = Convert.ToDecimal(invXref.inv_piece_count);
				}
			}

			return invPieceCount;
		}
		//--------------------------------------------------------------------------------------------------
		private string GetLegacyUnitNumber(int restaurantNo) {

			string legacyUnitNumber =
				(from r in _dataContext.restaurant_dims
				 where r.restaurant_no == restaurantNo
				 select r.RESTAURANT_NO_OLD).First();


			return legacyUnitNumber;
		}
		//--------------------------------------------------------------------------------------------------
		private mbminvs_load GetMbmInvsRecord(int systemId, string unitNumber, string invoiceNumber, int invItemId, int invXrefId) {

			mbminvs_load mbmRecord = null;

			if (systemId == 1) {
				mbmRecord =
					(from m in _dataContext.mbminvs_loads
					 where m.system_id == systemId
						&& m.hfs_unit_no == unitNumber
						&& m.mbm_invoice_no == invoiceNumber
						&& m.inv_item_id == invItemId
					 select m).SingleOrDefault();
			}
			else {

				mbmRecord =
					(from m in _dataContext.mbminvs_loads
					 where m.system_id == systemId
						&& m.hfs_unit_no == unitNumber
						&& m.mbm_invoice_no == invoiceNumber
						&& m.inv_xref_id == invXrefId
					 select m).SingleOrDefault();
			}

			return mbmRecord;
		}
		//--------------------------------------------------------------------------------------------------
		private DataTable GetMbmRcptDetailWithNegativeCost() {

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetMbmRcptDetailWithNegativeCost);

			return _dac.ExecuteQuery(sql);
		}
		//--------------------------------------------------------------------------------------------------
		private void ProcessRcptDetail(DataRow dr) {

			int systemId = Convert.ToInt32(dr["system_id"]);
			int invItemId = Convert.ToInt32(dr["inv_item_id"]);
			int invXrefId = Convert.ToInt32(dr["inv_xref_id"]);

			decimal quantity = Convert.ToDecimal(dr["mbm_qty"]);
			string unitNumber = dr["restaurant_no"].ToString();

			decimal cost = Convert.ToDecimal(dr["mbm_cost"]);
			decimal mbmAmount = Convert.ToDecimal(dr["mbm_amt"]);

			string invoiceNo = dr["mbm_inv_no"].ToString();


			mbminvs_load mbmRecord = GetMbmInvsRecord(systemId, unitNumber, invoiceNo, invItemId, invXrefId);

			if (mbmRecord == null) {
				string legacyUnitNumber = GetLegacyUnitNumber(Convert.ToInt32(unitNumber));

				mbmRecord = GetMbmInvsRecord(systemId, legacyUnitNumber, invoiceNo, invItemId, invXrefId);
			}

			if (mbmRecord != null) {

				string refInvoiceNo = mbmRecord.mbm_ref_no;

				mbminvs_load mbmOriginalRecord = GetMbmInvsRecord(systemId, unitNumber, refInvoiceNo, invItemId, invXrefId);

				if (mbmOriginalRecord == null) {

					string legacyUnitNumber = GetLegacyUnitNumber(Convert.ToInt32(unitNumber));

					mbmOriginalRecord = GetMbmInvsRecord(systemId, legacyUnitNumber, refInvoiceNo, invItemId, invXrefId);
				}

				if (mbmOriginalRecord != null) {

					decimal originalAmount = Convert.ToDecimal(mbmOriginalRecord.mbm_sales);
					decimal originalQuantity = Convert.ToDecimal(mbmOriginalRecord.mbm_qty);

					decimal originalCost = originalAmount / originalQuantity;


					decimal invPieceCount;

					if (systemId == 1) {

						invPieceCount = GetInvPieceCount(systemId, invItemId);
					}
					else {
						invPieceCount = GetInvPieceCount(systemId, invXrefId);
					}

					int mbmRcptDetailid = Convert.ToInt32(dr["mbm_rcpt_dtl_id"]);

					Logger.Write("Updating cost. Unit Number: " + unitNumber + ". Invoicer Number:  " + invoiceNo +
								". Incorrect cost:  " + cost.ToString("0.00") + ".  Updated cost:  " + (originalCost / invPieceCount).ToString("0.00"));

					UpdateMbmDtlCost(mbmRcptDetailid, originalCost / invPieceCount);
				}
				else {

					if (systemId == 1) {
						Logger.Write("Unable to locate original MBM Record for invoice " + refInvoiceNo + " and item ID " + invItemId.ToString());
					}
					else {
						Logger.Write("Unable to locate original MBM Record for invoice " + refInvoiceNo + " and item xref ID " + invXrefId.ToString());
					}
				}
			}
			else {
				if (systemId == 1) {
					Logger.Write("Unable to locate MBM credit Record for invoice " + invoiceNo + " and item ID " + invItemId.ToString());
				}
				else {
					Logger.Write("Unable to locate MBM credit Record for invoice " + invoiceNo + " and item xref ID " + invXrefId.ToString());
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void UpdateMbmDtlCost(int mbmRcptDtlId, decimal cost) {

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.UpdateMbmDtlCost));
			sb.Replace("!MBM_RCPT_DTL_ID", mbmRcptDtlId.ToString());
			sb.Replace("!MBM_COST", cost.ToString());

			_dac.ExecuteActionQuery(sb.ToString());
		}
		//---------------------------------------------------------------------------------------------------
		private void textBox1_TextChanged(object sender, EventArgs e) {

			if (textBox1.Text.Length > 2024) {
				textBox1.Text = "";
			}

			if (textBox1.Text.Length > 0) {
				textBox1.SelectionStart = textBox1.Text.Length - 1;
				textBox1.ScrollToCaret();
				Application.DoEvents();
			}
		}
	}
}

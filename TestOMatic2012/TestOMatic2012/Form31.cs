using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form31 : Form {
		public Form31() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private DataAnalysis2DataContext _da2DataContext = new DataAnalysis2DataContext();
		private INFO2000DataContext _epassDataContext = new INFO2000DataContext();


		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			ProcessItemXref();
			
			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessItemXref() {

			List<inv_item_dim> invItemList = _epassDataContext.inv_item_dims.ToList();


			List<HardeesProduct> hardeesProductList = _da2DataContext.HardeesProducts.ToList();


			foreach (HardeesProduct hp in hardeesProductList) {


				inv_item_dim invItem = _epassDataContext.inv_item_dims.Where(i => i.inv_item_code == hp.ParentProductNumber).FirstOrDefault();

				if (invItem != null) {


					inv_item_xref xrefItem = new inv_item_xref();

					xrefItem.exitem_code = hp.ProductNumber;
					xrefItem.exitem_descr = hp.Description;

					bool isFFM = (xrefItem.exitem_code.Length == 4);

					if (isFFM) {
						xrefItem.ffm_nonffm_flag = 'N';
					}
					else {
						xrefItem.ffm_nonffm_flag = 'Y';
					}

					xrefItem.initem_code = hp.ProductNumber;
					xrefItem.inv_ffm_retail = 0;
					xrefItem.inv_item_id = invItem.inv_item_id;
					xrefItem.inv_lbs_count = hp.ActualConversionFactor;
					xrefItem.inv_packaged = hp.UnitOfMeasure;
					xrefItem.inv_piece_count = hp.IdealConversionFactor;
					xrefItem.inv_type_code = null;

					if (!isFFM) {
						xrefItem.mbm_code = hp.ProductNumber;
					}


					_epassDataContext.inv_item_xrefs.InsertOnSubmit(xrefItem);
					_epassDataContext.SubmitChanges();
				}
			}
		}
	}
}

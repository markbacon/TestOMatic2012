using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestOMatic2012 {
	
	public partial class Form19 : Form {
		public Form19() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string filePath = "C:\\fc\\work\\prdnos.asc";

			ProcessPrdnosAscFile(filePath);

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessPrdnosAscFile(string filePath) {

			HardeesRecipeData data = new HardeesRecipeData();

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					InventoryItem invItem = new InventoryItem();
					invItem.AlternateUnitOfMeasure = line.Substring(46, 2).Trim();
					invItem.Code = line.Substring(1, 10).Trim();
					invItem.ConversionFactor = Convert.ToDecimal(line.Substring(48, 9).Trim());
					invItem.CreateDate = DateTime.Now;
					invItem.Description = line.Substring(11, 30).Trim();
					invItem.EffDate = DateTime.Today;
					invItem.EndDate = new DateTime(2016, 12, 31);
					invItem.IdealConversionFactor = Convert.ToDecimal(line.Substring(57, 9).Trim());
					invItem.IsReciped = true;

					string activeFlag = line.Substring(0, 1);

					if (activeFlag == "Y") {
						invItem.Status = 1;
					}
					else {
						invItem.Status = 0;
					}

					invItem.UnitOfMeasure = line.Substring(44, 2).Trim();


					data.SaveInventoryItem(invItem);


					string productNumber = line.Substring(1, 10).Trim();
					string description = line.Substring(11, 30).Trim();
					string unitOfMeasure = line.Substring(44, 2).Trim();
					string altUnitOfMeasure = line.Substring(46, 2).Trim();
					string actualConversionFactor = line.Substring(48, 9).Trim();
					string idealConversionFactor = line.Substring(57, 9).Trim();
				}
			}
		}
	}
}

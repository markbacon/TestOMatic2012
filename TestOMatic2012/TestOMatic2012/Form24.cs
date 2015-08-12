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
	public partial class Form24 : Form {
		public Form24() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private DataAnalysis2DataContext _dataContext = new DataAnalysis2DataContext();


		private enum MenuDestLineCsvPosition {

			UnitNumber = 0,
			BusinessDate = 1,
			DestItemTypeId = 2,
			NonFoodIndicator = 3,
			FreeIndicator = 4,
			SpecialPaymentypeId = 5,
			MenuItemId = 6,
			DestinationId = 7,
			DayPartId = 8,
			Amount = 9,
			Quantity = 10,
			ComboSizeIndicator = 11
		}
		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string[] items = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);


			string jrNumber;

			foreach (string item in items) {


				switch (item.Length) {

					case 1:
						jrNumber = "JR00" + item;
						break;

					case 2:
						jrNumber = "JR0" + item;
						break;

					case 3:
						jrNumber = "JR" + item;
						break;

					case 4:
						jrNumber = "J" + item;
						break;

					default:
						jrNumber =  item;
						break;
				}


				MixCategoryItemXref catItemXref = new MixCategoryItemXref();
				catItemXref.ItemCategoryId = 1;
				catItemXref.ItemNumber = jrNumber;

				_dataContext.MixCategoryItemXrefs.InsertOnSubmit(catItemXref);
				_dataContext.SubmitChanges();

			}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			ProcessFiles();

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			using (StreamReader sr = new StreamReader("C:\\Temp2.3\\GBItems.txt")) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { '\t' });

					if (items[3].Trim() != "NA") {

						DataWarehouseGBItem gbItem = new DataWarehouseGBItem();

						gbItem.Category = items[2].Trim();
						gbItem.Description = items[1].Trim();
						gbItem.ItemNumber = items[0].Trim();

						_dataContext.DataWarehouseGBItems.InsertOnSubmit(gbItem);
						_dataContext.SubmitChanges();
					}
				}
			}

			button3.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button4_Click(object sender, EventArgs e) {

			button4.Enabled = false;

			using (StreamReader sr = new StreamReader("C:\\Temp62\\CatItem.txt")) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { '\t' });

					CatItemXref x = new CatItemXref();
					x.CategoryId = items[2].Trim();
					x.CategoryName = items[3].Trim();
					x.ItemDescription = items[1].Trim();
					x.ItemNumber = items[0].Trim();

					_dataContext.CatItemXrefs.InsertOnSubmit(x);
					_dataContext.SubmitChanges();
					
				}
			}

			button4.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo file) {

			DateTime businessDate = DateTime.MinValue;
			string unitNumber;
			int mixDestPollFileId = -1;

			using (StreamReader sr = file.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();
					string[] items = line.Split(new char[] { ',' });

					for (int i = 0; i < items.Length; i++) {

						items[i] = items[i].Replace("\"", "");
					}



					if (items[0].IndexOf("DATE") > -1) {

						businessDate = DateTime.ParseExact(items[1].Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
					}
					else if (items[0].IndexOf("STORE") > -1) {

						unitNumber = items[1].Replace("\"", "").Trim();

						mixDestPollFileId = SaveMixDestPollFile(unitNumber, businessDate, file.Name);

					}
					else {
						//-- Detail records have more than 4 fields 
						if (items.Length > 4) {

							if (items[(int)MenuDestLineCsvPosition.FreeIndicator] != "Y") {

								MixDestPollItem pollItem = new MixDestPollItem();

								pollItem.ItemNumber = items[(int)MenuDestLineCsvPosition.MenuItemId];
								pollItem.MixDestPollFileId = mixDestPollFileId;
								pollItem.Price = Convert.ToDecimal(items[(int)MenuDestLineCsvPosition.Amount]);
								pollItem.Quantity = Convert.ToDecimal(items[(int)MenuDestLineCsvPosition.Quantity]);

								_dataContext.MixDestPollItems.InsertOnSubmit(pollItem);
								_dataContext.SubmitChanges();
							}
						}
					}
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFiles() {

			DirectoryInfo di = new DirectoryInfo("C:\\Temp2.0");

			FileInfo[] files = di.GetFiles("????MixDest.pol");

			foreach (FileInfo file in files) {

				ProcessFile(file);

			}



		}
		//---------------------------------------------------------------------------------------------------------
		private int SaveMixDestPollFile(string unitNumber, DateTime businessDate, string fileName) {

			MixDestPollFile pollFile = new MixDestPollFile();

			pollFile.FileDate = businessDate;
			pollFile.FileName = fileName;
			pollFile.UnitNumber = unitNumber;

			_dataContext.MixDestPollFiles.InsertOnSubmit(pollFile);
			_dataContext.SubmitChanges();

			return pollFile.MixDestPollFileId;

		}
	}
}

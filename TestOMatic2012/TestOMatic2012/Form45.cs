using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TestOMatic2012 {

	public partial class Form45 : Form {

		public Form45() {

			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------------
		private const string PATTERN = @",(?!(?<=(?:^|,)\s*\x22(?:[^\x22]|\x22\x22|\\\x22)*,)(?:[^\x22]|\x22\x22|\\\x22)*\x22\s*(?:,|$))";

		private Regex _regX = new Regex(PATTERN);

		private enum ActVsIdealCrunchTimeCsvPosition {
			ProductNumber = 0,
			ProductName = 1,
			InventoryUnit = 2,
			ValueBeginning = 3,
			QtyBeginning = 4,
			ValuePurchase = 5,
			QtyPurchase = 6,
			ValueEnding = 7,
			QtyEnding = 8,
			ActualCost = 9,
			ActualQtyUsed = 10,
			ValueInvAdj = 11,
			QtyInvAdj = 12,
			ValueInvVariance = 13,
			QtyInvVariance = 14
		}


		public enum ActVsIdealCkeCsvPosition {

			LocationCode,
			LocationName,
			InventoryDate,
			InventoryType,
			ProductNumber,
			ProductName,
			Price,
			VarianceAmount,
			VarianceQuantity,
			ValueBeginning,
			QuantityBeginning,
			Valuebook,
			QuantityBook,
			ValueReceived,
			QuantityReceive,
			ValueSold,
			QuantitySold,
			ValueTransferred,
			QuantityTransfers,
			ValuePhysical,
			QuantityPhysical,
			ValueActual,
			QuantityActual
		}

		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

			using (StreamReader sr = new StreamReader("C:\\Temp105.mm\\ActualVSTheoretical_ActualCostDetails_07_31_2023.csv")) {

				bool isFirst = true;

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					if (!isFirst) {

						string[] items = _regX.Split(line);

						for (int i = 0; i < items.Length; i++) {
							items[i] = items[i].Replace(",", "");
							items[i] = items[i].Replace("\"", "");
						}

						ActVsIdealCrunchTime actVsIdeal = new ActVsIdealCrunchTime() {
							UnitNumber = "1502786",
							InventoryDate = Convert.ToDateTime("2023-07-24"),

							ProductNumber = items[(int)ActVsIdealCrunchTimeCsvPosition.ProductNumber],
							ProductName = items[(int)ActVsIdealCrunchTimeCsvPosition.ProductName],
							InventoryUnit = items[(int)ActVsIdealCrunchTimeCsvPosition.InventoryUnit],
							ValueBeginning = Convert.ToDecimal(items[(int)ActVsIdealCrunchTimeCsvPosition.ValueBeginning]),
							QtyBeginning = Convert.ToDecimal(items[(int)ActVsIdealCrunchTimeCsvPosition.QtyBeginning]),
							ValuePurchase = Convert.ToDecimal(items[(int)ActVsIdealCrunchTimeCsvPosition.ValuePurchase]),
							QtyPurchase = Convert.ToDecimal(items[(int)ActVsIdealCrunchTimeCsvPosition.QtyPurchase]),
							ValueEnding = Convert.ToDecimal(items[(int)ActVsIdealCrunchTimeCsvPosition.ValueEnding]),
							QtyEnding = Convert.ToDecimal(items[(int)ActVsIdealCrunchTimeCsvPosition.QtyEnding]),
							ActualCost = Convert.ToDecimal(items[(int)ActVsIdealCrunchTimeCsvPosition.ActualCost]),
							ActualQtyUsed = Convert.ToDecimal(items[(int)ActVsIdealCrunchTimeCsvPosition.ActualQtyUsed]),
							ValueInvAdj = Convert.ToDecimal(items[(int)ActVsIdealCrunchTimeCsvPosition.ValueInvAdj]),
							QtyInvAdj = Convert.ToDecimal(items[(int)ActVsIdealCrunchTimeCsvPosition.QtyInvAdj]),
							ValueInvVariance = Convert.ToDecimal(items[(int)ActVsIdealCrunchTimeCsvPosition.ValueInvVariance]),
							QtyInvVariance = Convert.ToDecimal(items[(int)ActVsIdealCrunchTimeCsvPosition.QtyInvVariance])
						};

						dataContext.ActVsIdealCrunchTimes.InsertOnSubmit(actVsIdeal);
						dataContext.SubmitChanges();
					}
					else {
						isFirst = false;
					}
				}
			}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------

		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;
			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

			using (StreamReader sr = new StreamReader("C:\\Temp105.mm\\WeeklyInventory.csv")) {

				bool isFirst = true;

				int counter = 0;

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					if (!isFirst) {

						string[] items = _regX.Split(line);

						for (int i = 0; i < items.Length; i++) {
							items[i] = items[i].Replace(",", "");
							items[i] = items[i].Replace("\"", "");
							items[i] = items[i].Replace("%", "");
						}

						ActVsIdealCke actVsIdeal = new ActVsIdealCke() {

							InventoryDate = Convert.ToDateTime(items[(int)ActVsIdealCkeCsvPosition.InventoryDate]),
							LocationCode = items[(int)ActVsIdealCkeCsvPosition.LocationCode],
							LocationName = items[(int)ActVsIdealCkeCsvPosition.LocationName],
							InventoryType = items[(int)ActVsIdealCkeCsvPosition.InventoryType],
							ProductNumber = items[(int)ActVsIdealCkeCsvPosition.ProductNumber],
							ProductName = items[(int)ActVsIdealCkeCsvPosition.ProductName],
							Price = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.Price]),
							VarianceAmount = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.VarianceAmount]),
							VarianceQuantity = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.VarianceQuantity]),
							ValueBeginning = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.ValueBeginning]),
							QuantityBeginning = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.QuantityBeginning]),
							Valuebook = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.Valuebook]),
							QuantityBook = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.QuantityBook]),
							ValueReceived = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.ValueReceived]),
							QuantityReceive = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.QuantityReceive]),
							ValueSold = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.ValueSold]),
							QuantitySold = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.QuantitySold]),
							ValueTransferred = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.ValueTransferred]),
							QuantityTransfers = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.QuantityTransfers]),
							ValuePhysical = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.ValuePhysical]),
							QuantityPhysical = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.QuantityPhysical]),
							ValueActual = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.ValueActual]),
							QuantityActual = Convert.ToDecimal(items[(int)ActVsIdealCkeCsvPosition.QuantityActual])
						};

						string temp = items[(int)ActVsIdealCkeCsvPosition.LocationName];
						int pos = temp.LastIndexOf(' ') + 1;
						actVsIdeal.UnitNumber = temp.Substring(pos);

						dataContext.ActVsIdealCkes.InsertOnSubmit(actVsIdeal);
						dataContext.SubmitChanges();

						counter++;

						if (counter % 100 == 0) {
							Logger.Write(counter.ToString() + " records processed.");
						}
					}
					else {
						isFirst = false;
					}
				}
			}

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();


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

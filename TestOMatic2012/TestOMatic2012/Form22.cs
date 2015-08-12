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
	public partial class Form22 : Form {
		public Form22() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------------
		private class ItemUsageData {

			public string ItemCode = "";
			public string Description = "";
			public decimal Quantity = 0;
			public decimal Amount = 0;
		}

		private List<ItemUsageData> _imsList = new List<ItemUsageData>();
		private List<ItemUsageData> _epassList = new List<ItemUsageData>();





		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string filePath = "C:\\temp2.3\\IMSUsage.txt";

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { '\t' });

					ItemUsageData iu = new ItemUsageData();
					iu.Amount = Convert.ToDecimal(items[3]);
					iu.Description = items[1].Trim();
					iu.ItemCode = items[0].Trim();
					iu.Quantity = Convert.ToDecimal(items[2]);

					_imsList.Add(iu);
				}
			}


			filePath = "C:\\temp2.3\\EpassUsage.txt";

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { '\t' });

					ItemUsageData iu = new ItemUsageData();
					iu.Amount = Convert.ToDecimal(items[3]);
					iu.Description = items[1].Trim();
					iu.ItemCode = items[0].Trim();
					iu.Quantity = Convert.ToDecimal(items[2]);

					_epassList.Add(iu);
				}
			}


			filePath = "C:\\temp2.3\\Output.txt";


			using (StreamWriter sw = File.AppendText(filePath)) {


				foreach (ItemUsageData imsUsage in _imsList) {

					StringBuilder sb = new StringBuilder();
					sb.Append(imsUsage.ItemCode);
					sb.Append('\t');
					sb.Append(imsUsage.Description);
					sb.Append('\t');
					sb.Append(imsUsage.Quantity.ToString("0.00"));
					sb.Append('\t');
					sb.Append(imsUsage.Amount.ToString("0.00"));
					sb.Append('\t');
					sb.Append('\t');

					ItemUsageData epassUsage = _epassList.Where(i => i.ItemCode == imsUsage.ItemCode).FirstOrDefault();

					if (epassUsage != null) {

						sb.Append(epassUsage.ItemCode);
						sb.Append('\t');
						sb.Append(epassUsage.Description);
						sb.Append('\t');
						sb.Append(epassUsage.Quantity.ToString("0.00"));
						sb.Append('\t');
						sb.Append(epassUsage.Amount.ToString("0.00"));
					}

					else {
						sb.Append("\t\t\t");
					}

					sw.WriteLine(sb.ToString());
				}
			}
			
			button1.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			DateTime businessDate = new DateTime(2015, 4, 15);

			ValidationFileBuilder vfb = new ValidationFileBuilder();
			vfb.BuildCarlsFile(businessDate);
			vfb.BuildHardeesFile(businessDate);

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			string filePath = @"C:\Users\mbacon\Documents\Analysis\Marketing\RevenueManage\CJ_DLY_SLS_TRN.txt";

			StringBuilder sb = new StringBuilder();

			int whitespaceCount = 0;

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					for (int i = 0; i < line.Length; i++) {

						if (char.IsWhiteSpace(line[i])) {
							whitespaceCount++;
						}
						else {

							if (whitespaceCount > 0) {
								if (whitespaceCount > 10) {
									sb.Append("\t\t");
								}
								else {
									sb.Append("\t");
								}

								whitespaceCount = 0;
							}

							sb.Append(line[i]);
						}
					}

					sb.Append("\r\n");
				}
			}

			textBox1.Text = sb.ToString();

			button3.Enabled = true;

		}
	}
}

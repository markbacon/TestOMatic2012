using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form54 : Form {
		public Form54() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			ProcessDirectory(@"C:\Store Data\x1100285\X1100285");


			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectory(string dirName) {

			DirectoryInfo di = new DirectoryInfo(dirName);

			DirectoryInfo[] directories = di.GetDirectories("X*");

			foreach (DirectoryInfo directory in directories) {


				FileInfo[] files = directory.GetFiles("*Mix.pol");

				foreach (FileInfo file in files) {

					ProcessFile(file);

				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo file) {

			int mixPollFileId = 0;

			MixPollFile mpf = new MixPollFile();
			mpf.FileName = file.Name;

			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();
	

			using (StreamReader sr = file.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine().Replace("\"", "");

					string[] items = line.Split(new char[] { ',' });


					if (items.Count() < 5) {

						if (items[0].Trim() == "DATE") {

							mpf.BusinessDate = DateTime.ParseExact(items[1], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
						}

						else if (items[0].Trim() == "STORE") {

							mpf.UnitNumber = items[1].Trim();

							dataContext.MixPollFiles.InsertOnSubmit(mpf);
							dataContext.SubmitChanges();

							mixPollFileId = mpf.MixPollFileId;

						}
					}

					else {

						MixPollItem mpi = new MixPollItem();

						mpi.Department = items[1].Trim();
						mpi.ItemDescription = items[2].Trim();
						mpi.MenuItemId = items[9].Trim();
						mpi.MenuPollFileId = mixPollFileId;
						mpi.Price = Convert.ToDecimal(items[3]);
						mpi.QuantitySold = Convert.ToInt32(items[4]);

						dataContext.MixPollItems.InsertOnSubmit(mpi);
						dataContext.SubmitChanges();

					}

				}
			}
		}
	}
}

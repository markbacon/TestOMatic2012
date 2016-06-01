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
	public partial class Form55 : Form {
		public Form55() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string dirPath = "D:\\CarlsAdjustmentFiles";

			DirectoryInfo di = new DirectoryInfo(dirPath);

			DirectoryInfo[] directories = di.GetDirectories();

			foreach (DirectoryInfo directory in directories) {

				ProcessDirectory(directory);

			}


			button1.Enabled = true;
		}

		DataAnalysisDataContext _dataContext = new DataAnalysisDataContext();


		private void ProcessDirectory(DirectoryInfo di) {


			FileInfo[] files = di.GetFiles();

			foreach (FileInfo file in files) {

				ProcessFile(file);

			}
		}

		private void ProcessFile(FileInfo fi) {

			int lineCount = 0;

			using (StreamReader sr = fi.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					lineCount++;
				}
			}

			LaborAdjustmentFile laf = new LaborAdjustmentFile();

			laf.FileDate = DateTime.ParseExact(fi.Name.Substring(9, 8), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
			laf.FileName = fi.Name;
			laf.RecordCount = lineCount - 1;
			laf.UnitNumber = fi.Name.Substring(1, 7);

			_dataContext.LaborAdjustmentFiles.InsertOnSubmit(laf);
			_dataContext.SubmitChanges();

			if (lineCount > 1) {


				using (StreamReader sr = fi.OpenText()) {

					while (sr.Peek() != -1) {

						string line = sr.ReadLine();

						string[] items = line.Split(new char[] { ',' });

						LaborAdjustmentRecord lar = new LaborAdjustmentRecord();
						lar.Adjustment = items[7].Trim();
						lar.AdjustmentTime = items[4].Trim();
						lar.AdjustmentType = items[5].Trim();
						lar.AuditDate = items[9].Trim();
						lar.AuditType = items[12].Trim();
						lar.CreatedBy = items[10].Trim();
						lar.Deleted = items[8].Trim();
						lar.EmployeeId = items[1].Trim();
						lar.FirstName = items[3].Trim();
						lar.HoursMins = items[6].Trim();
						lar.LaborAdjustmentFileId = laf.LaborAdjustmentFileId;
						lar.LastName = items[2].Trim();
						lar.ModifiedBy = items[11].Trim();
						lar.RecordId = items[0].Trim();

						_dataContext.LaborAdjustmentRecords.InsertOnSubmit(lar);
						_dataContext.SubmitChanges();
					}
				}
			}
		}
	}
}

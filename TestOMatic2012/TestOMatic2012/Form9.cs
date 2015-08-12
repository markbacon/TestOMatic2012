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
	public partial class Form9 : Form {
		public Form9() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------------
		//---------------------------------------------------------------------------------------------------------
		private DataAnalysisDataContext _dataContext = new DataAnalysisDataContext();
		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string fileDirectory = "D:\\FinFile";

			ProcessFinFileDirectories(fileDirectory);


			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFinFile(FileInfo file) {

			FinFile finFile = new FinFile();
			finFile.RestaurantNo = file.Directory.Name;
			finFile.FileName = file.Name;

			_dataContext.FinFiles.InsertOnSubmit(finFile);
			_dataContext.SubmitChanges();

			int finFileId = finFile.FinFileId;

			using (StreamReader sr = file.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { ',' });

					items[5] = items[5].Replace("\"", "");

					if (items[5].Length > 0) {

						FinFileDetail finDetail = new FinFileDetail();

						finDetail.Amount = Convert.ToDecimal(items[11]) / 100;
						finDetail.Count = Convert.ToInt32(items[10]);
						finDetail.Descriptor = items[5];
						finDetail.FinFileId = finFileId;
						finDetail.LineIndex = Convert.ToInt32(items[4]);

						_dataContext.FinFileDetails.InsertOnSubmit(finDetail);
						_dataContext.SubmitChanges();
					}
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFinFileDirectories(string finFileRootDirecory) {

			DirectoryInfo di = new DirectoryInfo(finFileRootDirecory);

			DirectoryInfo[] directories = di.GetDirectories("X1100579");

			foreach (DirectoryInfo directory in directories) {

				ProcessFinFileDirectory(directory);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFinFileDirectory(DirectoryInfo di) {

			FileInfo[] files = di.GetFiles("*fin*");

			foreach (FileInfo file in files) {

				ProcessFinFile(file);
			}

		}
	}
}

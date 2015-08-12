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

using Ionic.Zip;

namespace TestOMatic2012 {
	public partial class frmSmartReportsFix : Form {
		public frmSmartReportsFix() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string hfscoRootDirectory = @"\\ckeanafnp01\HFSCO_RO";

			DirectoryInfo di = new DirectoryInfo(hfscoRootDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				ProcessDirectory(directory);
			}
			

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {

			DateTime startDate = DateTime.Today.AddDays(-10);


			while (startDate < DateTime.Today) {

				DirectoryInfo directory = new DirectoryInfo(Path.Combine(di.FullName, startDate.ToString("yyyyMMdd")));

				startDate = startDate.AddDays(1);

				if (directory.Exists) {

					FileInfo file = directory.GetFiles("SmartRpts.zip").FirstOrDefault();

					if (file != null) {

						if (file.Exists) {

							string targetPath = Path.Combine("D:\\HFSCO_RO", di.Name, directory.Name);

							if (!Directory.Exists(targetPath)) {
								Directory.CreateDirectory(targetPath);
							}

							targetPath = Path.Combine(targetPath, file.Name);

							file.CopyTo(targetPath);
						}
					}
				}
			}
		}
	}
}

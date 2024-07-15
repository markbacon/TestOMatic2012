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
	public partial class Form33 : Form {
		public Form33() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------------
		//---------------------------------------------------------------------------------------------------------
		private StringBuilder _sb = new StringBuilder();


		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			CheckCarlsTLogDirectories();
			textBox1.Text = _sb.ToString();

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			DirectoryInfo di = new DirectoryInfo(@"D:\xdata1\cmsos2\cke");

			DirectoryInfo[] direcories = di.GetDirectories();

			foreach (DirectoryInfo directory in direcories) {

				FileInfo file = directory.GetFiles("Empl.sav").SingleOrDefault();

				if (file.Exists) {
					string targetPath = Path.Combine(file.DirectoryName, "Empl.out");

					file.CopyTo(targetPath, true);

					targetPath = Path.Combine(file.DirectoryName, "Empl.bak");

					file.CopyTo(targetPath, true);

				}




			}

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void CheckCarlsTLogDirectories() {

			DirectoryInfo di = new DirectoryInfo(@"\\ckeanafnp01\cjrco_ro");


			List<string> companyStores = GetCarlsCompanyStores();

			foreach (string companyStore in companyStores) {


				DirectoryInfo[] directories = di.GetDirectories(companyStore);

				foreach (DirectoryInfo directory in directories) {

					if (string.Compare(directory.Name, "X1100000") > 0 && directory.Name.Length == 8) {

						int dateNum = 20150701;

						while (dateNum++ < 20150718) {

							DirectoryInfo tlogDirectory = directory.GetDirectories(dateNum.ToString()).FirstOrDefault();

							if (tlogDirectory != null) {
								string fileName = dateNum.ToString() + ".TransHist.xml";

								if (tlogDirectory.GetFiles(fileName).Count() == 0) {

									_sb.Append("Unit:   " + directory.Name + " is missing TLog file for:  " + fileName);
									_sb.Append("\r\n");
								}
							}
							else {
								_sb.Append("Unit:   " + directory.Name + " is missing TLog directroy for:  " + dateNum.ToString());
								_sb.Append("\r\n");

							}
						}
					}
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void CheckCarlsTLogDirectoriesOld() {

			DirectoryInfo di = new DirectoryInfo(@"\\ckeanafnp01\cjrco_ro");

			DirectoryInfo[] directories = di.GetDirectories();

			foreach (DirectoryInfo directory in directories) {

				if (string.Compare(directory.Name, "X1100000") > 0 && directory.Name.Length == 8) {

					int dateNum = 20150701;

					while (dateNum++ < 20150718) {

						DirectoryInfo tlogDirectory = directory.GetDirectories(dateNum.ToString()).FirstOrDefault();

						if (tlogDirectory != null) {
							string fileName = dateNum.ToString() + ".TransHist.xml";

							if (tlogDirectory.GetFiles(fileName).Count() == 0) {

								_sb.Append("Unit:   " + directory.Name + " is missing TLog file for:  " + fileName);
								_sb.Append("\r\n");
							}
						}
						else {
							_sb.Append("Unit:   " + directory.Name + " is missing TLog directroy for:  " + dateNum.ToString());
							_sb.Append("\r\n");

						}
					}
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		private List<string> GetCarlsCompanyStores() {

			List<string> companyStores = new List<string>();

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetCarlsCompanyStores);

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionStringProd);
			DataTable dt = dac.ExecuteQuery(sql);

			foreach (DataRow dr in dt.Rows) {

				companyStores.Add("X" + dr["c_unit_no"].ToString());
			}



			return companyStores;
		}

	}
}

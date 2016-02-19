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
	public partial class Form5 : Form {
		public Form5() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

			List<string> win7NodeList = Win7NodeUtility.GetWin7NodeList();

			foreach (string win7Node in win7NodeList) {

				Win7Unit win7Unit = new Win7Unit();

				win7Unit.RestaurantNo = Convert.ToInt32(win7Node.Substring(1));
				win7Unit.UnitNumber = win7Node.Trim();

				textBox1.Text = win7Unit.UnitNumber;
				Application.DoEvents();

				dataContext.Win7Units.InsertOnSubmit(win7Unit);
				dataContext.SubmitChanges();
			}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;


			//SmartReportsProcessor srp = new SmartReportsProcessor();
			//srp.RunAgainstStoreArchive();

			//string sql = textBox1.Text;

			//DataAccess dac = new DataAccess(AppSettings.IRISConnectionString);

			//DataTable dt = dac.ExecuteQuery(sql);

			//dt.TableName = "DayPartSales";

			//dt.WriteXmlSchema("C:\\temp\\DayPartSales.xsd");

			string fileDirectory = "D:\\xdata1\\cmsos2\\ckenode";

			DirectoryInfo di = new DirectoryInfo(fileDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				FileInfo[] files = directory.GetFiles("*.fin");

				foreach (FileInfo file in files) {

					string targetPath = Path.Combine(file.DirectoryName, "mtier", file.Name);
					file.CopyTo(targetPath, true);
				}
			}



			button2.Enabled = true;
		}

		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			string fileDirectory = "D:\\xdata1\\cmsos2\\ckenode";

			DirectoryInfo di = new DirectoryInfo(fileDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				FileInfo[] files = directory.GetFiles("*.fin");

				foreach (FileInfo file in files) {

					ZipFile zippy = new ZipFile(directory.Name + ".zip");
					zippy.AddFile(file.FullName, "");

					string targetPath = Path.Combine(file.DirectoryName, "mtier", zippy.Name);

					zippy.Save(targetPath);

				}
			}

			button3.Enabled = true;
		}

		private void button4_Click(object sender, EventArgs e) {

			button4.Enabled = false;

			string fileDirectory = "D:\\xdata1\\cmsos2\\ckenode";

			DirectoryInfo di = new DirectoryInfo(fileDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				string srcPath = Path.Combine(@"\\xdata1\cmsos2\ckenode" , directory.Name);

				DirectoryInfo srcDirectory = new DirectoryInfo(srcPath);

				FileInfo[] files = srcDirectory.GetFiles("*.pol");

				foreach (FileInfo file in files) {
					string destPath = Path.Combine(directory.FullName, file.Name);
					file.CopyTo(destPath, true);
				}

				files = srcDirectory.GetFiles("*.fin");

				foreach (FileInfo file in files) {
					string destPath = Path.Combine(directory.FullName, file.Name);
					file.CopyTo(destPath, true);
				}
	
			}

			button4.Enabled = false;
		}

		private void button5_Click(object sender, EventArgs e) {

			button5.Enabled = false;

			INFO2000DataContext dataContext = new INFO2000DataContext();

			List<FranchiseRestaurant> frList = dataContext.FranchiseRestaurants.ToList();

			StringBuilder sb = new StringBuilder();

			string template = "INSERT INTO [dbo].[FranchiseRestaurant] ([Franchisee], [RestaurantNumber]) VALUES('!FRANCHISEE', !RESTAURANT_NUMBER)\r\n\r\n";

			foreach (FranchiseRestaurant fr in frList) {

				sb.Append(template);
				sb.Replace("!FRANCHISEE", fr.Franchisee);
				sb.Replace("!RESTAURANT_NUMBER", fr.RestaurantNumber.ToString());


			}


			textBox1.Text = sb.ToString();
			button5.Enabled = true;
		}
	}
}

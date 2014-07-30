using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form6 : Form {
		public Form6() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			BuildInsertPaidAccountSql();

			//// set up domain context
			//PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

			//// find a user
			//UserPrincipal user = UserPrincipal.FindByIdentity(ctx, "mbacon");

			//if (user != null) {
			//	// do something here....     
			//	var usersSid = user.Sid;

			//	// not sure what you mean by "username" - the "DisplayName" ? The "SAMAccountName"??
			//	var username = user.DisplayName;
			//	var userSamAccountName = user.SamAccountName;
			//}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void BuildInsertPaidAccountSql() {

			string template = "INSERT INTO [Config].[PaidAccount]([PaidAccountID], [Description] ,[AccountNumber])\r\n  VALUES(!PAID_ACCOUNT_ID,'!DESCRIPTION',!ACCOUNT_NUMBER)";


			StringBuilder sb = new StringBuilder();

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

			int idx = 1;

			foreach (string line in lines) {

				string[] items = line.Split(new char[] { '\t' });

				sb.Append(template);
				sb.Replace("!PAID_ACCOUNT_ID", idx.ToString());
				sb.Replace("!DESCRIPTION", items[0].Trim());
				sb.Replace("!ACCOUNT_NUMBER", items[1].Trim());

				sb.Append("\r\n\r\n");

				idx++;

			}

			textBox1.Text = sb.ToString();
		}

		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;


			//string filePath = @"C:\TestData\MenudataInHFSDB";
			string filePath = @"C:\TestData";

			DirectoryInfo di = new DirectoryInfo(filePath);

			FileInfo[] files = di.GetFiles("*.log");


			foreach (FileInfo file in files) {

				ProcessFile(file);
			}


			button2.Enabled = true;
		}

		private void ProcessFile(FileInfo file) {


			DataAnalysis2DataContext dataContext = new DataAnalysis2DataContext();

			int restaturantNo = Convert.ToInt32(Path.GetFileNameWithoutExtension(file.Name.Substring(1)));

			using (StreamReader sr = new StreamReader(file.OpenRead())){

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					if (line.Trim() != "") {

						string temp = line.Substring(0, 12).Trim();

						int orderId = 0;

						if (int.TryParse(temp, out orderId)) {

							temp = line.Substring(12).Trim();

							int menuItemId = Convert.ToInt32(temp);

							BadOrderItem bo = new BadOrderItem();
							bo.MenuItemId = menuItemId;
							bo.OrderId = orderId;
							bo.RestaurantNo = restaturantNo;

							dataContext.BadOrderItems.InsertOnSubmit(bo);
							dataContext.SubmitChanges();
						}
					}
				}
			}
		}
	}
}

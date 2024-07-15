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
	public partial class Form21 : Form {
		public Form21() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------
		//---------------------------------------------------------------------------------------------------
		private HFSDBData _data = new HFSDBData();

		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DateTime startDate = new DateTime(2015,3, 23);
			DateTime endDate = new DateTime(2015, 3, 23);


			ProcessSalesData(startDate, endDate);

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			DirectoryInfo di = new DirectoryInfo( @"X:\Program Files\CKE\CkeFileTransfer\Archive");

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string line in lines) {

				DateTime businessDate = Convert.ToDateTime(line);

				string searchPattern = "*" + businessDate.ToString("yyyyMMdd") + "*.zip";

				FileInfo[] files = di.GetFiles(searchPattern);

				foreach (FileInfo file in files) {

					string targetPath = Path.Combine("C:\\Temp357", file.Name);

					Logger.Write("Copying file: " + targetPath);

					file.CopyTo(targetPath, true);
				}

				searchPattern = "Har*" + businessDate.ToString("yyyyMMdd") + "*.txt";

				files = di.GetFiles(searchPattern);

				foreach (FileInfo file in files) {

					string targetPath = Path.Combine("C:\\Temp357", file.Name);

					Logger.Write("Copying file: " + targetPath);

					file.CopyTo(targetPath, true);
				}
			}


			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void BuildFile(DateTime businessDate) {

			StringBuilder sb = new StringBuilder();

			sb.Append("USE HFSDB\r\n\r\n");

			sb.Append("SET IDENTITY_INSERT Detail.PosOrder ON\r\n");
			GetOrders(businessDate, sb);
			sb.Append("SET IDENTITY_INSERT Detail.PosOrder OFF\r\n");

			sb.Append("SET IDENTITY_INSERT Detail.OrderItem ON\r\n");
			GetOrderItems(businessDate, sb);
			sb.Append("SET IDENTITY_INSERT Detail.OrderItem OFF\r\n");

			sb.Append("SET IDENTITY_INSERT Detail.OrderItemModifier ON\r\n");
			GetOrderItemModifiers(businessDate, sb);
			sb.Append("SET IDENTITY_INSERT Detail.OrderItemModifier OFF\r\n");


			string filePath = "C:\\TestData\\HardeesFifoTestData\\FifoTestData_" + businessDate.ToString("yyyy-MM-dd") + ".sql";

			if (File.Exists(filePath)) {
				File.Delete(filePath);
			}

			using (StreamWriter sw = new StreamWriter(filePath)) {

				sw.Write(sb.ToString());
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}
		//---------------------------------------------------------------------------------------------------
		private void GetOrders(DateTime businessDate, StringBuilder sb) {



			DataTable dt = _data.GetPosOrder(businessDate);

			int counter = 0;


			foreach (DataRow dr in dt.Rows) {

				sb.Append(SqlTemplateBroker.Load(SqlTemplateId.InsertPosOrder));
				sb.Append("\r\n\r\nGO\r\n\r\n");

				sb.Replace("!ORDERID",  dr["OrderID"].ToString());
				sb.Replace("!TERMINALID", dr["TerminalID"].ToString());
				sb.Replace("!ORDERNUMBER", dr["OrderNumber"].ToString());
				sb.Replace("!ORDERDATE", dr["OrderDate"].ToString());
				sb.Replace("!EMPLOYEEID", dr["EmployeeID"].ToString());
				sb.Replace("!DESTINATIONID", dr["DestinationID"].ToString());
				sb.Replace("!ORDERTYPE", dr["OrderType"].ToString());
				sb.Replace("!TOTALNONFOODSALE", dr["TotalNonFoodSale"].ToString());
				sb.Replace("!TOTALFOODSALE", dr["TotalFoodSale"].ToString());
				sb.Replace("!TOTALTAXABLESALE", dr["TotalTaxableSale"].ToString());
				sb.Replace("!TAXCOLLECTED", dr["TaxCollected"].ToString());
				sb.Replace("!PROMOAMOUNT", dr["PromoAmount"].ToString());
				sb.Replace("!PROMOCOUNT", dr["PromoCount"].ToString());
				sb.Replace("!COUPONAMOUNT", dr["CouponAmount"].ToString());
				sb.Replace("!COUPONCOUNT", dr["CouponCount"].ToString());
				sb.Replace("!FREEAMOUNT", dr["FreeAmount"].ToString());
				sb.Replace("!FREECOUNT", dr["FreeCount"].ToString());
				sb.Replace("!FCTTRANID", dr["FCTTranID"].ToString());
				sb.Replace("!CENTSOFFAMOUNT", dr["CentsOffAmount"].ToString());
				sb.Replace("!CENTSOFFCOUNT", dr["CentsOffCount"].ToString());
				sb.Replace("!CREDITCARDAMOUNT", dr["CreditCardAmount"].ToString());
				sb.Replace("!GIFTCARDSALEAMOUNT", dr["GiftCardSaleAmount"].ToString());
				sb.Replace("!GIFTCARDSALECOUNT", dr["GiftCardSaleCount"].ToString());
				sb.Replace("!GIFTCARDREDEMAMOUNT", dr["GiftCardRedemAmount"].ToString());
				sb.Replace("!GIFTCARDREDEMCOUNT", dr["GiftCardRedemCount"].ToString());
				sb.Replace("!DEBITCARDAMOUNT", dr["DebitCardAmount"].ToString());
				sb.Replace("!CASHBACKAMOUNT", dr["CashBackAmount"].ToString());


				counter++;

				if (counter % 20 == 0) {
					textBox1.Text += counter.ToString() + " Orders Processed\r\n";
					Application.DoEvents();
				}


			}
		}
		//---------------------------------------------------------------------------------------------------
		private void GetOrderItemModifiers(DateTime businessDate, StringBuilder sb) {

			DataTable dt = _data.GetOrderItemModifier(businessDate);

			int counter = 0;

			
			foreach (DataRow dr in dt.Rows) {

				sb.Append(SqlTemplateBroker.Load(SqlTemplateId.InsertOrderItemModifier));
				sb.Append("\r\n\r\nGO\r\n\r\n");
				sb.Replace("!ORDERITEMMODIFIERID",  dr["OrderItemModifierID"].ToString());
				sb.Replace("!ORDERITEMID",  dr["OrderItemID"].ToString());
				sb.Replace("!MENUITEMID",  dr["MenuItemID"].ToString());
				sb.Replace("!MODIFIERACTIONID",  dr["ModifierActionId"].ToString());
				sb.Replace("!QUANTITY",  dr["Quantity"].ToString());

				counter++;

				if (counter % 20 == 0) {
					textBox1.Text += counter.ToString() + " Modifiers Processed\r\n";
					Application.DoEvents();
				}



			}
		}
		//---------------------------------------------------------------------------------------------------
		private void GetOrderItems(DateTime businessDate, StringBuilder sb) {

			DataTable dt = _data.GetOrderItem(businessDate);

			int counter = 0;

			foreach (DataRow dr in dt.Rows) {

				sb.Append(SqlTemplateBroker.Load(SqlTemplateId.InsertOrderItem));
				sb.Append("\r\n\r\nGO\r\n\r\n");

				sb.Replace("!ORDERITEMID",  dr["OrderItemID"].ToString());
				sb.Replace("!ORDERID",  dr["OrderID"].ToString());
				sb.Replace("!MENUITEMID",  dr["MenuItemID"].ToString());
				sb.Replace("!ORDERITEMTYPE",  dr["OrderItemType"].ToString());
				sb.Replace("!QUANTITY",  dr["Quantity"].ToString());
				sb.Replace("!PRICE",  dr["Price"].ToString());
				sb.Replace("!COST",  dr["Cost"].ToString());
				sb.Replace("!DISCOUNTAMOUNT",  dr["DiscountAmount"].ToString());

				counter++;

				if (counter % 20 == 0) {
					textBox1.Text += counter.ToString() + " Order Items Processed\r\n";
					Application.DoEvents();
				}


			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessSalesData(DateTime startDate, DateTime endDate) {


			while (startDate <= endDate) {

				BuildFile(startDate);

				startDate = startDate.AddDays(1);
			}
		}

		private void Form21_Load(object sender, EventArgs e)
		{
		
		}

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
		//---------------------------------------------------------------------------------------------------


	}
}

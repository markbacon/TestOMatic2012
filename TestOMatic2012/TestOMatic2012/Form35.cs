using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form35 : Form {
		public Form35() {
			InitializeComponent();
		}

		private List<Unit2ferSales> _unit2ferSales = new List<Unit2ferSales>();

		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;





			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;


			DataTable dt = Get2ferSales();

			foreach (DataRow dr in dt.Rows) {

				ProcessUnitSales(dr);
			}

			StringBuilder sb = new StringBuilder();
			//sb.Append("Unit\tBBQ Chicken\tFamous Star\tTurkey Burger\tWestern Bacon\r\n");

			foreach (Unit2ferSales u2s in _unit2ferSales) {
				sb.Append(u2s.UnitNumber);
				sb.Append("\t");
				sb.Append(u2s.BbqChickenSales);
				sb.Append("\t");
				sb.Append(u2s.FamousStarSales);
				sb.Append("\t");
				sb.Append(u2s.TurkeyBurgerSales);
				sb.Append("\t");
				sb.Append(u2s.WesternBaconSales);
				sb.Append("\r\n");
			}


			textBox1.Text = sb.ToString();



			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private DataTable Get2ferSales() {

			string sql = SqlTemplateBroker.Load(SqlTemplateId.Get2ferSales);

			DataAccess dac = new DataAccess(AppSettings.HardeesTransactionDataConnectionString);

			return dac.ExecuteQuery(sql);
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessUnitSales(DataRow dr) {

			string unitNumber = dr["UnitNumber"].ToString();

			Unit2ferSales u2s = _unit2ferSales.Where(u => u.UnitNumber == unitNumber).SingleOrDefault();

			if (u2s == null) {

				u2s = new Unit2ferSales();
				u2s.UnitNumber = unitNumber;
				_unit2ferSales.Add(u2s);
			}

			int quantity = Convert.ToInt32(dr["Quantity"]);

			string itemNumber = dr["ItemNumber"].ToString();

			switch (itemNumber) {

				case "30947":
					u2s.WesternBaconSales += (2 * quantity);
					break;

				case "32049":
					u2s.WesternBaconSales += (2 * quantity);
					break;

				case "32051":
					u2s.FamousStarSales += (2 * quantity);
					break;

				case "32053":
					u2s.TurkeyBurgerSales += (2 * quantity);
					break;

				case "32055":
					u2s.BbqChickenSales += (2 * quantity);
					break;

				case "32057":
					u2s.WesternBaconSales += quantity;
					u2s.FamousStarSales += quantity;
					break;

				case "32059":
					u2s.WesternBaconSales += quantity;
					u2s.TurkeyBurgerSales += quantity;
					break;

				case "32061":
					u2s.WesternBaconSales += quantity;
					u2s.BbqChickenSales += quantity;					
					break;

				case "32063":
					u2s.FamousStarSales += quantity;
					u2s.TurkeyBurgerSales += quantity;
					break;

				case "32065":
					u2s.FamousStarSales += quantity;
					u2s.BbqChickenSales += quantity;					
					break;

				case "32067":
					u2s.TurkeyBurgerSales += quantity;
					u2s.BbqChickenSales += quantity;					
					break;

				case "32068":
					u2s.WesternBaconSales += (2 * quantity);
					break;

				case "32069":
					u2s.FamousStarSales += (2 * quantity);
					break;

				case "32070":
					u2s.TurkeyBurgerSales += (2 * quantity);
					break;

				case "32071":
					u2s.BbqChickenSales += (2 * quantity);
					break;

				case "32072":
					u2s.WesternBaconSales += quantity;
					u2s.FamousStarSales += quantity;
					break;

				case "32073":
					u2s.WesternBaconSales += quantity;
					u2s.TurkeyBurgerSales += quantity;
					break;

				case "32074":
					u2s.WesternBaconSales += quantity;
					u2s.BbqChickenSales += quantity;					
					break;

				case "32075":
					u2s.FamousStarSales += quantity;
					u2s.TurkeyBurgerSales += quantity;
					break;

				case "32076":
					u2s.FamousStarSales += quantity;
					u2s.BbqChickenSales += quantity;					
					break;

				case "32077":
					u2s.TurkeyBurgerSales += quantity;
					u2s.BbqChickenSales += quantity;					
					break;
			}
		}
	}
}

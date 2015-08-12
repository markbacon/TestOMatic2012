using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	class IdealUsageCalculator {

		public List<InventoryUsage> Run(DateTime weekEndDate) {

			DataTable dt = _data.GetIdealUsageJrList(weekEndDate);

			foreach (DataRow dr in dt.Rows) {

				ProcessJrData(dr);
			}

			return _usageList;
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private ImsData _data = new ImsData();

		private List<InventoryUsage> _usageList = new List<InventoryUsage>();

		//---------------------------------------------------------------------------------------------------------
		private void ProcessJrData(DataRow dr) {

			string menuItemId = dr["jrno"].ToString();
			int quantitySold = Convert.ToInt32(dr["QuantitySold"]);

			DataTable dtProducts = _data.GetProductsForMenuItem(menuItemId);

			foreach (DataRow drProduct in dtProducts.Rows) {

				string productNumber = drProduct["jp_child"].ToString();

				InventoryUsage usage = _usageList.Where(u => u.productNumber == productNumber).SingleOrDefault();

				if (usage == null) {
					usage = new InventoryUsage();
					usage.productNumber = productNumber;
					_usageList.Add(usage);
				}

				decimal recipeQuantityRequired = Convert.ToDecimal(drProduct["jp_psreq"]);

				usage.theoreticalQuantity += quantitySold * recipeQuantityRequired;
			}
		}
	}
}

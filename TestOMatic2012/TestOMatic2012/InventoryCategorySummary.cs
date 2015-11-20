using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {

	class InventoryCategorySummary {

		public string ClassCode = "";
		public string Description = "";

		public decimal BeginningAmount = 0;
		public decimal EndingAmount = 0;
		public decimal ActualAmount = 0;
		public decimal IdealAmount = 0;
		public decimal PurchaseAmount = 0;
		public decimal TransferInAmount = 0;
		public decimal TransferOutAmount = 0;
		public decimal MenuWaste = 0;
		public decimal ProductWaste = 0;


		public decimal VarianceAmount {

			get {
				return ActualAmount - IdealAmount;

			}
		}
		//---------------------------------------------------------------------------------------------------

	}
}

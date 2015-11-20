using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {
	
	
	class InventoryDetail {

		public string ItemNumber;
		public string ClassCode;
		public decimal ActualConversionFactor = 0;
		public decimal IdealConversionFactor = 0;

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
		private decimal _varianceAmount = 0;
		


	}
}

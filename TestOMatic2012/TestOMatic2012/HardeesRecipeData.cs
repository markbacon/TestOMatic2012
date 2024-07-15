using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {

	class HardeesRecipeData {

		public void SaveInventoryItem(InventoryItem invItem) {

			//TBL_Inv_ItemMaster itemMasterRecord = null;


			//if (invItem.ID != 0) {
			//	itemMasterRecord = _dataContext.TBL_Inv_ItemMasters.Where(i => i.tblItemId == invItem.ID).FirstOrDefault();
			//}

			//if (itemMasterRecord == null) {

			//	itemMasterRecord = new TBL_Inv_ItemMaster();

			//	itemMasterRecord.accountNo = invItem.AccountNumber;
			//	itemMasterRecord.actualConversion = invItem.ConversionFactor;
			//	itemMasterRecord.alphaSearchCode = invItem.AlphaCode;
			//	itemMasterRecord.alternateUM = invItem.AlternateUnitOfMeasure;
			//	itemMasterRecord.baseUnitOfMeasure = invItem.UnitOfMeasure;
			//	itemMasterRecord.demandType = invItem.DemandType;
			//	itemMasterRecord.description = invItem.Description;
			//	itemMasterRecord.idealConversion = invItem.IdealConversionFactor;
			//	itemMasterRecord.isReciped = invItem.IsReciped;
			//	itemMasterRecord.itemCode = invItem.Code;
			//	itemMasterRecord.statusCode = invItem.Status;
			//	itemMasterRecord.storageCode = invItem.StorageCode;

			//	_dataContext.TBL_Inv_ItemMasters.InsertOnSubmit(itemMasterRecord);
			//	_dataContext.SubmitChanges();


			//}



		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private HardeesRecipesDataContext _dataContext = new HardeesRecipesDataContext();

	}
}

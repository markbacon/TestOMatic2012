using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	
	class MenuItemData {


		public void InsertMenuConfigItem(int menuItemId, string description, string type, decimal price, string financialDepartment) {

			MenuConfigItem mci = new MenuConfigItem();

			mci.Description = description;

			mci.FinancialDepartment = financialDepartment;

			mci.MenuItemId = menuItemId;
			//mci.PLU = plu;
			mci.Price = price;
			mci.Type = type;



			_dataContext.MenuConfigItems.InsertOnSubmit(mci);
			_dataContext.SubmitChanges();
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private DataValidationDataContext _dataContext = new DataValidationDataContext();



	}
}

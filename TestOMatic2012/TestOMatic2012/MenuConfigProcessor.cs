using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestOMatic2012 {


	class MenuConfigProcessor {

		public void Load(string filePath) {

			_menuConfigItemList = new List<MenuConfigItem>();

			_xmlDoc = new XmlDocument();
			_xmlDoc.Load(filePath);

			LoadMenuConfigItemList(_xmlDoc);

			SaveMenuConfigItemList();

		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private List<MenuConfigItem> _menuConfigItemList;
		private XmlDocument _xmlDoc = null;

		private MenuItemData _data = new MenuItemData();

		//---------------------------------------------------------------------------------------------------
		private bool GetBooleanValue(string elementValue) {

			bool tempValue;
			bool returnValue = false;

			if (bool.TryParse(elementValue, out tempValue)) {
				returnValue = tempValue;
			}

			return returnValue;
		}
		//---------------------------------------------------------------------------------------------------
		private decimal GetDecimalValue(string elementValue) {

			decimal tempValue;
			decimal returnValue = -1;

			if (decimal.TryParse(elementValue, out tempValue)) {
				returnValue = tempValue;
			}

			return returnValue;
		}
		//---------------------------------------------------------------------------------------------------
		private int GetIntegerValue(string elementValue) {

			int tempValue;
			int returnValue = -1;

			if (int.TryParse(elementValue, out tempValue)) {
				returnValue = tempValue;
			}

			return returnValue;
		}
		//---------------------------------------------------------------------------------------------------
		private void LoadMenuConfigItemList(XmlDocument xmlDoc) {

			_menuConfigItemList = new List<MenuConfigItem>();

			string xpath = "/MenuConfig/AllItems/Item";

			XmlNodeList nodeList = xmlDoc.SelectNodes(xpath);

			foreach (XmlElement element in nodeList) {

				MenuConfigItem menuCfgItem = new MenuConfigItem();

				menuCfgItem.AutoModify = GetBooleanValue(element.GetAttribute("AutoModify"));
				menuCfgItem.Classification = element.GetAttribute("Classification");
				menuCfgItem.ClassificationId = GetIntegerValue(element.GetAttribute("ClassificationId"));
				menuCfgItem.Department = element.GetAttribute("Department");
				menuCfgItem.DepartmentId = GetIntegerValue(element.GetAttribute("DepartmentId"));
				menuCfgItem.Discountable = GetBooleanValue(element.GetAttribute("Discountable"));
				menuCfgItem.FinancialClassification = element.GetAttribute("FinancialClassification");
				menuCfgItem.FinancialClassificationId = GetIntegerValue(element.GetAttribute("FinancialClassificationId"));
				menuCfgItem.FinancialDepartment = element.GetAttribute("FinancialDepartment");
				menuCfgItem.FinancialDepartmentId = GetIntegerValue(element.GetAttribute("FinancialDepartmentId"));
				menuCfgItem.Description = element.GetAttribute("Description");
				menuCfgItem.MenuItemId = GetIntegerValue(element.GetAttribute("MenuItemId"));
				menuCfgItem.Name = element.GetAttribute("Name");
				menuCfgItem.PLU = GetIntegerValue(element.GetAttribute("PLU"));
				menuCfgItem.Type = element.GetAttribute("Type");
				menuCfgItem.NonTaxable = GetBooleanValue(element.GetAttribute("NonTaxable"));
				menuCfgItem.Price = GetDecimalValue(element.GetAttribute("Price"));
				menuCfgItem.TaxSysId = GetIntegerValue(element.GetAttribute("TaxSysID"));

				_menuConfigItemList.Add(menuCfgItem);
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void SaveMenuConfigItemList() {

			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

			foreach (MenuConfigItem mci in _menuConfigItemList) {
				dataContext.MenuConfigItems.InsertOnSubmit(mci);
				dataContext.SubmitChanges();
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestOMatic2012 {
	
	
	class MenuConfigProcessor {

		public void Load(string filePath) {

			_xmlDoc = new XmlDocument();
			_xmlDoc.Load(filePath);

			string xpath = "/MenuConfig/AllItems/Item";

			XmlNodeList nodes = _xmlDoc.SelectNodes(xpath);

			foreach (XmlElement element in nodes) {

				SaveItem(element);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private XmlDocument _xmlDoc = null;

		private MenuItemData _data = new MenuItemData();


		private void SaveItem(XmlElement element) {

			int menuItemId = Convert.ToInt32(element.GetAttribute("MenuItemId"));

			//int plu = 0;
			//string temp = element.GetAttribute("PLU");

			//if (!string.IsNullOrEmpty(temp)) {
			//	plu = Convert.ToInt32(temp);
			//}
			
			string description = element.GetAttribute("Description");
			string type = element.GetAttribute("Type");

			string temp = element.GetAttribute("Price").Trim();

			decimal price = 0;

			if (!string.IsNullOrEmpty(temp)) {

				if (!decimal.TryParse(temp, out price)) {
					price = -1;
				}
			}
			else {
				price = -1;
			}


			string financialDepartment = element.GetAttribute("FinancialDepartment");


			_data.InsertMenuConfigItem(menuItemId, description, type, price, financialDepartment);
		}
	}
}

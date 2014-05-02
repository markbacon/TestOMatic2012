using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestOMatic2012 {

	class SalaryDataReader {

		public SalaryDataReader() {

			_xmlDoc = new XmlDocument();
			_xmlDoc.Load("C:\\UnitData\\SalaryData.xml");
		}
		//---------------------------------------------------------------------------------------------------
		public string GetGeneralManagerJobCode() {

			string state = GetState();


			string xpath = "/SALARY_DATA/JOB_DETAIL[@CONCEPT_ID='50' and @STATE='" + state + "' and @JOB_TITLE='General Manager']";

			XmlElement element = (XmlElement)_xmlDoc.SelectSingleNode(xpath);

			if (element == null) {

				xpath = "/SALARY_DATA/JOB_DETAIL[@CONCEPT_ID='50' and @STATE='**' and @JOB_TITLE='General Manager']";
				element = (XmlElement)_xmlDoc.SelectSingleNode(xpath);
			}


			return element.GetAttribute("JOB_CODE");

		}
		//---------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------
		private XmlDocument _xmlDoc;

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		//---------------------------------------------------------------------------------------------------
		private string GetState() {

			StringBuilder sb = new StringBuilder();
			int i = GetPrivateProfileString("unitdata", "StateName", "", sb, 255, "C:\\UnitData\\UnitInfo.ini");

			return sb.ToString();
		}
	}
}

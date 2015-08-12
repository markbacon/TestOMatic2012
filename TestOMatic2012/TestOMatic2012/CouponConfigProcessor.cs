using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestOMatic2012 {
	
	
	class CouponConfigProcessor {

		public void Run(string configFilePath) {

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(configFilePath);

			string xpath = "/CouponCodes/Coupon";

			XmlNodeList couponNodes = xmlDoc.SelectNodes(xpath);

			foreach (XmlNode couponNode in couponNodes) {

				ProcessCouponNode(couponNode);
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessCouponNode(XmlNode couponNode) {

			int couponId = 0;
			string description = "";

			string xpath = "./Code";

			XmlNode dataNode = couponNode.SelectSingleNode(xpath);

			if (dataNode != null) {

				couponId = Convert.ToInt32(dataNode.InnerText);
			}

			xpath = "./Description";

			dataNode = couponNode.SelectSingleNode(xpath);

			if (dataNode != null) {

				description = dataNode.InnerText;
			}

			if (couponId != 0) {

				DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

				CouponData cd = dataContext.CouponDatas.Where(c => c.CouponId == couponId).FirstOrDefault();

				if (cd == null) {
					cd = new CouponData();

					cd.CouponId = couponId;
					cd.Description = description;

					dataContext.CouponDatas.InsertOnSubmit(cd);
					dataContext.SubmitChanges();

				}
				else {

					if (cd.Description != description) {
						cd = new CouponData();

						cd.CouponId = couponId;
						cd.Description = description;

						dataContext.CouponDatas.InsertOnSubmit(cd);
						dataContext.SubmitChanges();

					}
				}
			}
		}
	}
}

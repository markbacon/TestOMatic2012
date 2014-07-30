﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestOMatic2012 {

	class TransHistCouponProcessor {

		public void ProcessFile(string unitNumber, string filePath) {

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(filePath);

			string xpath = "//Transaction/ScannedCouponItems";

			XmlNodeList nodes = xmlDoc.SelectNodes(xpath);

			_dataContext.Dispose();
			_dataContext = new CouponDataContext();

			foreach (XmlNode node in nodes) {

				ProcessScannedCouponItemsNode(unitNumber, node);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		public void Run() {

			string tlogDirectory = "D:\\TLogs";

			DirectoryInfo di = new DirectoryInfo(tlogDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				//if (string.Compare(directory.Name, "x1501298") < 0) {
				//	Logger.Write("Skipping Directory: " + directory.FullName);
				//	continue;
				//}

				Logger.Write("Processing Directory: " + directory.FullName);

				FileInfo[] files = directory.GetFiles("20140722.transhist.xml");

				foreach (FileInfo file in files) {

					if (char.IsDigit(file.Name[0])) {

						int fileNum = Convert.ToInt32(file.Name.Substring(0, 8));

						if (fileNum > 20140721) {
							Logger.Write("Processing File: " + file.FullName);
							ProcessFile(directory.Name, file.FullName);
						}
					}
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private List<ScannedCouponItem> _couponItemList = new List<ScannedCouponItem>();

		private CouponDataContext _dataContext = new CouponDataContext();

		//---------------------------------------------------------------------------------------------------------
		private string GetNodeValue(string xpath, XmlNode node) {

			string nodeValue = "";


			XmlNode dataNode = node.SelectSingleNode(xpath);

			if (dataNode != null) {

				nodeValue = dataNode.InnerText;

			}

			return nodeValue;
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessScannedCouponItemsNode(string unitNumber, XmlNode node) {


			string xpath = "./DateTime";
			DateTime transTime = Convert.ToDateTime(GetNodeValue(xpath, node.ParentNode));

			xpath = "./Order/OrderData/Regnum";
			string registerNumber = GetNodeValue(xpath, node.ParentNode);

			xpath = "./Order/OrderData/Ordernum";
			string orderNumber = GetNodeValue(xpath, node.ParentNode);

			xpath = "./Order/OrderData/Empnum";
			string employeeNumber = GetNodeValue(xpath, node.ParentNode);

			xpath = "./Order/OrderData/Destination";
			string destination = GetNodeValue(xpath, node.ParentNode);

			foreach (XmlNode childNode in node.ChildNodes) {

				ScannedCoupon couponItem = new ScannedCoupon();

				couponItem.RestaurantNumber = unitNumber;
				couponItem.Destination = destination;
				couponItem.EmployeeNumber = employeeNumber;
				couponItem.RegisterNumber = registerNumber;
				couponItem.OrderNumber = orderNumber;
				couponItem.TransactionTime = transTime;

				xpath = "./Name";
				couponItem.MenuItemDescription = GetNodeValue(xpath, childNode);

				xpath = "./CouponId";
				couponItem.CouponId = GetNodeValue(xpath, childNode);

				xpath = "./Qty";
				couponItem.Quantity = Convert.ToInt32(GetNodeValue(xpath, childNode));

				xpath = "./Price";
				couponItem.Price = Convert.ToDecimal(GetNodeValue(xpath, childNode));


				//int count =
				//	(from s in _dataContext.ScannedCoupons
				//	 where s.RestaurantNumber == couponItem.RestaurantNumber
				//		&& s.OrderNumber == couponItem.OrderNumber
				//		&& s.TransactionTime == couponItem.TransactionTime
				//		&& s.RegisterNumber == couponItem.RegisterNumber
				//		&& s.CouponId == couponItem.CouponId
				//		&& s.Quantity == couponItem.Quantity
				//	 select s).Count();

				//if (count == 0) {
					_dataContext.ScannedCoupons.InsertOnSubmit(couponItem);
					_dataContext.SubmitChanges();
				//}
				//else {
				//	StringBuilder sb = new StringBuilder();
				//	sb.Append("Apparent duplicate entry found for unit: ");
				//	sb.Append(couponItem.RestaurantNumber);
				//	sb.Append(" Order Number: ");
				//	sb.Append(couponItem.OrderNumber);
				//	sb.Append(" Transaction Time: ");
				//	sb.Append(couponItem.TransactionTime.ToString("yyyy-MM-dd HH:mm:ss"));
				//	sb.Append(" Coupon Id: ");
				//	sb.Append(couponItem.CouponId);
				//	sb.Append(" Quantity: ");
				//	sb.Append(couponItem.Quantity);

				//	Logger.Write(sb.ToString());
				//}
			}
		}

	}
}

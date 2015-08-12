using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form28 : Form {
		public Form28() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private DataAnalysis2DataContext _dataContext = new DataAnalysis2DataContext();
		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string dirPath = @"C:\TestData\AscFiles";

			DirectoryInfo di = new DirectoryInfo(dirPath);

			DirectoryInfo[] directories = di.GetDirectories();

			foreach (DirectoryInfo directory in directories) {

				string warehouseName = directory.Name;

				FileInfo file = directory.GetFiles("recipes.asc").Single();

				ProcessRecipeFile(file);
			}

			//	FileInfo file = directory.GetFiles("prdnos.asc").Single();
			//	ProcessProductFile(file.FullName, warehouseName);
			//}

			//string filePath = @"C:\Users\mbacon\Documents\Analysis\Epass\Epass Items vs IMS Items\Taylorville\Prdnos.asc";
			//string warehouseName = "Taylorville";

			//ProcessProductFile(filePath, warehouseName);

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			List<HardeesProduct> productList =
				(from h in _dataContext.HardeesProducts
				 where h.ActiveCode != "N"
				 orderby Convert.ToInt32(h.ProductNumber)
				 select h).ToList();

			StringBuilder sb = new StringBuilder();


			foreach (HardeesProduct product in productList) {


				var hp2List =
					(from p in _dataContext.HardeesProductIIs
					 join w in _dataContext.HardeesWarehouses on p.WarehouseId equals w.HardeesWarehouseId
					 where p.ProductNumber == product.ProductNumber
					 select new {
						 w.Description,
						 p.CategoryCode
					 }).ToList();




				string categoryCode = "";
				bool writeCatCodeInfo = false;

				foreach (var hp2 in hp2List) {

					if (categoryCode == "") {
						categoryCode = hp2.CategoryCode;
					}
					else {
						if (hp2.CategoryCode != categoryCode) {
							writeCatCodeInfo = true;
							break;
						}
					}
				}

				if (writeCatCodeInfo) {

					sb.Append(product.ProductNumber);
					sb.Append("\t");
					sb.Append(product.Description);

					foreach (var hp2 in hp2List) {

						sb.Append("\t");
						sb.Append(hp2.Description);
						sb.Append("\t");
						sb.Append(hp2.CategoryCode);
					}

					sb.Append("\r\n");

				}
			}

			textBox1.Text = sb.ToString();
			
			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private int GetWarehouseId(string warehouseName) {

			HardeesWarehouse hw =
				(from h in _dataContext.HardeesWarehouses
				 where h.Description == warehouseName
				 select h).FirstOrDefault();

			if (hw == null) {

				hw = new HardeesWarehouse();
				hw.Description = warehouseName;

				_dataContext.HardeesWarehouses.InsertOnSubmit(hw);
				_dataContext.SubmitChanges();


			}

			return hw.HardeesWarehouseId;
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessProductLine(string line, int warehouseId) {

			string productNumber = line.Substring(1, 10).Trim();

			int hardeesProductId = _dataContext.HardeesProducts.Where(h => h.ProductNumber == productNumber).Select(h => h.HardeesProductId).SingleOrDefault();

			if (hardeesProductId == 0) {

				HardeesProduct hp = new HardeesProduct();
				hp.ActiveCode = line.Substring(0, 1);
				hp.ActualConversionFactor = Convert.ToDecimal(line.Substring(48, 9));
				hp.AltUnitOfMeasure = line.Substring(46, 2).Trim();
				hp.CategoryCode = line.Substring(68, 2).Trim();
				hp.Description = line.Substring(11, 30).Trim();
				hp.IdealConversionFactor = Convert.ToDecimal(line.Substring(57, 9));
				hp.ParentProductNumber = line.Substring(95).Trim();
				hp.ProductNumber = line.Substring(1, 10).Trim();

				string storageCode = line.Substring(41, 3).Trim();
				if (storageCode.Trim().Length == 0) {
					storageCode = "0";
				}

				hp.StorageCode = storageCode;

				string unitOfMeasure = line.Substring(44, 2).Trim();



				switch (unitOfMeasure) {
					case "0":
						unitOfMeasure = "EA";
						break;
					case "1":
						unitOfMeasure = "PO";
						break;
					case "2":
						unitOfMeasure = "GL";
						break;
				}

				hp.UnitOfMeasure = unitOfMeasure;

				_dataContext.HardeesProducts.InsertOnSubmit(hp);
				_dataContext.SubmitChanges();

				hardeesProductId = hp.HardeesProductId;
			}

			//HardeesProductWarehouse hpw =
			//	(from w in _dataContext.HardeesProductWarehouses
			//	 where w.HardeesProductId == hardeesProductId
			//		&& w.HardeesWarehouseId == warehouseId
			//	 select w).FirstOrDefault();

			//if (hpw == null) {
			//	hpw = new HardeesProductWarehouse();

			//	hpw.HardeesProductId = hardeesProductId;
			//	hpw.HardeesWarehouseId = warehouseId;

			//	_dataContext.HardeesProductWarehouses.InsertOnSubmit(hpw);
			//	_dataContext.SubmitChanges();
			//}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessProductLineII(string line, int warehouseId) {

			string productNumber = line.Substring(1, 10).Trim();

			int hardeesProductId = _dataContext.HardeesProductIIs.Where(h => h.ProductNumber == productNumber && h.WarehouseId == warehouseId).Select(h => h.HardeesProductId).SingleOrDefault();

			if (hardeesProductId == 0) {

				HardeesProductII hp = new HardeesProductII();
				hp.ActiveCode = line.Substring(0, 1);
				hp.ActualConversionFactor = Convert.ToDecimal(line.Substring(48, 9));
				hp.AltUnitOfMeasure = line.Substring(46, 2).Trim();
				hp.CategoryCode = line.Substring(68, 2).Trim();
				hp.Description = line.Substring(11, 30).Trim();
				hp.IdealConversionFactor = Convert.ToDecimal(line.Substring(57, 9));
				hp.ParentProductNumber = line.Substring(95).Trim();
				hp.ProductNumber = line.Substring(1, 10).Trim();

				string storageCode = line.Substring(41, 3).Trim();
				if (storageCode.Trim().Length == 0) {
					storageCode = "0";
				}

				hp.StorageCode = storageCode;

				string unitOfMeasure = line.Substring(44, 2).Trim();



				switch (unitOfMeasure) {
					case "0":
						unitOfMeasure = "EA";
						break;
					case "1":
						unitOfMeasure = "PO";
						break;
					case "2":
						unitOfMeasure = "GL";
						break;
				}

				hp.UnitOfMeasure = unitOfMeasure;
				hp.WarehouseId = warehouseId;

				_dataContext.HardeesProductIIs.InsertOnSubmit(hp);
				_dataContext.SubmitChanges();

				hardeesProductId = hp.HardeesProductId;
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessProductFile(string filePath, string warehouseName) {


			int warehouseId = GetWarehouseId(warehouseName);

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine().Trim();

					if (!string.IsNullOrEmpty(line)) {

						ProcessProductLine(line, warehouseId);
					}
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessRecipeFile(FileInfo recipeFile) {


			using (StreamReader sr = recipeFile.OpenText()) {


				while (sr.Peek() != -1) {

					string line = sr.ReadLine().Trim();

					if (!string.IsNullOrEmpty(line)) {

						string itemCode = line.Substring(21, 5);

						if (_dataContext.RecipedItems.Where(r => r.RecipedItemCode == itemCode).Count() == 0) {

							RecipedItem ri = new RecipedItem();
							ri.RecipedItemCode = itemCode;

							_dataContext.RecipedItems.InsertOnSubmit(ri);
							_dataContext.SubmitChanges();
						}
					}
				}
			}
		}
	}
}

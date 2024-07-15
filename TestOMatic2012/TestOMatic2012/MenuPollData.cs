using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestOMatic2012 {


	class MenuPollData {

		public void Run() {

			string directoryName = "C:\\CkeMixTest\\X1506095";

			DirectoryInfo di = new DirectoryInfo(directoryName);

			List<FileInfo> fileList = di.GetFiles("*mix.pol").ToList();

			foreach (FileInfo file in fileList) {


				ProcessFile(file);

			}
		}
		//---------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------
		private enum MenuMixCvsPosition {
			MixLiteral,
			Department,
			ItemName,
			Price,
			Quantity,
			TotalAmount,
			PercentSalesLiteral,
			PercentDepartmentLiteral,
			FillerLiteral,
			MenuItemCode
		}

		private const string PATTERN = @",(?!(?<=(?:^|,)\s*\x22(?:[^\x22]|\x22\x22|\\\x22)*,)(?:[^\x22]|\x22\x22|\\\x22)*\x22\s*(?:,|$))";
		private Regex _regex = new Regex(PATTERN);


		private DataAnalysisDataContext _dataContext = new DataAnalysisDataContext();
		//---------------------------------------------------------------------------------------------------
		private int CreateMixFileRecord(string filePath, DateTime businessDate) {

			MenuMixFile mixFile = new MenuMixFile();

			mixFile.CreateDate = DateTime.Now;
			mixFile.FileDate = businessDate;
			mixFile.FileName = Path.GetFileName(filePath);

			_dataContext.MenuMixFiles.InsertOnSubmit(mixFile);
			_dataContext.SubmitChanges();

			return mixFile.MenuMixFileId;
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo file) {

			string filePath = "";
			DateTime businessDate;
			int mixFileId = 0;

			using (StreamReader sr = new StreamReader(file.OpenRead())) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					if (line.StartsWith("\"FILENAME\"")) {

						filePath = line.Split(new char[] { ',' })[1].Replace("\"", "").Trim();
					}

		
					if (line.StartsWith("\"DATE\"")) {

						string temp = line.Split(new char[] { ',' })[1].Replace("\"", "").Trim();

						businessDate = DateTime.ParseExact(temp, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

						mixFileId = CreateMixFileRecord(filePath, businessDate);

					}


					if (line.StartsWith("\"MIX\"")) {

						ProcessLine(mixFileId, line);
					}


				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessLine(int fileId, string line) {

			int menuItemId = 0;

			string[] items = _regex.Split(line);

			for (int i = 0; i < items.Length; i++) {
				items[i] = items[i].Replace("\"", "").Trim();
			}

			string menuItemCode = items[(int)MenuMixCvsPosition.MenuItemCode].Trim();


			MenuMixItem mixItem =
				(from m in _dataContext.MenuMixItems
				 where m.MenuItemCode == menuItemCode
				 select m).FirstOrDefault();

			if (mixItem != null) {

				menuItemId = mixItem.MenuMixItemId;
			}
			else {
				mixItem = new MenuMixItem();

				mixItem.CreateDate = DateTime.Now;
				mixItem.Description = items[(int)MenuMixCvsPosition.ItemName].Replace("\"", "").Trim();
				mixItem.MenuItemCode = menuItemCode;

				_dataContext.MenuMixItems.InsertOnSubmit(mixItem);
				_dataContext.SubmitChanges();

				menuItemId = mixItem.MenuMixItemId;
			}

			MenuMixPollItem pollItem = new MenuMixPollItem();

			pollItem.CreateDate = DateTime.Now;
			pollItem.MenuMixFileId = fileId;
			pollItem.MenuMixItemId = menuItemId;
			pollItem.Price = Convert.ToDecimal(items[(int)MenuMixCvsPosition.Price]);
			pollItem.Quantity = Convert.ToInt32(items[(int)MenuMixCvsPosition.Quantity]);
			pollItem.TotalAmount = Convert.ToDecimal(items[(int)MenuMixCvsPosition.TotalAmount]);
			pollItem.UnitNumber = "1500717";

			_dataContext.MenuMixPollItems.InsertOnSubmit(pollItem);
			_dataContext.SubmitChanges();
		}
	}
}

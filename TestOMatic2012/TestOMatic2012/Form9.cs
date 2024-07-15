using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace TestOMatic2012 {
	public partial class Form9 : Form {
		public Form9() {
			InitializeComponent();

			Logger.LoggerWrite += form9_onLoggerWrite;

		}
		//---------------------------------------------------------------------------------------------------------
		//---------------------------------------------------------------------------------------------------------
		private DataAnalysisDataContext _dataContext = new DataAnalysisDataContext();

		private const string PATTERN = @",(?!(?<=(?:^|,)\s*\x22(?:[^\x22]|\x22\x22|\\\x22)*,)(?:[^\x22]|\x22\x22|\\\x22)*\x22\s*(?:,|$))";
		private Regex _regex = new Regex(PATTERN);


		private enum FinFileLineCsvPosition {
            BusinessDate = 0,
            UnitNumber = 3,
            Index = 4,
            Count = 10,
            Amount = 11
        }


		private enum FinFileDataCsvPosition {
			UnitNumber,
			BusinessDate,
			AfternoonAmount,
			AfternoonCount,
			BreakfastAmount,
			BreakfastCount,
			CashDepositAmount,
			CreditCardAmount,
			DeliveryAmount,
			DineInAmount,
			DinnerAmount,
			DinnerCount,
			DriveThruAmount,
			EarlyBreakfastAmount,
			EarlyBreakfastCount,
			GraveyardAmount,
			GraveyardCount,
			LateNightAmount,
			LateNightCount,
			LunchAmount,
			LunchCount,
			NetSales,
			NetSalesCount,
			SalesTax,
			TaxableSales,
			ToGoAmount
		}



		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

            List<string> unitList = new List<string>();

			//unitList.Add("1100004");
			//unitList.Add("1100013");
			//unitList.Add("1100097");
			//unitList.Add("1100162");
			//unitList.Add("1100171");
			//unitList.Add("1100321");
			//unitList.Add("1100650");
			//unitList.Add("1101207");
			//unitList.Add("1101365");
			unitList.Add("1502998");
			unitList.Add("1505997");


			//using (StreamReader sr = new StreamReader("C:\\temp\\CkeBrinkUnits.txt")) {

			//    while (sr.Peek() != -1) {

			//        unitList.Add(sr.ReadLine().Trim());

			//    }
			//}

			//string srcRootDirName = "C:\\Temp105.mm";
			//string srcRootDirName = "\\\\xdata1\\cmsos2\\ckenode";
			//string srcRootDirName = "C:\\StoreData";

			string srcRootDirName = "C:\\2024-07-02_FIN";

			//foreach (string unitNumber in unitList) {

			//             string srcPath = Path.Combine(srcRootDirName, "X" + unitNumber);

			//             DirectoryInfo unitDirectory = new DirectoryInfo(srcPath);

			//             if (unitDirectory.Exists) {

			//                 ProcessFinFileDirectory(unitDirectory);
			//             }

			//         }

			////string fileDirectory = "X:\\Brink Files";

			////ProcessFinFileDirectories(fileDirectory);
			ProcessFinFileDirectories(srcRootDirName);

			//DirectoryInfo di = new DirectoryInfo(@"C:\Store Data\X1500523");

			//ProcessFinFileDirectory(di);



			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			DateTime fooDate = new DateTime(2024, 7, 7);

			int bar = fooDate.DayOfYear;



			string template1 = "	[System.ComponentModel.DataAnnotations.Schema.Table(\"BRINK_API_SKYSQL.!TABLE_NAME\")]";
			string template2 = "     [Column(\"!COLUMN_NAME\")]";


			string srcDirName = "C:\\Temp88.mm";
			string destDirName = "C:\\Temp88.mm\\Tables";

			if (!Directory.Exists(destDirName)) {
				Directory.CreateDirectory(destDirName);
			}


			DirectoryInfo di = new DirectoryInfo(srcDirName);

			FileInfo[] csFiles = di.GetFiles("*.cs");

			foreach (FileInfo csFile in csFiles) {

				StringBuilder sb = new StringBuilder();
				bool classDeclarationFound = false;

				using (StreamReader sr = csFile.OpenText()) {

					while (sr.Peek() != -1) {

						string line = sr.ReadLine();


						if (line.Contains("class")) {
							classDeclarationFound = true;

							string[] items = line.Split(' ');

							sb.Append(template1);
							sb.Replace("!TABLE_NAME", items[1].Trim());
							sb.Append("\r\n");


							sb.Append(line);
							sb.Append("\r\n");
						}
						else {
							if (!classDeclarationFound) {
								sb.Append(line);
								sb.Append("\r\n");
							}
							else {
								string[] items = line.Trim().Split(' ');

								if (items.Length >= 3) {

									sb.Append(template2);
									sb.Replace("!COLUMN_NAME", items[2].Trim().ToUpper());
									sb.Append("\r\n");


									sb.Append(line);
									sb.Append("\r\n");
								}
								else {
									sb.Append(line);
									sb.Append("\r\n");

								}
							}
						}
					}
				}

				using (StreamWriter sw = new StreamWriter(Path.Combine(destDirName, csFile.Name))) {

					sw.Write(sb.ToString());
				}


			}

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private DateTime GetFinFileDate(FileInfo finFile) {

			DateTime finFileDate = new DateTime(1900, 1, 1);

			using (StreamReader sr = finFile.OpenText()) {

				string[] items = sr.ReadLine().Split(',');

				if (items.Length > 1) {
					if (!DateTime.TryParseExact(items[0], "MMddyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out finFileDate)) {
						finFileDate = new DateTime(1900, 1, 1);
					}
				}
			}

			return finFileDate;
		}
        //---------------------------------------------------------------------------------------------------------
        private void ProcessFinFile(FileInfo file) {

            CkePollFileDataContext dataContext = new CkePollFileDataContext();

            if (dataContext.FinFiles.Where(f => f.FileName == file.Name).Count() == 0) {

                Logger.Write("Processing FIN file: " + file.FullName);

                FinFile finFile = new FinFile();
                finFile.BusinessDate = GetFinFileDate(file);
                finFile.CreateDate = DateTime.Now;
                finFile.FileDate = file.CreationTime;
                finFile.FileName = file.Name;
                finFile.UnitNumber = file.Directory.Name.Substring(1);





                dataContext.FinFiles.InsertOnSubmit(finFile);
                dataContext.SubmitChanges();

                int finFileId = finFile.FinFileId;

                using (StreamReader sr = file.OpenText()) {

                    while (sr.Peek() != -1) {

                        string line = sr.ReadLine();

                        string[] items = _regex.Split(line);

                        items[5] = items[5].Replace("\"", "");

                        if (items[5].Length > 0) {

                            FinFileDetail finDetail = new FinFileDetail();

                            finDetail.Amount = Convert.ToDecimal(items[11]) / 100;
                            finDetail.Count = Convert.ToInt32(items[10]);
                            finDetail.FinFileId = finFileId;
                            finDetail.IndexNumber = Convert.ToInt32(items[4]);

                            if (dataContext.FinFileIndexes.Where(f => f.IndexNumber == finDetail.IndexNumber).Count() == 0) {
                                FinFileIndex finIndex = new FinFileIndex(); ;

                                finIndex.Description = items[5];
                                finIndex.IndexNumber = finDetail.IndexNumber;

                                dataContext.FinFileIndexes.InsertOnSubmit(finIndex);
                                dataContext.SubmitChanges();

                            }


                            dataContext.FinFileDetails.InsertOnSubmit(finDetail);
                            dataContext.SubmitChanges();
                        }
                    }
                }
            }
            else {
                Logger.Write("FIN File record already exists in database. File name: " + file.Name);
            }
        }
        //---------------------------------------------------------------------------------------------------------
        private void ProcessFinFileData(FileInfo file) {

			FinFileData fileData = new FinFileData();

			Stopwatch sw1 = new Stopwatch();
            sw1.Start();

            Logger.Write("Begin processing FIN file: " + file.FullName);

            using (StreamReader sr = file.OpenText()) {

                bool done = false;

                while (sr.Peek() != -1 && !done) {

                    string line = sr.ReadLine();

                    string[] items = _regex.Split(line);

                    for (int i = 0; i < items.Length; i++) {

                        items[i] = items[i].Replace("\"", "").Trim();
                    }

                    switch (items[(int)FinFileLineCsvPosition.Index]) {

                        case "3":
                            fileData.BusinessDate = DateTime.ParseExact(items[(int)FinFileLineCsvPosition.BusinessDate], "MMddyy", System.Globalization.CultureInfo.CurrentCulture);
                            fileData.UnitNumber = items[(int)FinFileLineCsvPosition.UnitNumber];
                            fileData.NetSales = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.NetSalesCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;

                        case "8":
                            fileData.TaxableSales = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
                            break;

                        case "9":
                            fileData.SalesTax = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
                            break;

                        case "24":
                        case "106":
                        case "107":
                        case "108":
                        case "109":
                        case "110":
                        case "111":
                        case "112":
                        case "113":
                        case "114":
                            decimal amount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;

                            if (items[(int)FinFileLineCsvPosition.Count] == "1" || items[(int)FinFileLineCsvPosition.Count] == "2") {
                                fileData.CashDepositAmount += amount;
                            }
                            else if (items[(int)FinFileLineCsvPosition.Count] != "0") {
                                fileData.CreditCardAmount += amount;
                            }
                            break;


                        case "61":
                            fileData.ToGoAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
                            break;

                        case "62":
                            fileData.DineInAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
                            break;

                        case "63":
                            fileData.DriveThruAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
                            break;

                        case "181":
                            fileData.DeliveryAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
                            break;

                        case "140":
                            fileData.GraveyardAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.GraveyardCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);

							break;


                        case "141":
                            fileData.EarlyBreakfastAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.EarlyBreakfastCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;


                        case "142":
                            fileData.BreakfastAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.BreakfastCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;


                        case "143":
                            fileData.LunchAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.LunchCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;


                        case "144":
                            fileData.AfternoonAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.AfternoonCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;


                        case "145":
                            fileData.DinnerAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.DinnerCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;


                        case "146":
                            fileData.LateNightAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.LateNightCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;


                        case "148":
                            //done = true;
                            break;
                    }
                }
            }

			using (CkePollFileDataContext dataContext = new CkePollFileDataContext()) {

				if (dataContext.FinFileDatas.Where(f => f.UnitNumber == fileData.UnitNumber && f.BusinessDate == fileData.BusinessDate).Count() == 0) {

					dataContext.FinFileDatas.InsertOnSubmit(fileData);
					dataContext.SubmitChanges();
				}
				else {
					Logger.Write("Record already exists for unit number: " + fileData.UnitNumber + " and business date: " + fileData.BusinessDate.ToString("yyyy-MM-dd"));
				}
			}

			Logger.Write("Finished processing FIN file: " + file.FullName + ". Elapsed time: " + sw1.Elapsed.ToString());

        }
        //---------------------------------------------------------------------------------------------------------
        private void ProcessFinFileDirectories(string finFileRootDirecory) {

			DirectoryInfo di = new DirectoryInfo(finFileRootDirecory);

			DirectoryInfo[] directories = di.GetDirectories("*");

			foreach (DirectoryInfo directory in directories) {

                ProcessFinFileDirectory(directory);
            }
        }
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFinFileDirectory(DirectoryInfo di) {

			FileInfo[] files = di.GetFiles("*fin");

			foreach (FileInfo file in files) {

				ProcessFinFileData(file);
			}

		}

		private void Form9_Load(object sender, EventArgs e) {

		}

		//---------------------------------------------------------------------------------------------------
		private void form9_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}

		private void textBox1_TextChanged(object sender, EventArgs e) {

			//if (textBox1.Text.Length > 2024) {
			//	textBox1.Text = "";
			//}


			if (textBox1.Text.Length > 0) {
				textBox1.SelectionStart = textBox1.Text.Length - 1;
				textBox1.ScrollToCaret();
				Application.DoEvents();
			}

		}

		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			DirectoryInfo di = new DirectoryInfo("C:\\2024-01-30_Fin");

			ProcessFinFileDirectories(di.FullName);


			button3.Enabled = true;
		}

		private void button4_Click(object sender, EventArgs e) {

			button4.Enabled = false;

			BrinkApiData brinkApiData = new BrinkApiData();
			DateTime businessDate = new DateTime(2019, 04, 25);
			DateTime endDate = new DateTime(2019, 12, 31);

			Stopwatch sw1 = new Stopwatch();
			sw1.Start();
			Logger.Write("Begin processing unit net sales for dates: " + businessDate.ToString("yyyy-MM-dd") + " to " + endDate.ToString("yyyy-MM-dd"));



			using (CkePollFileDataContext dataContext = new CkePollFileDataContext()) {

				while (businessDate <= endDate) {
					Stopwatch sw2 = new Stopwatch();
					sw2.Start();
					Logger.Write("Begin processing business date: " + businessDate.ToString("yyyy-MM-dd"));


					Stopwatch sw3 = new Stopwatch();
					sw3.Start();
					Logger.Write("Begin retrieving Brink order data.");
					List<CalendarDayNetSales> netSalesList = brinkApiData.GetCalendarDayNetSales(businessDate);
					Logger.Write("Finished retrieving Brink order data.  Elapsed time: " + sw3.Elapsed.ToString());

					Logger.Write(netSalesList.Count().ToString() + " records found for business date");

					foreach (CalendarDayNetSales netSales in netSalesList) {

						UnitNetSale unitNetSale = new UnitNetSale() {
							BusinessDate = netSales.BusinessDate,
							CreateDate = DateTime.Now,
							NetSales = netSales.NetSales,
							UnitNumber = netSales.UnitNumber

						};

						dataContext.UnitNetSales.InsertOnSubmit(unitNetSale);
						dataContext.SubmitChanges();



					}

					Logger.Write("Finished processing business date: " + businessDate.ToString("yyyy-MM-dd") + ".  Elapsed time: " + sw2.Elapsed.ToString());

					businessDate = businessDate.AddDays(1);
				}

			}

			Logger.Write("Finished processing unit net sales. Elapsed time: " + sw1.Elapsed.ToString());


			button4.Enabled = true;

		}

		private void button5_Click(object sender, EventArgs e) {

			button5.Enabled = false;

			List<string> unitList = new List<string>();

			using (StreamReader sr = new StreamReader("C:\\T3\\ArmUnits.txt")) {

				while (sr.Peek() != -1) {
					string unitNumer = sr.ReadLine().Split('\t')[1].Trim();

					if (unitList.Where(u => u == unitNumer).Count() == 0) {
						unitList.Add(unitNumer);
					}
				}
			}

			StringBuilder sb = new StringBuilder();

			bool isFirst = true;

			int counter = 0;

			foreach (string unitNumber in unitList) {

				if (!isFirst) {
					sb.Append(", ");
				}
				else {
					isFirst = false;
				}

				sb.Append("\"X");
				sb.Append(unitNumber);
				sb.Append("\"");

				if (++counter % 10 == 0) {
					sb.Append("\r\n");
				}
			}

			textBox1.Text = sb.ToString();



			button5.Enabled = true;
		}

		private string ProcessFinFileII(FileInfo finFile) {


			FinFileData fileData = new FinFileData();

			StringBuilder sb = new StringBuilder();

			Stopwatch sw1 = new Stopwatch();
			sw1.Start();

			Logger.Write("Begin processing FIN file: " + finFile.FullName);

			using (StreamReader sr = finFile.OpenText()) {

				bool done = false;

				while (sr.Peek() != -1 && !done) {

					string line = sr.ReadLine();

					string[] items = _regex.Split(line);

					for (int i = 0; i < items.Length; i++) {

						items[i] = items[i].Replace("\"", "").Trim();
					}

					switch (items[(int)FinFileLineCsvPosition.Index]) {

						case "3":
							fileData.BusinessDate = DateTime.ParseExact(items[(int)FinFileLineCsvPosition.BusinessDate], "MMddyy", System.Globalization.CultureInfo.CurrentCulture);
							fileData.UnitNumber = items[(int)FinFileLineCsvPosition.UnitNumber];
							fileData.NetSales = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.NetSalesCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;

						case "8":
							fileData.TaxableSales = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							break;

						case "9":
							fileData.SalesTax = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							break;

						case "24":
						case "106":
						case "107":
						case "108":
						case "109":
						case "110":
						case "111":
						case "112":
						case "113":
						case "114":
							decimal amount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;

							if (items[(int)FinFileLineCsvPosition.Count] == "1" || items[(int)FinFileLineCsvPosition.Count] == "2") {
								fileData.CashDepositAmount += amount;
							}
							else if (items[(int)FinFileLineCsvPosition.Count] != "0") {
								fileData.CreditCardAmount += amount;
							}
							break;


						case "61":
							fileData.ToGoAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							break;

						case "62":
							fileData.DineInAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							break;

						case "63":
							fileData.DriveThruAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							break;

						case "181":
							fileData.DeliveryAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							break;

						case "140":
							fileData.GraveyardAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.GraveyardCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);

							break;


						case "141":
							fileData.EarlyBreakfastAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.EarlyBreakfastCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;


						case "142":
							fileData.BreakfastAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.BreakfastCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;


						case "143":
							fileData.LunchAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.LunchCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;


						case "144":
							fileData.AfternoonAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.AfternoonCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;


						case "145":
							fileData.DinnerAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.DinnerCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;


						case "146":
							fileData.LateNightAmount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							fileData.LateNightCount = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);
							break;


						case "148":
							done = true;
							break;
					}
				}
			}

			sb.Append(fileData.UnitNumber);
			sb.Append('\t');
			sb.Append(fileData.BusinessDate.ToString("yyyy-MM-dd"));
			sb.Append('\t');
			sb.Append(fileData.AfternoonAmount);
			sb.Append('\t');
			sb.Append(fileData.AfternoonCount);
			sb.Append('\t');
			sb.Append(fileData.BreakfastAmount);
			sb.Append('\t');
			sb.Append(fileData.BreakfastCount);
			sb.Append('\t');
			sb.Append(fileData.CashDepositAmount);
			sb.Append('\t');
			sb.Append(fileData.CreditCardAmount);
			sb.Append('\t');
			sb.Append(fileData.DeliveryAmount);
			sb.Append('\t');
			sb.Append(fileData.DineInAmount);
			sb.Append('\t');
			sb.Append(fileData.DinnerAmount);
			sb.Append('\t');
			sb.Append(fileData.DinnerCount);
			sb.Append('\t');
			sb.Append(fileData.DriveThruAmount);
			sb.Append('\t');
			sb.Append(fileData.EarlyBreakfastAmount);
			sb.Append('\t');
			sb.Append(fileData.EarlyBreakfastCount);
			sb.Append('\t');
			sb.Append(fileData.GraveyardAmount);
			sb.Append('\t');
			sb.Append(fileData.GraveyardCount);
			sb.Append('\t');
			sb.Append(fileData.LateNightAmount);
			sb.Append('\t');
			sb.Append(fileData.LateNightCount);
			sb.Append('\t');
			sb.Append(fileData.LunchAmount);
			sb.Append('\t');
			sb.Append(fileData.LunchCount);
			sb.Append('\t');
			sb.Append(fileData.NetSales);
			sb.Append('\t');
			sb.Append(fileData.NetSalesCount);
			sb.Append('\t');
			sb.Append(fileData.SalesTax);
			sb.Append('\t');
			sb.Append(fileData.TaxableSales);
			sb.Append('\t');
			sb.Append(fileData.ToGoAmount);



			return sb.ToString();
		}

		private void button6_Click(object sender, EventArgs e) {

			button6.Enabled = false;

			using (StreamReader sr = new StreamReader("C:\\Temp\\2023B_FinFile.txt")) {

				while (sr.Peek() != -1) {

					string[] items = sr.ReadLine().Split('\t');

					FinFileData ffd = new FinFileData() {
						UnitNumber = items[(int)FinFileDataCsvPosition.UnitNumber],
						BusinessDate = Convert.ToDateTime(items[(int)FinFileDataCsvPosition.BusinessDate]),
						AfternoonAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.AfternoonAmount]),
						AfternoonCount = Convert.ToInt32(items[(int)FinFileDataCsvPosition.AfternoonCount]),
						BreakfastAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.BreakfastAmount]),
						BreakfastCount = Convert.ToInt32(items[(int)FinFileDataCsvPosition.BreakfastCount]),
						CashDepositAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.CashDepositAmount]),
						CreditCardAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.CreditCardAmount]),
						DeliveryAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.DeliveryAmount]),
						DineInAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.DineInAmount]),
						DinnerAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.DinnerAmount]),
						DinnerCount = Convert.ToInt32(items[(int)FinFileDataCsvPosition.DinnerCount]),
						DriveThruAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.DriveThruAmount]),
						EarlyBreakfastAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.EarlyBreakfastAmount]),
						EarlyBreakfastCount = Convert.ToInt32(items[(int)FinFileDataCsvPosition.EarlyBreakfastCount]),
						GraveyardAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.GraveyardAmount]),
						GraveyardCount = Convert.ToInt32(items[(int)FinFileDataCsvPosition.GraveyardCount]),
						LateNightAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.LateNightAmount]),
						LateNightCount = Convert.ToInt32(items[(int)FinFileDataCsvPosition.LateNightCount]),
						LunchAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.LunchAmount]),
						LunchCount = Convert.ToInt32(items[(int)FinFileDataCsvPosition.LunchCount]),
						NetSales = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.NetSales]),
						NetSalesCount = Convert.ToInt32(items[(int)FinFileDataCsvPosition.NetSalesCount]),
						SalesTax = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.SalesTax]),
						TaxableSales = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.TaxableSales]),
						ToGoAmount = Convert.ToDecimal(items[(int)FinFileDataCsvPosition.ToGoAmount])
					};

					Logger.Write("Saving FIN record for unit number: " + ffd.UnitNumber + " and business date: " + ffd.BusinessDate.ToString("yyyy-MM-dd"));

					using (CkePollFileDataContext dataContext = new CkePollFileDataContext()) {

						dataContext.FinFileDatas.InsertOnSubmit(ffd);
						dataContext.SubmitChanges();
					}

				}
			}

			button6.Enabled = true;
		}

		private string ProcessName(string sqlServerName) {

			StringBuilder sb = new StringBuilder();

			bool isFirst = true;

			foreach (char c in sqlServerName) {


				if (!isFirst) {
					if (char.IsUpper(c)) {
						sb.Append('_');
					}
				}
				else {
					isFirst = false;
				}

				sb.Append(char.ToUpper(c));
			}

			return sb.ToString();
		}

		private void button7_Click(object sender, EventArgs e) {

			button7.Enabled = false;

			using (DataAnalysisDataContext dataContext = new DataAnalysisDataContext()) {

				using (StreamReader sr = new StreamReader("C:\\T3\\Data.csv")) {

					bool isFirst = true;

					while (sr.Peek() != -1) {

						string[] items = _regex.Split(sr.ReadLine());

						if (isFirst) {
							isFirst = false;
							continue;
						}


						//File Category   Unit Owner Code POS/ Timer   Group Entity  Tx Date


						MissingFileAnalysi1 missing = new MissingFileAnalysi1() {
							Entity = items[5].Replace("\"", ""),
							GroupDescription = items[4].Replace("\"", ""),
							POS = items[3].Replace("\"", ""),
							FileCategory = items[0].Replace("\"", ""),
							FileType = "DriveThru",
							OwnerCode = items[2].Replace("\"", ""),
							TransactionDate = Convert.ToDateTime(items[6]),
							Unit = items[1].Replace("\"", ""),
							UnitNumber = items[1].Replace("\"", "").Substring(0, 7)
						};

						dataContext.MissingFileAnalysi1s.InsertOnSubmit(missing);

					}

				}

				dataContext.SubmitChanges();

			}


				button7.Enabled = true;
		}

		private void button8_Click(object sender, EventArgs e) {

			button8.Enabled = false;

			string fileName;
			string filePath;


			string template = "BrinkApiReader /Action=LoadApiData /UnitNumber=!UNIT_NUMBER /BusinessDate=!BUSINESS_DATE";

			StringBuilder sb = new StringBuilder();

			sb.Append("C:\r\ncd \\CkeProcesses\\Brink\\BrinkApiProcessor\r\n");


			int idx = 1;


			using (StreamReader sr = new StreamReader("C:\\T8\\BrinkCheck.txt")) {

				int counter = 0;

				while (sr.Peek() != -1) {

					string[] items = sr.ReadLine().Split('\t');

					StringBuilder sbLoad = new StringBuilder();
					sbLoad.Append(template);

					sbLoad.Replace("!UNIT_NUMBER", items[0]);

					string datestring = items[1].Substring(0, 4) + "-" + items[1].Substring(4, 2) + "-" + items[1].Substring(6, 2);
					sbLoad.Replace("!BUSINESS_DATE", datestring);
					sb.Append(sbLoad);
					sb.Append("\r\n");

					if (++counter % 1000 == 0) {

						sb.Append("pause");

						fileName = "LoadData_" + idx.ToString() + ".cmd";
						filePath = Path.Combine("C:\\T8", fileName);

						Logger.Write("Writing file: " + filePath);
						using (StreamWriter sw = new StreamWriter(filePath)) {

							sw.Write(sb.ToString());
						}


						sb = new StringBuilder();
						sb.Append("C:\r\ncd \\CkeProcesses\\Brink\\BrinkApiProcessor\r\n");
						idx++;

					}
				}

			}


			sb.Append("pause");
			fileName = "LoadData_" + idx.ToString() + ".cmd";
			filePath = Path.Combine("C:\\T8", fileName);

			Logger.Write("Writing file: " + filePath);
			using (StreamWriter sw = new StreamWriter(filePath)) {

				sw.Write(sb.ToString());
			}

			button8.Enabled = true;
		}

		private void button9_Click(object sender, EventArgs e) {

			button9.Enabled = false;

			using (StreamReader sr = new StreamReader("C:\\T4\\TableDef4.csv")) {

				while (sr.Peek() != -1) {

					string[] items = sr.ReadLine().Split(',');

					//TABLE_NAME,COLUMN_NAME,ORDINAL_POSITION,DATA_TYPE,IS_NULLABLE,CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION

					SnowflakeColumnDef columnDef = new SnowflakeColumnDef() {
						ColumnName = items[1].Trim(),
						DataType = items[3].Trim(),
						OrdinalPosition = Convert.ToInt32(items[2]),
						TableName = items[0].Trim()
					};

					if (items[4].Trim() == "YES") {
						columnDef.IsNullable = true;
					}
					else {
						columnDef.IsNullable = false;
					}



					//if (items[5].Trim() != "") {
					//	columnDef.MaxLength = Convert.ToInt32(items[5]);
					//}
					if (items[6].Trim() != "") {
						columnDef.NumericPrecision = Convert.ToInt32(items[6]);
					}

					using (DataAnalysisDataContext dataContext = new DataAnalysisDataContext()) {

						dataContext.SnowflakeColumnDefs.InsertOnSubmit(columnDef);
						dataContext.SubmitChanges();
					}


				}

			}


			button9.Enabled = true;
		}

		private void button10_Click(object sender, EventArgs e) {

			button10.Enabled = false;

			List<SnowflakeColumnDef> columnDefList;

			using (DataAnalysisDataContext dataContext = new DataAnalysisDataContext()) {
				columnDefList = dataContext.SnowflakeColumnDefs.ToList();
			}

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml("<Tables/>");

			//List<string> tableNames = columnDefList.Select(c => c.TableName).Distinct().ToList();
			List<string> tableNames = new List<string>() { "CashManagementDetail", "CashManagementFile" };


			foreach (string tableName in tableNames) {

				XmlElement tableElement = xmlDoc.CreateElement("Table");
				xmlDoc.DocumentElement.AppendChild(tableElement);


				tableElement.SetAttribute("Name", tableName);

				List<SnowflakeColumnDef> selectedColumnDefs = columnDefList.Where(c => c.TableName == tableName).OrderBy(c => c.OrdinalPosition).ToList();



				foreach (SnowflakeColumnDef columnDef in selectedColumnDefs) {

					XmlElement columnElement = xmlDoc.CreateElement("Column");
					tableElement.AppendChild(columnElement);


					columnElement.SetAttribute("ColumnName", columnDef.ColumnName);
					columnElement.SetAttribute("DataType", columnDef.DataType);
					columnElement.SetAttribute("IsNullable", columnDef.IsNullable.ToString());
					columnElement.SetAttribute("" +
						"DotNetDataType", columnDef.DotNetDataType);
				}
			}

			xmlDoc.Save("C:\\T4\\DataMapX1.xml");

			button10.Enabled = true;
		}

		private void button11_Click(object sender, EventArgs e) {

			button11.Enabled = false;

			Stopwatch sw1 = new Stopwatch();
			sw1.Start();
			Logger.Write("Begin processing cc recon files.");

			string destDirName = "C:\\Temp\\CCReconFiles";

			if (!Directory.Exists(destDirName)) {
				Directory.CreateDirectory(destDirName);
			}

			DirectoryInfo di = new DirectoryInfo(@"\\skyxdata01.ckrcorp.com\cmsos2\ckenode");

			DirectoryInfo[] unitDirectories = di.GetDirectories("X1*");

			foreach (DirectoryInfo unitDirectory in unitDirectories) {

				Stopwatch sw2 = new Stopwatch();
				sw2.Start();
				Logger.Write("Begin processing directory: " + unitDirectory.Name);

				DirectoryInfo inboxDirectory = unitDirectory.GetDirectories("Inbox").SingleOrDefault();

				if (inboxDirectory != null) {
					if (inboxDirectory.Exists) {

						FileInfo[] ccReconFiles = inboxDirectory.GetFiles("X1*CreditCardRecon.csv");

						foreach (FileInfo ccReconFile in ccReconFiles) {
							string filePath = Path.Combine(destDirName, ccReconFile.Name); ;
							Logger.Write("Copying file: " + ccReconFile.Name);
							ccReconFile.CopyTo(filePath, true);
						}
					}
					else {
						Logger.Write("Inbox directory does not exist.");
					}
				}
				else {
					Logger.Write("Inbox directory is null.");
				}

				Logger.Write("Finished processing directory: " + unitDirectory.Name + ". Elapsed time: " + sw2.Elapsed.ToString());
			}

			Logger.Write("Finished processing cc recon files. Elapsed time: " + sw1.Elapsed.ToString());
			button11.Enabled = true;
		}

		private void button12_Click(object sender, EventArgs e) {

			button12.Enabled = false;

			string fileName;
			string filePath;

			StringBuilder sb = new StringBuilder();

			int idx = 1;


			using (StreamReader sr = new StreamReader("C:\\T8\\BrinkCheck.txt")) {

				int counter = 0;

				while (sr.Peek() != -1) {

					string[] items = sr.ReadLine().Split('\t');
					sb.Append(items[0]);
					sb.Append('\t');
					sb.Append(items[1].Substring(0, 4) + "-" + items[1].Substring(4, 2) + "-" + items[1].Substring(6, 2));
					sb.Append("\r\n");

					if (++counter % 1000 == 0) {

						sb.Append("pause");

						fileName = "ProcessUnits_" + idx.ToString() + ".txt";
						filePath = Path.Combine("C:\\T8", fileName);

						Logger.Write("Writing file: " + filePath);
						using (StreamWriter sw = new StreamWriter(filePath)) {

							sw.Write(sb.ToString());
						}


						sb = new StringBuilder();
						idx++;

					}
				}

			}


			fileName = "ProcessUnits_" + idx.ToString() + ".txt";
			filePath = Path.Combine("C:\\T8", fileName);

			Logger.Write("Writing file: " + filePath);
			using (StreamWriter sw = new StreamWriter(filePath)) {

				sw.Write(sb.ToString());
			}

			button8.Enabled = true;




			button12.Enabled = true;
		}

		private void button13_Click(object sender, EventArgs e) {

			button13.Enabled = false;

			//--TABLE_NAME,COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION,NUMERIC_SCALE,IS_NULLABLE,ORDINAL_POSITION
			using (StreamReader sr = new StreamReader("C:\\TestFiles\\SnowflakeTest\\Destination.csv")) {

				bool isFirst = true;

				while (sr.Peek() != -1) {

					string[] items = sr.ReadLine().Split(',');

					if (isFirst) {
						isFirst = false;
						continue;
					}


					SnowflakeColumnDef snowflakeTable = new SnowflakeColumnDef() {
						ColumnName = items[1],
						DataType = items[2],
						OrdinalPosition = Convert.ToInt32(items[7]),
						TableName = items[0]
					};

					string temp = items[3];

					if (!string.IsNullOrEmpty(temp)) {
						snowflakeTable.CharacterMaximumLength = Convert.ToInt32(temp);
					}

					temp = items[4];

					if (!string.IsNullOrEmpty(temp)) {
						snowflakeTable.NumericPrecision = Convert.ToInt32(temp);
					}

					temp = items[5];

					if (!string.IsNullOrEmpty(temp)) {
						snowflakeTable.NumericScale = Convert.ToInt32(temp);
					}

					temp = items[6];

					if (temp == "YES") {
						snowflakeTable.IsNullable = true;
					}
					else {
						snowflakeTable.IsNullable = false;
					}

					using (DataAnalysisDataContext dataContext = new DataAnalysisDataContext()) {
						dataContext.SnowflakeColumnDefs.InsertOnSubmit(snowflakeTable);
						dataContext.SubmitChanges();

					}


				}
			}

			button13.Enabled = true;
		}

		private void button14_Click(object sender, EventArgs e) {

			button14.Enabled = false;

			StringBuilder sb = new StringBuilder();

			using (StreamReader sr = new StreamReader("C:\\T1\\AwsFileProcessor_20231212_1441_7212.log")) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new string[] { "\t:" }, StringSplitOptions.RemoveEmptyEntries);

					if (items.Length > 1) {
						if (items[1].Contains("GetPresignedUrl has completed") || items[1].Contains("Finished calling GetResponse") || items[1].Contains("SendFileToS3 has successfully completed")) {
							sb.Append(items[1]);
							sb.Append("\r\n");
						}
					}
				}
			}

			textBox1.Text = sb.ToString();

			button14.Enabled = true;
		}

		private void button15_Click(object sender, EventArgs e) {

			button15.Enabled = false;

			StringBuilder sb = new StringBuilder();

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string line in lines) {

				string[] items = line.Split('\t');

				StringBuilder buffer = new StringBuilder();

				string item = items[0].Trim();

				bool isFirst = true;

				foreach (char c in item) {

					if (!isFirst) {
						if (char.IsUpper(c)) {
							buffer.Append("_");
						}
					}
					else {
						isFirst = false;
					}

					buffer.Append(char.ToUpper(c));
				}

				sb.Append(buffer);
				sb.Append(" ");

				if (items.Length > 1) {
					sb.Append(items[1].Trim().ToUpper());
					sb.Append(" COMMENT '");
					sb.Append(items[2].Trim());
					sb.Append("',");

				}

				sb.Append(",\r\n");
			}

			textBox1.Text = sb.ToString();


			button15.Enabled = true;

		}

		private void button16_Click(object sender, EventArgs e) {

			button16.Enabled = false;


			List<string> unitList = new List<string>();

			using (StreamReader sr = new StreamReader("C:\\Temp\\Units.txt")) {

				while (sr.Peek() != -1) {


					unitList.Add(sr.ReadLine());

				}
			}

			DirectoryInfo di = new DirectoryInfo("C:\\Temp\\Logs");

			FileInfo[] files = di.GetFiles("*.log");

			foreach (string unitNumber in unitList) {

				StringBuilder sb = new StringBuilder();

				if (unitNumber.StartsWith("110")) {
					continue;
				}

				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("Begin processing unit: " + unitNumber);


				foreach (FileInfo file in files) {

					using (StreamReader sr = file.OpenText()) {

						while (sr.Peek() != -1) {

							string line = sr.ReadLine();

							if (line.Contains(unitNumber)) {
								
								if (line.Contains(@"ProcessFile starting for: \\ckrcorp.com\ckeftp\FTP\Domestic\Xpient\ToCke")) {
								//if (line.Contains(@"ProcessFile starting for: \\skyftp01\FtpRoot\CKE\USR\Xpient\")) {
											sb.Append(line);
									sb.Append("\r\n");
								}
							}
						}
					}


				}

				if (sb.Length > 0) {

					using (StreamWriter sw = new StreamWriter("C:\\Temp\\Bar_" + unitNumber + ".txt")) {
						sw.Write(sb.ToString());
					}
				}

				Logger.Write("Finished processing unit: " + unitNumber + ". Elapsed time: " + sw1.Elapsed.ToString());

			}










			button16.Enabled = true;

		}

		private class CardDate {
			public string TransactionId;
			public string CardExpirationDate;
			public string PosReferenceId;
			public DateTime? EstimatedDateofPayoutLocal = null;
			public DateTime? EstimatedDateofPayoutUTC = null;
		}





		private void button17_Click(object sender, EventArgs e) {

			button17.Enabled = false;

			List<CardDate> cardDates = new List<CardDate>();

			DirectoryInfo di = new DirectoryInfo("C:\\OloPayProcessor\\Archive");

			FileInfo[] files = di.GetFiles("Transaction*");

			foreach (FileInfo file in files) {

				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("Begin processing file: " + file.Name);

				using (StreamReader sr = file.OpenText()) {


					bool isFirst = true;

					while (sr.Peek() != -1) {

						string line = sr.ReadLine().Replace("\"", "");

						if (!isFirst) {
							string[] items = line.Split(',');

							CardDate cardDate = new CardDate() {
								CardExpirationDate = items[19].Trim(),
								TransactionId = items[2].Trim()
							};

							if (!string.IsNullOrEmpty(items[22])) {
								cardDate.EstimatedDateofPayoutUTC = Convert.ToDateTime(items[22]);
							}

							if (!string.IsNullOrEmpty(items[23])) {
								cardDate.EstimatedDateofPayoutLocal = Convert.ToDateTime(items[23]);
							}



							if (cardDates.Where(c => c.TransactionId == cardDate.TransactionId).Count() == 0) {
								cardDates.Add(cardDate);
							}

						}
						else {
							isFirst = false;
						}

					}
				}

				Logger.Write("Finished processing file: " + file.Name + ".  Elapsed time: " + sw1.Elapsed.ToString());
			}

			StringBuilder sb = new StringBuilder();

			foreach (CardDate cardDate in cardDates) {

				sb.Append("UPDATE TransactionDetail SET CardExpirationDate = '");
				sb.Append(cardDate.CardExpirationDate);
				sb.Append("' ");

				if (cardDate.EstimatedDateofPayoutUTC != null) {
					sb.Append(", EstimatedDateofPayoutUTC = '");
					sb.Append(((DateTime)cardDate.EstimatedDateofPayoutUTC).ToString("yyyy-MM-dd"));
					sb.Append("' ");
				}


				if (cardDate.EstimatedDateofPayoutLocal != null) {
					sb.Append(", EstimatedDateofPayoutLocalTime = '");
					sb.Append(((DateTime)cardDate.EstimatedDateofPayoutLocal).ToString("yyyy-MM-dd"));
					sb.Append("' ");
				}



				sb.Append(" WHERE TransactionID = '");
				sb.Append(cardDate.TransactionId);
				sb.Append("'\r\n");
			}


			textBox1.Text = sb.ToString();

			button17.Enabled = true;
		}


		private class AwsTime {

			public string FileName;
			public decimal PresingUrlElapsed;
			public decimal GetResponseElapsed;
			public decimal SendFileElapsed;

		}



		private void button18_Click(object sender, EventArgs e) {

			button18.Enabled = false;

			List<AwsTime> awsTimes = new List<AwsTime>();

			StringBuilder sb = new StringBuilder();

			using (StreamReader sr = new StreamReader("C:\\T1\\AwsFileProcessor_20231212_1709_4180.log")) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new string[] { "\t:" }, StringSplitOptions.RemoveEmptyEntries);

					if (items.Length > 1) {
						if (items[1].Contains("GetPresignedUrl has completed") || items[1].Contains("Finished calling GetResponse") || items[1].Contains("SendFileToS3 has successfully completed")) {

							int position = items[1].IndexOf("for: ") + "For: ".Length;



							string fileName = items[1].Substring(position, 25);

							AwsTime aws = awsTimes.Where(a => a.FileName == fileName).SingleOrDefault();

							if (aws == null) {
								aws = new AwsTime() {
									FileName = fileName
								};

								awsTimes.Add(aws);
							}

							position = items[1].IndexOf("Elapsed time: ") + "Elapased Time: 00:00:".Length;
							string timeVal = items[1].Substring(position);

							if (items[1].Contains("GetPresignedUrl has completed")) {
								aws.PresingUrlElapsed = Convert.ToDecimal(timeVal);

							}
							else if (items[1].Contains("Finished calling GetResponse")) {
								aws.GetResponseElapsed = Convert.ToDecimal(timeVal);
							}

							else if (items[1].Contains("SendFileToS3 has successfully completed")) {
								aws.SendFileElapsed = Convert.ToDecimal(timeVal);
							}
						}
					}
				}
			}
				foreach (AwsTime aws in awsTimes) {
					sb.Append(aws.FileName);
					sb.Append('\t');
					sb.Append(aws.PresingUrlElapsed);
					sb.Append('\t');
					sb.Append(aws.GetResponseElapsed);
					sb.Append('\t');
					sb.Append('\t');
					sb.Append(aws.SendFileElapsed);
					sb.Append("\r\n");

				}


				textBox1.Text = sb.ToString();

			button18.Enabled = true;
		}

		private void button19_Click(object sender, EventArgs e) {

			button19.Enabled = false;

			List<string> unitList = new List<string>();

			using (StreamReader sr = new StreamReader("C:\\Temp\\BneUnits.txt")) {

				while (sr.Peek() != -1) {
					unitList.Add(sr.ReadLine().Trim());
				}
			}

			string destDirName = "C:\\Temp\\BNE";

			if (!Directory.Exists(destDirName)) {
				Directory.CreateDirectory(destDirName);
			}


			DirectoryInfo di = new DirectoryInfo(@"C:\CKEProcesses\CkeFranchiseFileProcessor\ZipFileArchive\Xpient\Wednesday");

			foreach (string unitNumber in unitList) {

				Stopwatch sw2 = new Stopwatch();
				sw2.Start();
				Logger.Write("Checking unit number: " + unitNumber);

				string searchPattern = "*" + unitNumber + "*";

				FileInfo[] files = di.GetFiles(searchPattern);

				foreach (FileInfo file in files) {

					string filePath = Path.Combine(destDirName, file.Name);
					Logger.Write("Copying file: " + file.Name);
					file.CopyTo(filePath, true);
				}

				Logger.Write("Finsished processing unit number: " + unitNumber + ", Elapsed time: " + sw2.Elapsed.ToString());

			}

			button19.Enabled = true;
		}

		private void button20_Click(object sender, EventArgs e) {

			button20.Enabled = false;


			using (StreamReader sr = new StreamReader("C:\\Temp\\CouponCheck.txt")) {

				while (sr.Peek() != -1) {

					string[] items = sr.ReadLine().Split('\t');

					Coupon coupon = new Coupon() {
						CouponName = items[2],
						CouponNumber = items[1],
						TypeName = items[3],
						UnitNumber = items[0]
					};

					using (DataAnalysisDataContext dataContext = new DataAnalysisDataContext()) {

						if (dataContext.Coupons.Where(c => c.UnitNumber == coupon.UnitNumber && c.CouponNumber == coupon.CouponNumber).Count() == 0) {
							dataContext.Coupons.InsertOnSubmit(coupon);
							dataContext.SubmitChanges();
						}
					}
				}
			}

			button20.Enabled = true;
		}

		private void button21_Click(object sender, EventArgs e) {

			button21.Enabled = false;

			StringBuilder sb = new StringBuilder();

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string line in lines) {

				sb.Append(ProcessName(line));
				sb.Append("\r\n");
			}


			textBox1.Text = sb.ToString();






			button21.Enabled = true;
		}

		private class FreeBurger {
			public string UnitNumber;
			public decimal TotalDiscount = 0;
			public int TotalCount = 0;
		}
		private void button22_Click(object sender, EventArgs e) {

			button22.Enabled = false;

			using (StreamReader sr = new StreamReader("C:\\T4\\Cke Restaurants.txt")) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split('\t');

					string locationToken = items[1];

					string desription = items[0].Substring(10);

					string unitNumber = "1";

					int position = desription.IndexOf("-10");

					if (position == -1) {
						position = desription.IndexOf("-50");
					}

					if (position != -1) {

						unitNumber += desription.Substring(position+1, 6);

						Logger.Write("Unit number = " + unitNumber);

						BrinkUnitData brinkUnitData = new BrinkUnitData() {
							Description = desription,
							LocationToken = locationToken,
							UnitNumber = unitNumber
						};

						using (DataAnalysisDataContext dataContext = new DataAnalysisDataContext()) {
							dataContext.BrinkUnitDatas.InsertOnSubmit(brinkUnitData);
							dataContext.SubmitChanges();
						}
					}
				}
			}

			button22.Enabled = true;
		}


		private class GLAccount {
			string AccountNumber { get; set; }
			string Concept { get; set; }

		}


		private void button23_Click(object sender, EventArgs e) {

			button23.Enabled = false;

			DirectoryInfo di = new DirectoryInfo(@"\\ckecldfnp02\HFSCO_RO\X1501708");

			DirectoryInfo[] dateDirectories = di.GetDirectories("2015*");

			foreach (DirectoryInfo dateDirectory in dateDirectories) {

				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("Begin processing date directory: " + dateDirectory.Name);

				FileInfo[] timeFiles = dateDirectory.GetFiles("timeClockActivity*.xml");

				foreach (FileInfo timeFile in timeFiles) {

					string filePath = Path.Combine("C:\\t6", timeFile.Name);
					Logger.Write("copying file: " + filePath);
					timeFile.CopyTo(filePath, true);
				}

				Logger.Write("Finished processing date directory: " + dateDirectory.Name + ". Elapsed time: " + sw1.Elapsed.ToString());
			}





			button23.Enabled = true;
		}

		private void button24_Click(object sender, EventArgs e) {

			button24.Enabled = false;

			StringBuilder sb = new StringBuilder();

			using (StreamReader sr = new StreamReader("C:\\T4\\UTZ.txt")) {

				while (sr.Peek() != -1) {

					string[] items = sr.ReadLine().Split('\t');

					string unitNumber = items[0].Trim();

					string databaseTimeZone = items[1].Trim();
					string apiTimeZone = items[7].Trim();

					int offsetAdjustment = 0;

					if (databaseTimeZone.StartsWith("Central") && apiTimeZone.StartsWith("Eastern")) {
						offsetAdjustment = 1;
					}
					else if (databaseTimeZone.StartsWith("Eastern") && apiTimeZone.StartsWith("Central")) {
						offsetAdjustment = -1;
					}
					else if (databaseTimeZone.StartsWith("Central") && apiTimeZone.StartsWith("Mountain")) {
						offsetAdjustment = -1;
					}

					else if (databaseTimeZone.StartsWith("Mountain") && apiTimeZone.StartsWith("Pacific")) {
						offsetAdjustment = -1;
					}

					else if (databaseTimeZone.StartsWith("Mountain") && apiTimeZone.StartsWith("Central")) {
						offsetAdjustment = 1;
					}

					else if (databaseTimeZone.StartsWith("Pacific") && apiTimeZone.StartsWith("Mountain")) {
						offsetAdjustment = 1;
					}

					sb.Append(unitNumber);
					sb.Append('\t');
					sb.Append(databaseTimeZone);
					sb.Append('\t');
					sb.Append(apiTimeZone);
					sb.Append('\t');
					sb.Append(offsetAdjustment);
					sb.Append("\r\n");
				}
			}

			textBox1.Text = sb.ToString();

			button24.Enabled = true;
		}

		private void button25_Click(object sender, EventArgs e) {

			button25.Enabled = false;

			string template = SqlTemplateBroker.Load(SqlTemplateId.UpdateOrderTimes);
				
			StringBuilder sb = new StringBuilder();

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string line in lines) {
				string[] items = line.Split('\t');

				if (items[3] != "0") {

					sb.Append(template);
					sb.Replace("!UNIT_NUMBER", items[0]);
					sb.Replace("!DATE_TIME_OFFSET", items[3]);

					sb.Append("\r\n\r\n");
				}
			}

			using (StreamWriter sw = new StreamWriter("C:\\T4\\UpdateOrderTimes.sql")) {
				sw.Write(sb.ToString());
			}




			button25.Enabled = true;

		}

		private void button26_Click(object sender, EventArgs e) {

			button26.Enabled = false;

			string template = "UPDATE BrinkUnit SET TimeZone = '!TIME_ZONE', ModifyDate = GETDATE() WHERE UnitNumber = UnitNumber = '!UNIT_NUMBER'";

			StringBuilder sb = new StringBuilder();

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string line in lines) {
				string[] items = line.Split('\t');


				sb.Append(template);
				sb.Replace("!UNIT_NUMBER", items[0]);
				sb.Replace("!TIME_ZONE", items[2] + " Standard Time");


				sb.Append("\r\n\r\n");
			}

			using (StreamWriter sw = new StreamWriter("C:\\T4\\UpdateUnitTimeZones.sql")) {
				sw.Write(sb.ToString());
			}




			button26.Enabled = true;

		}

		private void button27_Click(object sender, EventArgs e) {

			button27.Enabled = false;

			BrinkApiData brinkApiData = new BrinkApiData();

			List<string> unitList = new List<string>() { "1100820" };

			unitList = new List<string>() {
				"1101354","1101382","1101385","1101552","1101587","1101745","1102348","1102556","1102596","1102776",
				"1500082","1500085","1500086","1500097","1500104","1500106","1500108","1500114","1500124","1500181",
				"1500218","1500220","1500222","1500242","1500540","1500584","1500647","1500660","1500668","1500714",
				"1500881","1500909","1500941","1500960","1500968","1501057","1501064","1501081","1501091","1501108",
				"1501152","1503130","1503377","1503434","1503537","1503742","1504226","1504232","1504237","1505437",
				"1505465","1505466","1505503","1505535","1505543","1505791","1505820","1505881","1505888","1505923",
				"1506119","1506122","1506175","1506235","1506274","1506315","1506326","1506355","1506388","1506408",
				"1506490","1506492","1506496","1506526","1506603","1506622","1506651","1501336","1501626","1503324",
				"1503804","1504040","1505304","1505607","1102517","1102871","1500678","1100820","1101353","1101631",
				"1102782", "1501010" };














			DateTime businessDate = new DateTime(2022, 3, 13);

			StringBuilder sb = new StringBuilder();

			foreach (string unitNumber in unitList) {

				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("Begin processing unit: " + unitNumber);


				Stopwatch sw2 = new Stopwatch();
				sw2.Start();
				Logger.Write("Begin retrieving orders...");
				List<BrinkOrder> brinkOrders = brinkApiData.GetBrinkOrders(unitNumber, businessDate);
				Logger.Write("Finished retrieving orders. Elapsed time: " + sw2.Elapsed.ToString());

				Logger.Write(brinkOrders.Count.ToString() + " orders found.");

				foreach (BrinkOrder brinkOrder in brinkOrders) {
					string template = SqlTemplateBroker.Load(SqlTemplateId.UpdateDstOrder);
					StringBuilder sb2 = new StringBuilder();

					sb2.Append(template);
					sb2.Replace("!OPEN_TIME", brinkOrder.OpenTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));

					if (brinkOrder.ClosedTime != null) {
						sb2.Replace("!CLOSED_TIME", ((DateTime)brinkOrder.ClosedTime).ToString("yyyy-MM-dd HH:mm:ss.fff"));
					}
					else {
						sb2.Replace("'!CLOSED_TIME'", "NULL");
					}
					if (brinkOrder.FirstSendTime != null) {
						sb2.Replace("!FIRST_SEND_TIME", ((DateTime)brinkOrder.FirstSendTime).ToString("yyyy-MM-dd HH:mm:ss.fff"));
					}
					else {
						sb2.Replace("'!FIRST_SEND_TIME'", "NULL");
					}

					sb2.Replace("!UNIT_NUMBER", unitNumber);
					sb2.Replace("!ID_ENCODED_FIELD", brinkOrder.IdEncodedField);


					sb2.Append("\r\n");

					sb.Append(sb2);

				}

				Logger.Write("Finshed processing unit: " + unitNumber + ". Elapsed time: " + sw1.Elapsed.ToString());

			}

			using (StreamWriter sw = new StreamWriter("C:\\T2\\DstUpdate.sql")) {
				sw.Write(sb.ToString());
			}


			button27.Enabled = true;

		}

		private void button28_Click(object sender, EventArgs e) {

			button28.Enabled = false;

			DirectoryInfo di = new DirectoryInfo("C:\\T3\\Logs");

			FileInfo[] logFiles = di.GetFiles();

			foreach (FileInfo logFile in logFiles) {

				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("Begin processing file: " + logFile.Name);

				bool isFirst = true;

				using (StreamReader sr = logFile.OpenText()) {

					while (sr.Peek() != -1) {

						string[] items = sr.ReadLine().Split('\t');

						if (!isFirst) {
							FileCreationLogAnalysi fileCreationLog = new FileCreationLogAnalysi() {
								BusinessDate = Convert.ToDateTime(items[1]),
								CreateDateTime = Convert.ToDateTime(items[3]),
								FileName = items[2],
								UnitNumber = items[0]
							};

							using (DataAnalysisDataContext dataContext = new DataAnalysisDataContext()) {
								dataContext.FileCreationLogAnalysis.InsertOnSubmit(fileCreationLog);
								dataContext.SubmitChanges();

							}
						}
						else {
							isFirst = false;
						}
					}
				}

				Logger.Write("Finished processing file: " + logFile.Name + ". Elapsed time: " + sw1.Elapsed.ToString());

			}

			button28.Enabled = true;
		}

		private void button29_Click(object sender, EventArgs e) {

			button29.Enabled = false;

			HmeFileProcessor fileProcessor = new HmeFileProcessor();
			fileProcessor.Run();


			button29.Enabled = true;

		}

		private void button30_Click(object sender, EventArgs e) {

			button30.Enabled = false;

			StringBuilder sb = new StringBuilder();

			BrinkApiData brinkApiData = new BrinkApiData();

			List<MenuItemPricing> menuItemPricingList = brinkApiData.GetMenuItemPricingList("1100054");

			List<MenuItemPricing> prodMenuItemPricingList = brinkApiData.GetMenuItemPricingListProd("1100054");


			foreach (MenuItemPricing menuItemPricing in menuItemPricingList) {



				MenuItemPricing prodMenuItemPricing = prodMenuItemPricingList.Where(p => p.BrinkMenuItemId == menuItemPricing.BrinkMenuItemId).SingleOrDefault();

				if (prodMenuItemPricing == null) {
					sb.Append(menuItemPricing.BrinkMenuItemId);
					sb.Append('\t');
					sb.Append(menuItemPricing.PLU);
					sb.Append('\t');
					sb.Append(menuItemPricing.ItemDescription);
					sb.Append('\t');
					sb.Append(menuItemPricing.Price);
					sb.Append('\t');
					sb.Append("\t\t\t\tProd data not found.");
					sb.Append("\r\n");
				}
				else {
					if (prodMenuItemPricing.Price != menuItemPricing.Price) {
						sb.Append(menuItemPricing.BrinkMenuItemId);
						sb.Append('\t');
						sb.Append(menuItemPricing.PLU);
						sb.Append('\t');
						sb.Append(menuItemPricing.ItemDescription);
						sb.Append('\t');
						sb.Append(menuItemPricing.Price);
						sb.Append('\t');
						sb.Append(prodMenuItemPricing.BrinkMenuItemId);
						sb.Append('\t');
						sb.Append(prodMenuItemPricing.PLU);
						sb.Append('\t');
						sb.Append(prodMenuItemPricing.ItemDescription);
						sb.Append('\t');
						sb.Append(prodMenuItemPricing.Price);
						sb.Append('\t');
						sb.Append("Price mismatch");
						sb.Append("\r\n");
					}
				}

			}

			foreach (MenuItemPricing prodMenuItemPricing in prodMenuItemPricingList) {

				MenuItemPricing menuItemPricing = menuItemPricingList.Where(m => m.BrinkMenuItemId == prodMenuItemPricing.BrinkMenuItemId).SingleOrDefault();

				if (menuItemPricing == null) {
					sb.Append("\t\t\t\t");
					sb.Append(prodMenuItemPricing.BrinkMenuItemId);
					sb.Append('\t');
					sb.Append(prodMenuItemPricing.PLU);
					sb.Append('\t');
					sb.Append(prodMenuItemPricing.ItemDescription);
					sb.Append('\t');
					sb.Append(prodMenuItemPricing.Price);
					sb.Append('\t');
					sb.Append("Test data missing");
					sb.Append("\r\n");
				}
			}

			using (StreamWriter sw = new StreamWriter("C:\\T4\\PricingTest_X.dat")) {

				sw.Write(sb.ToString());
			}



			button30.Enabled = true;
		}

		private void button31_Click(object sender, EventArgs e) {

			button31.Enabled = false;


			StringBuilder sb = new StringBuilder();

			BrinkApiData brinkApiData = new BrinkApiData();

			List<string> unitNumberList = brinkApiData.GetBrinkUnitList();
			//List<string> unitNumberList = new List<string>() { "1502971" };

			unitNumberList = unitNumberList.Where(u => u.StartsWith("150")).ToList();

			foreach (string unitNumber in unitNumberList) {
				
				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("Begin processing for unit: " + unitNumber);

				List<string> lovesUnitList = brinkApiData.GetBrinkUnits().Where(u => u.EntityId == 2).Select(u => u.UnitNumber).ToList();

				if (lovesUnitList.Contains(unitNumber)) {
					continue;
				}

				List<MenuItemPricing> menuItemPricingList = brinkApiData.GetMenuItemPricingList(unitNumber);

				List<MenuItemPricing> prodMenuItemPricingList = brinkApiData.GetMenuItemPricingListProd(unitNumber);


				foreach (MenuItemPricing menuItemPricing in menuItemPricingList) {




					MenuItemPricing prodMenuItemPricing = prodMenuItemPricingList.Where(p => p.BrinkMenuItemId == menuItemPricing.BrinkMenuItemId).SingleOrDefault();

					if (prodMenuItemPricing == null) {
						sb.Append(menuItemPricing.UnitNumber);
						sb.Append('\t');
						sb.Append(menuItemPricing.BrinkMenuItemId);
						sb.Append('\t');
						sb.Append(menuItemPricing.PLU);
						sb.Append('\t');
						sb.Append(menuItemPricing.ItemDescription);
						sb.Append('\t');
						sb.Append(menuItemPricing.Price);
						sb.Append('\t');
						sb.Append("\t\t\t\tProd data not found.");
						sb.Append("\r\n");
					}
					else {
						if (prodMenuItemPricing.Price != menuItemPricing.Price) {
							sb.Append(menuItemPricing.UnitNumber);
							sb.Append('\t');
							sb.Append(menuItemPricing.BrinkMenuItemId);
							sb.Append('\t');
							sb.Append(menuItemPricing.PLU);
							sb.Append('\t');
							sb.Append(menuItemPricing.ItemDescription);
							sb.Append('\t');
							sb.Append(menuItemPricing.Price);
							sb.Append('\t');
							sb.Append(prodMenuItemPricing.UnitNumber);
							sb.Append('\t');
							sb.Append(prodMenuItemPricing.BrinkMenuItemId);
							sb.Append('\t');
							sb.Append(prodMenuItemPricing.PLU);
							sb.Append('\t');
							sb.Append(prodMenuItemPricing.ItemDescription);
							sb.Append('\t');
							sb.Append(prodMenuItemPricing.Price);
							sb.Append('\t');
							sb.Append("Price mismatch");
							sb.Append("\r\n");
						}
					}

				}

				foreach (MenuItemPricing prodMenuItemPricing in prodMenuItemPricingList) {

					MenuItemPricing menuItemPricing = menuItemPricingList.Where(m => m.BrinkMenuItemId == prodMenuItemPricing.BrinkMenuItemId).SingleOrDefault();

					if (menuItemPricing == null) {
						sb.Append("\t\t\t\t\t");
						sb.Append(prodMenuItemPricing.UnitNumber);
						sb.Append('\t');
						sb.Append(prodMenuItemPricing.BrinkMenuItemId);
						sb.Append('\t');
						sb.Append(prodMenuItemPricing.PLU);
						sb.Append('\t');
						sb.Append(prodMenuItemPricing.ItemDescription);
						sb.Append('\t');
						sb.Append(prodMenuItemPricing.Price);
						sb.Append('\t');
						sb.Append("Test data missing");
						sb.Append("\r\n");
					}
				}

				Logger.Write("Finished processing for unit: " + unitNumber + ".  Elapsed time: " + sw1.Elapsed.ToString());

			}

			using (StreamWriter sw = new StreamWriter("C:\\T4\\PricingTest_XH2.dat")) {

				sw.Write(sb.ToString());
			}

			button31.Enabled = true;
		}

		private void button32_Click(object sender, EventArgs e) {

			button32.Enabled = false;

			DataAccess dac = new DataAccess(AppSettings.BrinkApiDataConnectionString);

			StringBuilder sb = new StringBuilder();

			using (StreamReader sr = new StreamReader("C:\\T4\\UnitSalesCheck.txt")) {

				while (sr.Peek() != -1) {

					string[] items = sr.ReadLine().Split('\t');
					
					Stopwatch sw1 = new Stopwatch();
					sw1.Start();
					Logger.Write("Begin processing unit number: " + items[0] + " and business date: " + items[1]);

					sb.Append(items[0]);
					sb.Append('\t');
					sb.Append(items[1]);
					sb.Append('\t');

					StringBuilder sbQuery = new StringBuilder();
					sbQuery.Append(SqlTemplateBroker.Load(SqlTemplateId.GetDailySalesTotal));

					sbQuery.Replace("!UNIT_NUMBER", items[0]);
					sbQuery.Replace("!BUSINESS_DATE", items[1]);

					DataTable dt = dac.ExecuteQuery(sbQuery.ToString());

					if (dt.Rows.Count > 0) {
						sb.Append(dt.Rows[0]["TotalNetSales"].ToString());
					}
					else {
						sb.Append("0");
					}
					sb.Append("\r\n");

					Logger.Write("Finished processing unit number: " + items[0] + " and business date: " + items[1] + ". Elapsed time: " + sw1.Elapsed.ToString());

				}

				using (StreamWriter sw = new StreamWriter("C:\\T4\\SalesResults_4.dat")) {
					sw.Write(sb.ToString());
				}
			}

			button32.Enabled = true;
		}

		private void button33_Click(object sender, EventArgs e) {

			button33.Enabled = false;

			HmeData hmeData = new HmeData();
			List<string> hmeUnitList = hmeData.GetHmeUnitList();

			List<string> unitList = new List<string>();
			StringBuilder sb = new StringBuilder();

			using (StreamReader sr = new StreamReader("C:\\C8\\UnitDriveThru.txt")) {

				while (sr.Peek() != -1) {

					string unitNumber = sr.ReadLine().Split('\t')[0].Trim();

					if (hmeUnitList.Contains(unitNumber)) {
						if (unitList.Where(u => u == unitNumber).Count() == 0) {
							unitList.Add(unitNumber);
							sb.Append(unitNumber);
							sb.Append("\r\n");
						}
					}
				}
			}


			textBox1.Text = sb.ToString();






			button33.Enabled = true;
		}

		private void button34_Click(object sender, EventArgs e) {

			button34.Enabled = false;

			StringBuilder sb = new StringBuilder();


			DirectoryInfo di = new DirectoryInfo("C:\\T9");

			FileInfo[]  logFiles = di.GetFiles("*.log");


			foreach (FileInfo logFile in logFiles) {

				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("Begin processing file: " + logFile.Name);

				List<string> lineList = new List<string>();

				using (StreamReader sr = logFile.OpenText()) {

					while (sr.Peek() != -1) {
						lineList.Add(sr.ReadLine());
					}
				}

				string unitLine = "";
				bool hasError = false;


				for (int i = 0; i < lineList.Count; i++) {

					if (lineList[i].Contains("ProcessSingleUnit starting")) {
						unitLine = lineList[i];
					}

					if (lineList[i].Contains("An exception occurred in ProcessSingleUnit")) {
						hasError = true;
						break;
					}
				}

				if (hasError) {
					int pos = unitLine.IndexOf("unit number: ") + "unit number: ".Length;

					string unitNumber = unitLine.Substring(pos).Trim();

					pos = unitLine.IndexOf("business date:") + "business date:".Length;

					string dateString = unitLine.Substring(pos, 10);
					sb.Append("BrinkApiReader /Action=LoadApiData /UnitNumber=");
					sb.Append(unitNumber);
					sb.Append(" /BusinessDate=");
					sb.Append(dateString);
					sb.Append("\r\n");



				}



				Logger.Write("Finished processing file: " + logFile.Name + ".  Elapsed time: " + sw1.Elapsed.ToString());


			}

			using (StreamWriter sw = new StreamWriter("C:\\t4\\UpdateOrders2.txt")) {
				sw.Write(sb.ToString());
			}


			textBox1.Text = sb.ToString();

			button34.Enabled = true;
		}

		private void button35_Click(object sender, EventArgs e) {

			button35.Enabled = false;

			DirectoryInfo di = new DirectoryInfo("\\\\skyxdata01\\cmsos2\\ckenode");

			DirectoryInfo[] unitDirectories = di.GetDirectories("X1*");

			foreach (DirectoryInfo unitDirectory in unitDirectories) {

				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("Begin processing directory: " + unitDirectory.Name);

				FileInfo[] tlogFiles = unitDirectory.GetFiles("X1*20240415_Transhist.xml");

				foreach (FileInfo tlogFile in tlogFiles) {
					string filePath = Path.Combine("C:\\T11", tlogFile.Name);
					tlogFile.CopyTo(filePath, true);
				}

				Logger.Write("Finished processing directory: " + unitDirectory.Name + ". Elapsed time: " + sw1.Elapsed.ToString());

			}

			button35.Enabled = true;
		}

		private void button36_Click(object sender, EventArgs e) {

			button36.Enabled = false;

			string businessDateString;

			DirectoryInfo di = new DirectoryInfo("\\\\skyxdata01\\cmsos2\\ckenode");

			DirectoryInfo[] unitDirectories = di.GetDirectories("X1*");

			foreach (DirectoryInfo unitDirectory in unitDirectories) {

				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("Begin processing directory: " + unitDirectory.Name);

				string unitNumber = unitDirectory.Name.Substring(1, 7);

				FileInfo[] finFiles = unitDirectory.GetFiles("*240415*.fin");

				foreach (FileInfo finFile in finFiles) {

					string fileName = finFile.Name;

					if (!finFile.Name.StartsWith("X")) {

						using (StreamReader sr = finFile.OpenText()) {

							string[] items = sr.ReadLine().Split(',');

							DateTime tempDate = DateTime.ParseExact(items[0], "MMddyy", System.Globalization.CultureInfo.CurrentCulture);

							businessDateString = tempDate.ToString("yyyyMMdd");
						}

						fileName = "X" + unitNumber + "_" + businessDateString + "_PD.Fin";

						Logger.Write("Renaming file: " + fileName);
					}

					string filePath = Path.Combine("C:\\T12", fileName);
					finFile.CopyTo(filePath, true);
				}

				Logger.Write("Finished processing directory: " + unitDirectory.Name + ". Elapsed time: " + sw1.Elapsed.ToString());

			}

			button36.Enabled = true;
		}

		private void button37_Click(object sender, EventArgs e) {

			button37.Enabled = false;

			try {

				string rootDirName = "\\\\skyxdata01\\cmsos2\\ckenode";
				string destDirName = "C:\\T10";

				int counter = 0;

				Stopwatch sw1 = new Stopwatch();
				sw1.Start();

				using (StreamReader sr = new StreamReader("C:\\T10\\FileList.txt")) {

					while (sr.Peek() != -1) {

						string[] items = sr.ReadLine().Split('\t');


						string unitNumber = items[0];

						string srcDirName = Path.Combine(rootDirName, "X" + unitNumber);

						DirectoryInfo unitDirectory = new DirectoryInfo(srcDirName);

						FileInfo pollFile = unitDirectory.GetFiles(items[2]).SingleOrDefault();

						if (pollFile != null) {
							string destPath = Path.Combine(destDirName, pollFile.Name);
							Logger.Write("Copying file: " + destPath);
							pollFile.CopyTo(destPath, true);
							pollFile.Delete();
						}

						counter++;

						if (counter % 1000 == 0) {
							Logger.Write(counter.ToString() + " records processed.  Elapsed time: " + sw1.Elapsed.ToString());
						}
					}
				}
			}
			catch (Exception ex) {
				Logger.Write("An exception occurred in button37_click. Please see error log for details.");
				Logger.WriteError(ex);
			}

			button37.Enabled = true;
		}

		private void button38_Click(object sender, EventArgs e) {

			button38.Enabled = false;

			string template = "UPDATE BrinkOrder SET IdField = !ID_FIELD WHERE BrinkOrderId = !BRINK_ORDER_ID";


			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetOrderIdField);

			DataAccess dac = new DataAccess(AppSettings.BrinkApiDataConnectionString);

			Stopwatch sw1 = new Stopwatch();
			sw1.Start();
			Logger.Write("Begin retrieving data...");
			DataTable dt = dac.ExecuteQuery(sql);
			Logger.Write("Finished retrieving data. Elpapsed time: " + sw1.Elapsed.ToString());


			Logger.Write(dt.Rows.Count.ToString() + " records retrieved.");

			int counter = 0;
			string fileNameTemplate = "UpdateIdField_!IDX.sql";

			int idx = 1;

			string fileName = fileNameTemplate.Replace("!IDX", idx.ToString("000"));
			string filePath = Path.Combine("C:\\t3", fileName);

			foreach (DataRow dr in dt.Rows) {

				string idEncodedField = dr["IdEncodedField"].ToString();
				long brinkOrderId = Convert.ToInt64(dr["BrinkOrderId"]);

				long idField = ConvertIdEncodedField(idEncodedField);

				string updateStatement = template.Replace("!BRINK_ORDER_ID", brinkOrderId.ToString());
				updateStatement = updateStatement.Replace("!ID_FIELD", idField.ToString());

				counter++;

				if (counter % 10000 == 0) {
					Logger.Write(counter.ToString() + " rows processed.");
					idx++;
					fileName = fileNameTemplate.Replace("!IDX", idx.ToString("000"));
					filePath = Path.Combine("C:\\t3", fileName);
				}

				using (StreamWriter sw = File.AppendText(filePath)) {
					sw.WriteLine(updateStatement);
				}
			}


			button38.Enabled = true;
		}

		private Dictionary<char, int> _encodingLookup = null;

		private long ConvertIdEncodedField(string idEncodedField) {


			if (_encodingLookup == null) {
				_encodingLookup	= new Dictionary<char, int>();

				string _table = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

				for (int i = 0; i < _table.Length; i++) {
					_encodingLookup.Add(_table[i], i);
				}
			}

			long result = 0;

			int pow = 0;

			if (idEncodedField.Contains("-")) {
				idEncodedField = idEncodedField.Substring(2);
			}



			for (int i = idEncodedField.Length - 1; i >= 0; i--) {

				char decodeChar = idEncodedField[i];
				int multiplier = _encodingLookup[decodeChar];

				result += (multiplier * (long)Math.Pow(32, pow));

				pow++;
			}

			return result;
		}
		private void button39_Click(object sender, EventArgs e) {

			button39.Enabled = false;

			string destDirName = "C:\\T6\\T6";

			DirectoryInfo di = new DirectoryInfo("C:\\2024-04-18_Labor2\\X1100321");

			DateTime weekEndDate = new DateTime(2016, 02, 08);
			DateTime endDate = new DateTime(2017, 11, 22);

			while (weekEndDate <= endDate) {
				
				string searchPattern = di.Name + "_" + weekEndDate.ToString("yyyyMMdd") + "_LbrSchHrsDlyByEmp.csv";
																		   
				FileInfo laborFile = di.GetFiles(searchPattern).SingleOrDefault();

				if (laborFile != null) {
					string filePath = Path.Combine(destDirName, laborFile.Name);
					Logger.Write("Copying file: " + filePath);
					laborFile.CopyTo(filePath, true);
				}

				weekEndDate = weekEndDate.AddDays(7);
			}

			button39.Enabled = true;
		}

		private void button40_Click(object sender, EventArgs e) {

			button40.Enabled = false;

			DirectoryInfo di = new DirectoryInfo("C:\\T6\\T6");

			FileInfo[] files = di.GetFiles();

			foreach (FileInfo file in files) {

				StringBuilder sb = new StringBuilder();

				using (StreamReader sr = file.OpenText()) {

					while (sr.Peek() != -1) {
						string line = sr.ReadLine();

						if (!string.IsNullOrEmpty(line)) {
							if (char.IsDigit(line[0])) {
								sb.Append(line);
								sb.Append("\r\n");
							}
						}
					}
				}

				using (StreamWriter sw = File.AppendText("C:\\T7\\LbrSchHrsDlyByEmpConsolidated.csv")) {
					sw.Write(sb);
				}
			}

			button40.Enabled = true;
		}

		private void button41_Click(object sender, EventArgs e) {

			button41.Enabled = false;

			string template = "RoyaltyReportProcessor  /FranchiseNumber=!FRANCHISE_NUMBER /BusinessDate=!BUSINESS_DATE ";
			StringBuilder sb = new StringBuilder();
			DateTime weekEndDate = new DateTime(2023, 10, 23);
			DateTime stopDate = new DateTime(2024, 04, 22);

			while (weekEndDate <= stopDate) {

				sb.Append(template);
				sb.Replace("!FRANCHISE_NUMBER", "8009141");
				sb.Append("\r\n");

				sb.Append(template);
				sb.Replace("!FRANCHISE_NUMBER", "8009142");
				sb.Append("\r\n");

				sb.Replace("!BUSINESS_DATE", weekEndDate.ToString("yyyy-MM-dd"));

				weekEndDate = weekEndDate.AddDays(7);
			}



			textBox1.Text = sb.ToString();

			//REM 




			button41.Enabled = true;
		}

		private class UnitDate {
			public string UnitNumber;
			public string BusinessDate;

		}

		private void button42_Click(object sender, EventArgs e) {

			button42.Enabled = false;

			List<UnitDate> unitDateList = new List<UnitDate>();

			DirectoryInfo di = new DirectoryInfo("C:\\T1");

			FileInfo[] files = di.GetFiles("*.tsv");

			foreach (FileInfo file in files) {

				using (StreamReader sr = file.OpenText()) {

					bool isFirst = true;

					while (sr.Peek() != -1) {
						string[] items = sr.ReadLine().Split('\t');

						if (!isFirst) {

							if (unitDateList.Where(u => u.UnitNumber == items[0] && u.BusinessDate == items[1]).Count() == 0) {
								UnitDate unitDate = new UnitDate() {
									BusinessDate = items[1],
									UnitNumber = items[0]
								};

								unitDateList.Add(unitDate);

							}
						}
						else {
							isFirst = false;
						}
					}
				}
			}

			unitDateList = unitDateList.OrderBy(u => u.UnitNumber).ThenBy(u => u.BusinessDate).ToList();


			StringBuilder sb = new StringBuilder();

			foreach (UnitDate unitDate in unitDateList) {
				sb.Append(unitDate.UnitNumber);
				sb.Append('\t');
				sb.Append(unitDate.BusinessDate);
				sb.Append("\r\n");

			}




			using (StreamWriter sw = new StreamWriter("C:\\T1\\UpdateUnits.txt")) {
				sw.Write(sb.ToString());
			}














			button42.Enabled = true;

		}

		private void button43_Click(object sender, EventArgs e) {

			button43.Enabled = false;

			string searchValue = "GetMenuItems starting for unit: ";
			string unitNumber = "";
			StringBuilder sb = new StringBuilder();

			List<string> unitList = new List<string>();

			DirectoryInfo di = new DirectoryInfo("C:\\T10");

			FileInfo[] files = di.GetFiles("*.log");

			foreach (FileInfo file in files) {

				using (StreamReader sr = file.OpenText()) {

					while (sr.Peek() != -1) {


						string line = sr.ReadLine();

						int position = line.IndexOf(searchValue);

						if (position > -1) {

							unitNumber = line.Substring(position + searchValue.Length, 7);
						}
						else {
							if (line.Contains("SINGLE USE CHARGE")) {

								//unitList.Add(unitNumber);
								sb.Append(unitNumber);
								sb.Append("\r\n");
							}
						}


					}
				}

			}

			textBox1.Text = sb.ToString();



			button43.Enabled = true;
		}

		private void button44_Click(object sender, EventArgs e) {

			button44.Enabled = false;

			StringBuilder sb = new StringBuilder();

			DirectoryInfo di = new DirectoryInfo("C:\\T2\\Temp");

			FileInfo[] files = di.GetFiles();

			foreach (FileInfo file in files) {

				Stopwatch sw1 = Stopwatch.StartNew();
				Logger.Write("Begin processing file: " + file.Name);


				using (StreamReader sr = file.OpenText()) {

					while (sr.Peek() != -1) {

						string line = sr.ReadLine();

						string[] items = line.Split(',');

						if (items.Count() > 1) {

							if (items[1] == "1501517") {
								sb.Append(line);
								sb.Append("\r\n");
							}
						}
					}
				}

				Logger.Write("Finished processing file: " + file.Name + ". Elapsed time: " + sw1.Elapsed.ToString());
				

			}


			using (StreamWriter sw = new StreamWriter("C:\\T2\\1501517.csv")) {
				sw.Write(sb.ToString());
			}



				button44.Enabled = true;
		}
	}
}
	

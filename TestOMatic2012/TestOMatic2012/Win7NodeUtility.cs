	using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {

	class Win7NodeUtility {

		public static List<string> GetWin7NodeList() {

			List<string> win7NodeList = new List<string>();

			using (StreamReader sr = new StreamReader(AppSettings.Win7UnitListFilePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine().Trim();

					if (line.Length > 0) {
						string[] items = line.Split(new char[] { ',' });

						if (items.Length == 3) {
							//-- We  only care about Hardees Win7 stores
							if (items[2].StartsWith("X15", StringComparison.CurrentCultureIgnoreCase)) {

								//-- Exclude labs from list
								if (!items[2].StartsWith("X1509")) {

									win7NodeList.Add(items[2]);
								}
							}
						}
					}
				}
			}

			win7NodeList.Sort();

			return win7NodeList;
		}
	}
}

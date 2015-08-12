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
	public partial class Form26 : Form {
		public Form26() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

			string filePath = "C:\\Temp2.0\\ParentChild.csv";
			string parentNumber = "";
			string parentDescription = "";
			string concept = "";

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { ',' });

					if (parentNumber == "") {
						parentNumber = items[0].Trim();
						parentDescription = items[1].Trim().Replace("\"", "");
						concept = items[2].Trim();
					}

					else {

						if (items[0].Trim() != "") {
							parentNumber = items[0].Trim();
							parentDescription = items[1].Trim().Replace("\"", "");
							concept = items[2].Trim();
						}
					}

					ParentChildNumber pcn = new ParentChildNumber();

					pcn.ChildDescription = items[4].Trim().Replace("\"", "");
					pcn.ChildNumber = items[3].Trim();
					pcn.Concept = concept;

					pcn.ParentDescription = parentDescription;
					pcn.ParentNumber = parentNumber;

					dataContext.ParentChildNumbers.InsertOnSubmit(pcn);
					dataContext.SubmitChanges();
				}
			}


			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void textBox1_TextChanged(object sender, EventArgs e) {

		}
	}
}

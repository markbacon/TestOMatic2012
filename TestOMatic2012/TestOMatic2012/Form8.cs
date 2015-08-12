using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form8 : Form {
		public Form8() {
			InitializeComponent();
			Logger.LoggerWrite += form8_onLoggerWrite;

			Logger.StartLogSession();
		}
		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;
			//CouponOrdersProcessor couponProcessor = new CouponOrdersProcessor();
			TransHistCouponProcessor couponProcessor = new TransHistCouponProcessor();
			couponProcessor.Run();

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			//string filePath = @"C:\StarPos\StarPosconfig\CouponConfig.xml";
			string filePath = @"C:\StarPos\CouponConfig.xml";
			
			CouponConfigProcessor ccp = new CouponConfigProcessor();
			ccp.Run(filePath);

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();


		}
		//---------------------------------------------------------------------------------------------------
		private void textBox1_TextChanged(object sender, EventArgs e) {

			if (textBox1.Text.Length > 2024) {
				textBox1.Text = "";
			}


			if (textBox1.Text.Length > 0) {
				textBox1.SelectionStart = textBox1.Text.Length - 1;
				textBox1.ScrollToCaret();
				Application.DoEvents();
			}
		}
	}
}

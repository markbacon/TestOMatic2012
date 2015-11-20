using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Management.Instrumentation;
using System.Text;
using System.Windows.Forms;
using System.Windows;


namespace TestOMatic2012 {
	public partial class Form40 : Form {
		public Form40() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			GetIP();



			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void GetIP() {
		//private void GetIP(string nicName, out string[] ipAdresses, out string[] subnets, out string[] gateways, out string[] dnses) {
		//	ipAdresses = null;
		//	subnets = null;
		//	gateways = null;
		//	dnses = null;

			ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
			ManagementObjectCollection moc = mc.GetInstances();

			foreach (ManagementObject mo in moc) {
				// Make sure this is a IP enabled device. 
				// Not something like memory card or VM Ware
				if (Convert.ToBoolean(mo["ipEnabled"])) {

					string foo = mo["Caption"].ToString();

					textBox1.Text += foo + "\r\n";
					Application.DoEvents();
					

					string[] ipAddresses = (string[])mo["IPAddress"];

					foreach (string fred in ipAddresses) {

						textBox1.Text += fred + "\r\n";
						Application.DoEvents();
					}

					textBox1.Text += "\r\n\r\n";

					string[] subnets = (string[])mo["IPSubnet"];

					foreach (string fred in subnets) {

						textBox1.Text += fred + "\r\n";
						Application.DoEvents();
					}

					textBox1.Text += "\r\n\r\n";
					
					string[] gateways = (string[])mo["DefaultIPGateway"];


					foreach (string fred in gateways) {

						textBox1.Text += fred + "\r\n";
						Application.DoEvents();
					}

					textBox1.Text += "\r\n\r\n";


					string[] dnses = (string[])mo["DNSServerSearchOrder"];


					foreach (string fred in dnses) {

						textBox1.Text += fred + "\r\n";
						Application.DoEvents();
					}



					//if (mo["Caption"].Equals(nicName)) {
					//	ipAdresses = (string[])mo["IPAddress"];
					//	subnets = (string[])mo["IPSubnet"];
					//	gateways = (string[])mo["DefaultIPGateway"];
					//	dnses = (string[])mo["DNSServerSearchOrder"];

					//	break;
					//}

				}
			}
		}

	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {

	/// <summary>
	/// Helper class to set networking configuration like IP address, DNS servers, etc.
	/// </summary>
	public class NetworkConfigurator {

		/// <summary>
		/// Returns the network card configuration of the specified NIC
		/// </summary>
		/// <PARAM name="nicName">Name of the NIC</PARAM>
		/// <PARAM name="ipAdresses">Array of IP</PARAM>
		/// <PARAM name="subnets">Array of subnet masks</PARAM>
		/// <PARAM name="gateways">Array of gateways</PARAM>
		/// <PARAM name="dnses">Array of DNS IP</PARAM>
		public static void GetIP(string nicName, out string[] ipAdresses,
		  out string[] subnets, out string[] gateways, out string[] dnses) {
			ipAdresses = null;
			subnets = null;
			gateways = null;
			dnses = null;

			//ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
			//ManagementObjectCollection moc = mc.GetInstances();

			//foreach (ManagementObject mo in moc) {
			//	// Make sure this is a IP enabled device. 
			//	// Not something like memory card or VM Ware
			//	if (mo["ipEnabled"] as bool) {
			//		if (mo["Caption"].Equals(nicName)) {
			//			ipAdresses = (string[])mo["IPAddress"];
			//			subnets = (string[])mo["IPSubnet"];
			//			gateways = (string[])mo["DefaultIPGateway"];
			//			dnses = (string[])mo["DNSServerSearchOrder"];

			//			break;
			//		}
			//	}
			}
		}
		/// <summary>
		/// Set's a new IP Address and it's Submask of the local machine
		/// </summary>
		/// <param name="ipAddress">The IP Address</param>
		/// <param name="subnetMask">The Submask IP Address</param>
		/// <param name="gateway">The gateway.</param>
		/// <remarks>Requires a reference to the System.Management namespace</remarks>
		public void SetIP(string ipAddress, string subnetMask, string gateway) {
			using (var networkConfigMng = new ManagementClass("Win32_NetworkAdapterConfiguration")) {
				using (var networkConfigs = networkConfigMng.GetInstances()) {
					foreach (var managementObject in networkConfigs.Cast<ManagementObject>().Where(managementObject => (bool)managementObject["IPEnabled"])) {
						using (var newIP = managementObject.GetMethodParameters("EnableStatic")) {
							// Set new IP address and subnet if needed
							if ((!String.IsNullOrEmpty(ipAddress)) || (!String.IsNullOrEmpty(subnetMask))) {
								if (!String.IsNullOrEmpty(ipAddress)) {
									newIP["IPAddress"] = new[] { ipAddress };
								}

								if (!String.IsNullOrEmpty(subnetMask)) {
									newIP["SubnetMask"] = new[] { subnetMask };
								}

								managementObject.InvokeMethod("EnableStatic", newIP, null);
							}

							// Set mew gateway if needed
							if (!String.IsNullOrEmpty(gateway)) {
								using (var newGateway = managementObject.GetMethodParameters("SetGateways")) {
									newGateway["DefaultIPGateway"] = new[] { newGateway };
									newGateway["GatewayCostMetric"] = new[] { 1 };
									managementObject.InvokeMethod("SetGateways", newGateway, null);
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Set's the DNS Server of the local machine
		/// </summary>
		/// <param name="nic">NIC address</param>
		/// <param name="dnsServers">Comma seperated list of DNS server addresses</param>
		/// <remarks>Requires a reference to the System.Management namespace</remarks>
		public void SetNameservers(string nic, string dnsServers) {
			using (var networkConfigMng = new ManagementClass("Win32_NetworkAdapterConfiguration")) {
				using (var networkConfigs = networkConfigMng.GetInstances()) {
					foreach (var managementObject in networkConfigs.Cast<ManagementObject>().Where(objMO => (bool)objMO["IPEnabled"] && objMO["Caption"].Equals(nic))) {
						using (var newDNS = managementObject.GetMethodParameters("SetDNSServerSearchOrder")) {
							newDNS["DNSServerSearchOrder"] = dnsServers.Split(',');
							managementObject.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
						}
					}
				}
			}
		}
	}
}
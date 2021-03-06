﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace BioEnumerator.RemoteHelper.ExternalTool
{
    public class InternetHelp
    {

        private const string PingTestWebSite = "mail.google.com";

        public static bool Check()
        {
            try
            {
                var pingReply = new Ping().Send("mail.google.com", 15000);
                if (pingReply == null)
                    return false;
                return pingReply.Status == IPStatus.Success;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("AppMain.PingWeb failed. \n" + ex.Message);
                return false;
            }
        }

        public static string GetIpAddress(NetworkInterface ni)
        {
            try
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Console.WriteLine(ni.Name);
                    foreach (UnicastIPAddressInformation unicastAddress in ni.GetIPProperties().UnicastAddresses)
                    {
                    if (unicastAddress.Address.AddressFamily == AddressFamily.InterNetwork)
                        return unicastAddress.Address.ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static NetworkInterface GetMainNetworkInterface()
        {
            var candidates = new List<NetworkInterface>();

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                candidates.AddRange(networkInterfaces.Where(ni => ni.OperationalStatus == OperationalStatus.Up));
            }

            if (candidates.Count == 1)
            {
                return candidates[0];
            }

            // Accoring to our tech, the main NetworkInterface should have a Gateway 
            // and it should be the ony one with a gateway.
            if (candidates.Count > 1)
            {
                for (var n = candidates.Count - 1; n >= 0; n--)
                {
                    if (candidates[n].GetIPProperties().GatewayAddresses.Count == 0)
                    {
                        candidates.RemoveAt(n);
                    }
                }

                if (candidates.Count == 1)
                {
                    return candidates[0];
                }
            }

            // Fallback to try by getting my ipAdress from the dns
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var myMainIpAdress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            if (myMainIpAdress != null)
            {
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                return (from ni in networkInterfaces where ni.OperationalStatus == OperationalStatus.Up let props = ni.GetIPProperties() from ai in props.UnicastAddresses where ai.Address.Equals(myMainIpAdress) select ni).FirstOrDefault();
            }

            return null;
        }
        
        public static string GetMACAddress()
        {
            var objectCollection = new ManagementObjectSearcher("Select * FROM Win32_NetworkAdapterConfiguration").Get();
            string empty = string.Empty;
            foreach (ManagementObject managementObject in objectCollection)
            {
                object obj = managementObject["MacAddress"];
                if (obj != null)
                {
                    if (empty == string.Empty)
                    empty = obj.ToString();
                    managementObject.Dispose();
                }
            }
            return empty.Replace(":", "");
        }

    }
}

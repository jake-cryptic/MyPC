using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using System.Management;
using System.Net;
using System.Diagnostics;


namespace Absx2.MyPC
{
    class MySystem
    {
        public string               Generated { get; set; }
        public List<List<string>>   RAM { get; set; }
        public List<List<string>>   CPU { get; set; }
        public List<List<string>>   GPU { get; set; }
        public List<string>         BSE { get; set; }
        public List<List<string>>   NIC { get; set; }
        public List<List<string>>   HDD { get; set; }
        public List<string>         INF { get; set; }
        public List<List<string>>   USR { get; set; }
    }

    class MyPC
    {
        public static List<List<string>> InfoRAM()
        {
            List<List<string>> Item = new List<List<string>>();
            ManagementObjectSearcher Searcher = new ManagementObjectSearcher("Select * from Win32_PhysicalMemory");
            foreach (ManagementObject data in Searcher.Get())
            {
                try
                {
                    List<string> it = new List<string>();
                    it.Add(data.GetPropertyValue("Manufacturer") != null ? data.GetPropertyValue("Manufacturer").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("Capacity") != null ? data.GetPropertyValue("Capacity").ToString() : "Unavailable");
                    Item.Add(it);
                }
                catch (ManagementException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return Item;
        }

        public static List<List<string>> InfoCPU()
        {
            List<List<string>> Item = new List<List<string>>();
            ManagementObjectSearcher Searcher = new ManagementObjectSearcher("Select * from Win32_Processor");
            foreach (ManagementObject data in Searcher.Get())
            {
                try
                {
                    List<string> it = new List<string>();
                    it.Add(data.GetPropertyValue("CurrentClockSpeed") != null ? data.GetPropertyValue("CurrentClockSpeed").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("CurrentVoltage") != null ? data.GetPropertyValue("CurrentVoltage").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("Manufacturer") != null ? data.GetPropertyValue("Manufacturer").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("MaxClockSpeed") != null ? data.GetPropertyValue("MaxClockSpeed").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("Name") != null ? data.GetPropertyValue("Name").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("NumberOfCores") != null ? data.GetPropertyValue("NumberOfCores").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("NumberOfLogicalProcessors") != null ? data.GetPropertyValue("NumberOfLogicalProcessors").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("SystemName") != null ? data.GetPropertyValue("SystemName").ToString() : "Unavailable");
                    Item.Add(it);
                }
                catch (ManagementException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return Item;
        }

        public static List<List<string>> InfoGPU()
        {
            List<List<string>> Item = new List<List<string>>();
            ManagementObjectSearcher Searcher = new ManagementObjectSearcher("Select * from Win32_VideoController");
            foreach (ManagementObject data in Searcher.Get())
            {
                try
                {
                    List<string> it = new List<string>();
                    it.Add(data.GetPropertyValue("AdapterCompatibility") != null ? data.GetPropertyValue("AdapterCompatibility").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("AdapterRAM") != null ? data.GetPropertyValue("AdapterRAM").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("DeviceID") != null ? data.GetPropertyValue("DeviceID").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("Name") != null ? data.GetPropertyValue("Name").ToString() : "Unavailable");
                    Item.Add(it);
                }
                catch (ManagementException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return Item;
        }

        public static List<string> InfoBSE()
        {
            List<string> Item = new List<string>();
            ManagementObjectSearcher Searcher = new ManagementObjectSearcher("Select * from Win32_BaseBoard");
            foreach (ManagementObject data in Searcher.Get())
            {
                try
                {
                    Item.Add(data.GetPropertyValue("HotSwappable") != null ? data.GetPropertyValue("HotSwappable").ToString() : "Unavailable");
                    Item.Add(data.GetPropertyValue("Manufacturer") != null ? data.GetPropertyValue("Manufacturer").ToString() : "Unavailable");
                    Item.Add(data.GetPropertyValue("Product") != null ? data.GetPropertyValue("Product").ToString() : "Unavailable");
                }
                catch (ManagementException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return Item;
        }

        public static List<List<string>> InfoNIC()
        {
            List<List<string>> Item = new List<List<string>>();
            ManagementObjectSearcher Searcher = new ManagementObjectSearcher("Select * from Win32_NetworkAdapter");
            foreach (ManagementObject data in Searcher.Get())
            {
                try
                {
                    List<string> it = new List<string>();
                    if (data.GetPropertyValue("PhysicalAdapter") != null || data.GetPropertyValue("PhysicalAdapter").ToString().ToLower() == "false")
                    {
                        if (data.GetPropertyValue("AdapterType") == null || data.GetPropertyValue("AdapterType").ToString() == "Tunnel")
                        {
                            it.Add("IS_TUNNEL_ADAPTER");
                        }
                        else if (data.GetPropertyValue("Manufacturer").ToString().ToLower().Contains("tap-win"))
                        {
                            it.Add("IS_TAP_ADAPTER");
                        }
                        else
                        {
                            it.Add(data.GetPropertyValue("Description") != null ? data.GetPropertyValue("Description").ToString() : "Unavailable");
                            it.Add(data.GetPropertyValue("Installed") != null ? data.GetPropertyValue("Installed").ToString() : "Unavailable");
                            //it.Add(data.GetPropertyValue("Product") != null ? data.GetPropertyValue("Product").ToString() : "Unavailable");
                            it.Add(data.GetPropertyValue("MACAddress") != null ? data.GetPropertyValue("MACAddress").ToString() : "Unavailable");
                            it.Add(data.GetPropertyValue("Manufacturer") != null ? data.GetPropertyValue("Manufacturer").ToString() : "Unavailable");
                            it.Add(data.GetPropertyValue("Installed") != null ? data.GetPropertyValue("Installed").ToString() : "Unavailable");
                            it.Add(data.GetPropertyValue("Name") != null ? data.GetPropertyValue("Name").ToString() : "Unavailable");
                            it.Add(data.GetPropertyValue("NetConnectionID") != null ? data.GetPropertyValue("NetConnectionID").ToString() : "Unavailable");
                            it.Add(data.GetPropertyValue("Speed") != null ? data.GetPropertyValue("Speed").ToString() : "0");
                        }
                    }
                    Item.Add(it);
                }
                catch (ManagementException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return Item;
        }

        public static List<List<string>> InfoHDD()
        {
            List<List<string>> Item = new List<List<string>>();
            ManagementObjectSearcher Searcher = new ManagementObjectSearcher("Select * from Win32_LogicalDisk");
            foreach (ManagementObject data in Searcher.Get())
            {
                try
                {
                    List<string> it = new List<string>();
                    it.Add(data.GetPropertyValue("Compressed") != null ? data.GetPropertyValue("Compressed").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("Description") != null ? data.GetPropertyValue("Description").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("DeviceID") != null ? data.GetPropertyValue("DeviceID").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("FileSystem") != null ? data.GetPropertyValue("FileSystem").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("FreeSpace") != null ? data.GetPropertyValue("FreeSpace").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("Size") != null ? data.GetPropertyValue("Size").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("VolumeName") != null ? data.GetPropertyValue("VolumeName").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("Caption") != null ? data.GetPropertyValue("Caption").ToString() : "Unavailable");
                    Item.Add(it);
                }
                catch (ManagementException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return Item;
        }

        public static List<string> InfoINF()
        {
            List<string> Item = new List<string>();
            ManagementObjectSearcher Searcher = new ManagementObjectSearcher("Select * from Win32_OperatingSystem");
            foreach (ManagementObject data in Searcher.Get())
            {
                try
                {
                    Item.Add(data.GetPropertyValue("Caption") != null ? data.GetPropertyValue("Caption").ToString() : "Unavailable");
                    Item.Add(data.GetPropertyValue("CountryCode") != null ? data.GetPropertyValue("CountryCode").ToString() : "Unavailable");
                    Item.Add(data.GetPropertyValue("InstallDate") != null ? data.GetPropertyValue("InstallDate").ToString() : "Unavailable");
                    Item.Add(data.GetPropertyValue("OSArchitecture") != null ? data.GetPropertyValue("OSArchitecture").ToString() : "Unavailable");
                    Item.Add(data.GetPropertyValue("NumberOfUsers") != null ? data.GetPropertyValue("NumberOfUsers").ToString() : "Unavailable");
                }
                catch (ManagementException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return Item;
        }

        public static List<List<string>> InfoUSR()
        {
            List<List<string>> Item = new List<List<string>>();
            ManagementObjectSearcher Searcher = new ManagementObjectSearcher("Select * from Win32_UserAccount");
            foreach (ManagementObject data in Searcher.Get())
            {
                try
                {
                    List<string> it = new List<string>();
                    it.Add(data.GetPropertyValue("Caption") != null ? data.GetPropertyValue("Caption").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("Description") != null ? data.GetPropertyValue("Description").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("Disabled") != null ? data.GetPropertyValue("Disabled").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("Name") != null ? data.GetPropertyValue("Name").ToString() : "Unavailable");
                    it.Add(data.GetPropertyValue("PasswordRequired") != null ? data.GetPropertyValue("PasswordRequired").ToString() : "Unavailable");
                    Item.Add(it);
                }
                catch (ManagementException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return Item;
        }
    }
}
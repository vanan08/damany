using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using Encryptor;
using Microsoft.Win32;
using ImageProcess;

namespace RemoteImaging
{
    public static class Util
    {

        private static string ProductRegistryPath = @"Software\Yufei\RemoteImaging";
        private static string IDRegistryName = "UUID";
        private static string KeyRegistryName = "Key";

        private static string GetKeyFile()
        {
            string keyFile = Path.Combine(Application.StartupPath, "key");
            return keyFile;
        }


        public static void WriteKey(string ID, string key)
        {
            string file = GetKeyFile();

            FileStream fs = File.OpenWrite(file);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(key);
            sw.Close();
        }

        public static bool VerifyKey()
        {
            string uuid = "";
            string key = "";

            ReadAuthentication(out uuid, out key);

            if (string.IsNullOrEmpty(uuid) || string.IsNullOrEmpty(key)) return false;

            string encodedSN = EncryptService.Encode(uuid);

            string decoded = EncryptService.Decode(key);

            return string.Compare(encodedSN, key, StringComparison.Ordinal) == 0;
        }



        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }

        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }

        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }


        private static string MAC()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            string MACAddress = String.Empty;
            foreach (ManagementObject mo in moc)
            {
                if (MACAddress == String.Empty)  // only return MAC Address from first card
                {
                    bool enabled = (bool)mo["IPEnabled"];

                    MACAddress = mo["MacAddress"].ToString();
                }
                mo.Dispose();
            }
            MACAddress = MACAddress.Replace(":", "");

            return EncryptService.Encode(MACAddress);
        }


        public static void WriteAuthentication(string UUID, string key)
        {
            RegistryKey productKey = Registry.LocalMachine.CreateSubKey(ProductRegistryPath);
            productKey.SetValue(IDRegistryName, UUID);
            productKey.SetValue(KeyRegistryName, key);
        }

        public static void ReadAuthentication(out string UUID, out string key)
        {
            try
            {
                RegistryKey productKey = Registry.LocalMachine.OpenSubKey(ProductRegistryPath);
                UUID = (string)productKey.GetValue(IDRegistryName, "");
                key = (string)productKey.GetValue(KeyRegistryName, "");
            }
            catch
            {
                UUID = null;
                key = null;
            }


        }


        public static string GetOrGenerateUniqID()
        {
            string uuid;
            string key;
            ReadAuthentication(out uuid, out key);


            if (string.IsNullOrEmpty(uuid))
                uuid = System.Guid.NewGuid().ToString();

            return uuid.ToUpper();
        }
    }
}

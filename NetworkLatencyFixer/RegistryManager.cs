using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkLatencyFixer
{
    class RegistryManager
    {
        public void SetValues(string interfaceId)
        {
            var keyId = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + interfaceId;

            SetValue(keyId, "TcpAckFrequency", 1, RegistryValueKind.DWord);
        }

        public Dictionary<string,string> GetAllNetworks()
        {
            var res = new Dictionary<string, string>();
            const string basePath = @"SYSTEM\CurrentControlSet\Control\Network\{4D36E972-E325-11CE-BFC1-08002BE10318}";
            using (var parent = Registry.LocalMachine.OpenSubKey(basePath))
            {

                foreach (var keyName in parent.GetSubKeyNames())
                {
                    var connRegKey = Registry.LocalMachine.OpenSubKey($"{basePath}\\{keyName}\\Connection");
                    if (connRegKey == null)
                        continue;

                    var val = connRegKey.GetValue("Name");
                    res.Add(keyName, val.ToString());
                }

                return res;
            }
        }

        public void DeleteValues(string interfaceId)
        {
            var keyId = @"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + interfaceId;
            using (var key = Registry.LocalMachine.OpenSubKey(keyId, true))
            {
                key.DeleteValue("TcpAckFrequency");
            }
        }

        private void SetValue(string keyId, string valueName, int valueValue, RegistryValueKind kind)
        {
            Registry.SetValue(keyId, valueName, valueValue, kind);
            Console.WriteLine($"Setting {valueName} to {valueValue}");
        }
    }
}

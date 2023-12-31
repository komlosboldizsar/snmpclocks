using BToolbox.Model;
using SNMPclocks.GUI;
using SNMPclocks.Model;
using SNMPclocks.SNMP;
using SNMPclocks.SNMP.Local;
using System.Text.Json;

namespace SNMPclocks
{
    internal static class Program
    {

        private const string PATH_CONFIG = "config.json";
        private const string PATH_CLOCKS = "clocks.json";

        [STAThread]
        static void Main()
        {
            Config config = loadConfig();
            ObservableList<Clock> clockList = loadAndInitClocks(out ClockListSerializer serializer);
            ObservableEnumerableItemsChangedDelegate<Clock> clockAddedOrRemovedHandler =
                (iwp) => serializer.Save();
            clockList.ItemsAdded += clockAddedOrRemovedHandler;
            clockList.ItemsRemoved += clockAddedOrRemovedHandler;
            SnmpAgent snmpAgent = initSnmp(config, clockList);
            ApplicationConfiguration.Initialize();
            MainForm mainForm = new(clockList, snmpAgent);
            mainForm.ClockEdited += (c) => serializer.Save();
            mainForm.FormClosing += (sender, eventArgs) => serializer.Save();
            Application.Run(mainForm);
        }

        private static Config loadConfig()
        {
            try
            {
                string configJson = File.ReadAllText(PATH_CONFIG);
                return JsonSerializer.Deserialize<Config>(configJson);
            }
            catch
            {
                return Config.Default;
            }
        }

        private static ObservableList<Clock> loadAndInitClocks(out ClockListSerializer serializer)
        {
            ObservableList<Clock> clockList = new();
            serializer = new(PATH_CLOCKS, clockList);
            serializer.Load();
            clockList.Foreach(c => c.Init());
            return clockList;
        }

        private static SnmpAgent initSnmp(Config config, ObservableList<Clock> clockList)
        {
            Func<Config.TrapReceiverVersion, TrapSendingConfig.TrapReceiverVersion> convertTrapReceiverVersion = (version)
                => version switch
                {
                    Config.TrapReceiverVersion.V1 => TrapSendingConfig.TrapReceiverVersion.V1,
                    Config.TrapReceiverVersion.V2 => TrapSendingConfig.TrapReceiverVersion.V2,
                    _ => TrapSendingConfig.TrapReceiverVersion.V1
                };
            TrapSendingConfig trapSendingConfig = new();
            foreach (Config.TrapReceiver trapReceiver in config.TrapReceivers)
                trapSendingConfig.AddReceiver(trapReceiver.IP, trapReceiver.Port, convertTrapReceiverVersion(trapReceiver.Version), trapReceiver.Community, trapReceiver.Filter, trapReceiver.SendMyIP);
            SnmpAgent snmpAgent = new(config.Port, config.CommunityRead, config.CommunityWrite, trapSendingConfig);
            snmpAgent.Start();
            _ = new DataTableBoundObjectStoreAdapter<Clock, ClockSnmpDataTable>(snmpAgent, clockList);
            return snmpAgent;
        }

    }
}
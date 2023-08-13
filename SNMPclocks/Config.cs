using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SNMPclocks
{
    public class Config
    {

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("communityRead")]
        public string CommunityRead { get; set; }

        [JsonPropertyName("communityWrite")]
        public string CommunityWrite { get; set; }

        [JsonPropertyName("trapReceivers")]
        public TrapReceiver[] TrapReceivers { get; set; }

        public class TrapReceiver
        {

            [JsonPropertyName("ip")]
            public string IP { get; set; }

            [JsonPropertyName("port")]
            public int Port { get; set; } = 162;

            [JsonPropertyName("version")]
            public TrapReceiverVersion Version { get; set; } = TrapReceiverVersion.V1;

            [JsonPropertyName("community")]
            public string Community { get; set; } = "public";

            [JsonPropertyName("filter")]
            public string[] Filter { get; set; } = null;

            [JsonPropertyName("sendMyIp")]
            public bool SendMyIP { get; set; } = false;

        }

        public enum TrapReceiverVersion
        {
            V1,
            V2
        }

        public static readonly Config Default = new()
        {
            Port = 161,
            CommunityRead = "public",
            CommunityWrite = "private",
            TrapReceivers = Array.Empty<TrapReceiver>()
        };

    }
}

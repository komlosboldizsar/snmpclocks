using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib.Pipeline;
using System.Net;
using System.Net.Sockets;

namespace SNMPclocks.SNMP
{
    public class SnmpAgent
    {

        private readonly int _port;
        private readonly string _communityRead;
        private readonly string _communityWrite;

        public readonly MyObjectStore ObjectStore = new();
        private SnmpEngine _engine;

        public SnmpAgent(int port, string communityRead, string communityWrite)
        {
            _port = port;
            _communityRead = communityRead;
            _communityWrite = communityWrite;
            createEngine();
        }

        private void createEngine()
        {
            IMembershipProvider v1MembershipProvider = new Version1MembershipProvider(new OctetString(_communityRead), new OctetString(_communityWrite));
            IMembershipProvider v2MembershipProvider = new Version2MembershipProvider(new OctetString(_communityRead), new OctetString(_communityWrite));
            IMembershipProvider membershipProvider = new ComposedMembershipProvider(new IMembershipProvider[] {
                v1MembershipProvider,
                v2MembershipProvider
            });
            var handlerFactory = new MessageHandlerFactory(new[]
            {
                new HandlerMapping("v1", "GET", new GetV1MessageHandler()),
                new HandlerMapping("v1", "GETNEXT", new GetNextV1MessageHandler()),
                new HandlerMapping("v1", "SET", new MySetV1MessageHandler()),
                new HandlerMapping("v2", "GET", new GetMessageHandler()),
                new HandlerMapping("v2", "GETNEXT", new GetNextMessageHandler()),
                new HandlerMapping("v2", "GETBULK", new GetBulkMessageHandler()),
                new HandlerMapping("v2", "SET", new MySetMessageHandler())
            });
            var pipelineFactory = new SnmpApplicationFactory(new MyLogger(), ObjectStore, membershipProvider, handlerFactory);
            _engine = new SnmpEngine(pipelineFactory, new Listener(), new EngineGroup());
        }

        public void Start()
        {
            try
            {
                _engine.Listener.ClearBindings();
                if (Socket.OSSupportsIPv4)
                    _engine.Listener.AddBinding(new IPEndPoint(IPAddress.Any, _port));
                _engine.Start();
            }
            catch (Exception)
            {
                //LogDispatcher.E($"Couldn't start SNMP service at UDP port {_port}, because IP endpoint is in use by another application.");
            }
        }

        private class MyLogger : ILogger
        {
            public void Log(ISnmpContext context) { }
        }

        public const string OID_BASE = "1.3.6.1.4.1.59150.2";

    }
}

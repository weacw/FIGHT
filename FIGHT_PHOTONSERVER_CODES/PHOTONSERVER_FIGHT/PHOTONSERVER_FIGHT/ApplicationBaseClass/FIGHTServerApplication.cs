using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using Photon.SocketServer;
using PHOTONSERVER_FIGHT.ApplicationBaseClass;
using PHOTONSERVER_FIGHT.Handlers;

namespace PHOTONSERVER_FIGHT
{
    public class FIGHTserverapplication : ApplicationBase
    {
        public Dictionary<byte, Handlerbase> handlers = new Dictionary<byte, Handlerbase>();
        public Dictionary<string, Clientpeer> clientpeers = new Dictionary<string, Clientpeer>();
        public Dictionary<int, Room> rooms = new Dictionary<int, Room>();

        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        private static FIGHTserverapplication instance { get; set; }

        public FIGHTserverapplication()
        {
            instance = this;
            
        }

        public static FIGHTserverapplication Getfightserverapplication()
        {
            return instance;
        }



        private void Registerhandler()
        {
            Type[] types = Assembly.GetAssembly(typeof(Handlerbase)).GetTypes();
            foreach (Type type in types)
            {
                if (type.FullName.EndsWith("handler"))
                    Activator.CreateInstance(type);
            }
        }









        //Photon fucntion

        protected override PeerBase CreatePeer(InitRequest _initRequest)
        {
            log.Info("Client peer has been created!");
            Clientpeer peer = new Clientpeer(_initRequest.Protocol, _initRequest.PhotonPeer);
            return peer;
        }

        protected override void Setup()
        {
            ExitGames.Logging.LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            GlobalContext.Properties["LogFileName"] = this.ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));
            string serverlog = string.Format("\n\ndata {0} \n-----------\n", System.DateTime.Now);
            log.Info(serverlog + "Application setup complete.");

            Registerhandler();
        }

        protected override void TearDown()
        {
            string serverlog = string.Format("\ndata{0} \n-----------\n", System.DateTime.Now);
            log.Info(serverlog + "fight server application tear down");
        }
    }
}

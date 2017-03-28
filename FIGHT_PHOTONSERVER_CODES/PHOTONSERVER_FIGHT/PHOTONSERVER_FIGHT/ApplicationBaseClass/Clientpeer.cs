using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Logging;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using PHOTONSERVER_FIGHT.Handlers;
using PHOTONSERVER_FIGHT.Units;

namespace PHOTONSERVER_FIGHT.ApplicationBaseClass
{
    /**
	*
	* 功 能： N/A
	* 类 名： Clientpeer	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/

    public class Clientpeer : PeerBase
    {

        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public Playerdata playerdata;
        public bool isjoinedroom = false;
        public Room joinedroom = null;

        private Defaultdata defaultdata;

        public class Defaultdata
        {
            public string defaultname;
            public string defaultid;
        }


        public Clientpeer(IRpcProtocol _protocol, IPhotonPeer _unmanagedPeer) : base(_protocol, _unmanagedPeer)
        {
            defaultdata = new Defaultdata();
            defaultdata.defaultid = this.GetHashCode().ToString();
            defaultdata.defaultname = "connected use|"+_unmanagedPeer.GetRemoteIP()+" ";
        }

        protected override void OnOperationRequest(OperationRequest _operationrequest, SendParameters _sendparameters)
        {
            Handlerbase handler;
            FIGHTserverapplication.Getfightserverapplication()
                .handlers.TryGetValue(_operationrequest.OperationCode, out handler);
            OperationResponse response = new OperationResponse();
            response.OperationCode = _operationrequest.OperationCode;
            response.Parameters = new Dictionary<byte, object>();
            if (handler != null)
            {
                handler.OnHandlerMessage(_operationrequest, response, this, _sendparameters);
            }
            else
            {
                log.Info("can not find handler from operation code : " + _operationrequest.OperationCode);
            }
        }

        protected override void OnDisconnect(DisconnectReason _reasoncode, string _reasondetail)
        {
            if (!isjoinedroom)
            {
                log.Info(_reasoncode+":"+_reasoncode+"|"+ defaultdata.defaultid+"-"+ defaultdata.defaultname+"has been disconnected.");
                defaultdata.defaultid = null;
                defaultdata.defaultname = null;
                defaultdata = null;
                return;
            }
            joinedroom.Exitintheroom(this);
            if (FIGHTserverapplication.Getfightserverapplication().clientpeers.ContainsKey(playerdata.playername))
                FIGHTserverapplication.Getfightserverapplication().clientpeers.Remove(playerdata.playername);
            playerdata = null;
        }
    }
}

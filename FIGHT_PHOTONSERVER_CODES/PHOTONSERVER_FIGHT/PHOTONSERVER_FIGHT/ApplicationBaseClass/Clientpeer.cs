﻿namespace PHOTONSERVER_FIGHT.ApplicationBaseClass
{
    using System.Collections.Generic;
    using ExitGames.Logging;
    using Photon.SocketServer;
    using PhotonHostRuntimeInterfaces;
    using Handlers;
    using Units;
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
        #region 全局变量区域
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
        #endregion


        public Clientpeer(IRpcProtocol _protocol, IPhotonPeer _unmanagedPeer) : base(_protocol, _unmanagedPeer)
        {
            defaultdata = new Defaultdata();
            defaultdata.defaultid = this.GetHashCode().ToString();
            defaultdata.defaultname = "connected use|" + _unmanagedPeer.GetRemoteIP() + " ";
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
                SendOperationResponse(response, _sendparameters);
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
                if (playerdata == null)
                {
                    log.Info(_reasoncode + ":" + _reasoncode + "|" + defaultdata.defaultid + "-" + defaultdata.defaultname + "has been disconnected.");
                    //释放默认信息
                    defaultdata.defaultid = null;
                    defaultdata.defaultname = null;
                    defaultdata = null;
                    return;
                }

                log.Info(_reasoncode + ":" + _reasoncode + "|" + playerdata.playerid + "-" + playerdata.playername + "has been disconnected.");
                if (FIGHTserverapplication.Getfightserverapplication().clientpeers.ContainsKey(playerdata.playername))
                    FIGHTserverapplication.Getfightserverapplication().clientpeers.Remove(playerdata.playername);


                //释放玩家信息
                playerdata.playername = null;
                playerdata.playerid = 0;
                playerdata = null;

                //释放默认信息
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

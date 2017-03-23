using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightServer.Common;
using Photon.SocketServer;

namespace Fight.Handler
{
    /**
	*
	* 功 能： N/A
	* 类 名： SignOff	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    public class SignOff:HandlerBase
    {
        public override OperationCode OpCode { get; }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, FightUnityClientPeer peer,
            SendParameters sendParameters)
        {
            if (FightServer.GetFightServer().fightUnityClientPeers.ContainsKey(peer.GetPlayerDetails.PlayerName))
            {
                FightServer.GetFightServer().fightUnityClientPeers.Remove(peer.GetPlayerDetails.PlayerName);
            }
        }
    }
}

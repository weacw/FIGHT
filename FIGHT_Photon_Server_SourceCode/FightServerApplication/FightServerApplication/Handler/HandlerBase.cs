using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Logging;
using FightServer.Common;
using Photon.SocketServer;

namespace Fight.Handler
{
    /**
	*
	* 功 能： N/A
	* 类 名： HandlerBase	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/

    public abstract class HandlerBase
    {
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        public abstract OperationCode OpCode { get; }

        public HandlerBase()
        {
            FightServer.GetFightServer().handlers.Add((byte) OpCode, this);
            log.Debug("Handler:" + GetType().Name + " is register");
        }

        public abstract void OnHandlerMessage(OperationRequest request, OperationResponse response,
            FightUnityClientPeer peer, SendParameters sendParameters);
    }
}

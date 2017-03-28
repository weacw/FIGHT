using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Logging;
using FightServer.Common;
using Photon.SocketServer;
using PHOTONSERVER_FIGHT.ApplicationBaseClass;

namespace PHOTONSERVER_FIGHT.Handlers
{
    /**
	*
	* 功 能： N/A
	* 类 名： Handlerbase	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/

    public abstract class Handlerbase
    {
        protected static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();



        public Handlerbase()
        {
            log.Info("11111" + Opcode.ToString());
            FIGHTserverapplication.Getfightserverapplication().handlers.Add((byte)Opcode, this);
            log.Info("Handler: " + GetType().Name + " is registered");
        }

        public abstract void OnHandlerMessage(OperationRequest _request, OperationResponse _response, Clientpeer _peer,
            SendParameters _sendparameters);
        public abstract Operationcode Opcode { get; }
    }
}

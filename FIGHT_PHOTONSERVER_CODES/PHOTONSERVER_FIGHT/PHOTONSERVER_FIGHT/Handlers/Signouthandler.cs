using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightServer.Common;
using Photon.SocketServer;
using PHOTONSERVER_FIGHT.ApplicationBaseClass;

namespace PHOTONSERVER_FIGHT.Handlers
{
    /**
	*
	* 功 能： N/A
	* 类 名： Signouthandler	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Signouthandler:Handlerbase
    {
        public override Operationcode Opcode => Operationcode.SIGNOUT;

        public override void OnHandlerMessage(OperationRequest _request, OperationResponse _response, Clientpeer _peer,
            SendParameters _sendparameters)
        {
            throw new NotImplementedException();
        }
    }
}

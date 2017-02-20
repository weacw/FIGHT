using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightServer.Common;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace Fight.Operations
{
    /**
	*
	* 功 能： N/A
	* 类 名： BaseOperation	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/

    public class BaseOperation : Operation
    {
        public BaseOperation(IRpcProtocol protocol, OperationRequest request) : base(protocol, request)
        {
        }

        public virtual OperationResponse GetResponse(ErrorCode errorCode, string debugeMessage = "")
        {
            var response = new OperationResponse(OperationRequest.OperationCode);
            response.ReturnCode = (short) errorCode;
            response.DebugMessage = debugeMessage;
            return response;            
        }
    }
}

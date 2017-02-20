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
	* 类 名： Login	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Login:BaseOperation
    {
        public Login(IRpcProtocol protocol, OperationRequest request) : base(protocol, request)
        {
        }
        [DataMember(Code =(byte)ParameterCode.CharacterName)]
        public string CharacterName { get; set; }
    }
}

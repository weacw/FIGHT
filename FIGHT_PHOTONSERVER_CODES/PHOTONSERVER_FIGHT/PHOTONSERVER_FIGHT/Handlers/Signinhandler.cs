using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightServer.Common;
using Photon.SocketServer;
using PHOTONSERVER_FIGHT.ApplicationBaseClass;
using PHOTONSERVER_FIGHT.Units;

namespace PHOTONSERVER_FIGHT.Handlers
{
    /**
	*
	* 功 能： 用于处理客户端发起登入的请求
	* 类 名： SIGNIN	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Signinhandler:Handlerbase
    {
        public override Operationcode Opcode => Operationcode.SIGNIN;

        public override void OnHandlerMessage(OperationRequest _request, OperationResponse _response, Clientpeer _peer,
            SendParameters _sendparameters)
        {
            //提取当前发起登入客户端所发送的数据
            object playerobj = null;
            _request.Parameters.TryGetValue((byte) Parametercode.CHARACTERNAME, out playerobj);
            if (playerobj == null) return;
            string playername = (string) playerobj;

            //检查服务器中是否已经存在当前发起登入的客户端用户名称
            if (FIGHTserverapplication.Getfightserverapplication().clientpeers.ContainsKey(playername))
            {
                log.Info("Character name is already exists");
                _response.ReturnCode = (byte) Returncode.CHARACTERNAMEISEXIST;
                return;
            }

            //添加当前登入的客户端到服务器用户标内
            FIGHTserverapplication.Getfightserverapplication().clientpeers.Add(playername,_peer);
            log.Info("Character name: "+playername+ " is Successful login");

            //创建用户数据
            Playerdata playerdata = new Playerdata();
            playerdata.playername = playername;
            playerdata.playerid = playerdata.GetHashCode();

            //链接用户数据到当前登入的客户端
            _peer.playerdata = playerdata;

            //反馈客户端登入操作完成并且成功
            _response.ReturnCode = (byte) Returncode.CHARACTERCREATED;
        }
    }
}

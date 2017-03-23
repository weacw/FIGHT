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
	* 功 能： 用户登录
	* 类 名： LoginHandler	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    public class SignIn : HandlerBase
    {
        public override OperationCode OpCode => OperationCode.SignIn;

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, FightUnityClientPeer peer,
            SendParameters sendParameters)
        {
            //获取客户端发送的请求
            object playerObj = null;

            request.Parameters.TryGetValue((byte)ParameterCode.CharacterName, out playerObj);
            if (playerObj == null) return;

            string player= (string)playerObj;
            //判断当前用户名是否存在
            if (FightServer.GetFightServer().fightUnityClientPeers.ContainsKey(player))
            {
                //若存在提示用户，用户名已存在。
                FightServer.Log.Info("Character name isarealdy");
                response.ReturnCode = (byte) ReturnCode.NameIsExist;
                return;
            }

            //若不存在，往fight unity client peers 字典内添加
            FightServer.GetFightServer().fightUnityClientPeers.Add(player, peer);
            FightServer.Log.Info("Character name: "+playerObj+ " is logined");
            
            //用户细节信息
            PlayerDetails playerDetails = new PlayerDetails();
            playerDetails.PlayerName = player;
            playerDetails.PlayerID = GetHashCode();
            peer.GetPlayerDetails = playerDetails;
            response.ReturnCode = (byte) ReturnCode.Ok;
        }
    }
}

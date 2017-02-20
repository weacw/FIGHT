using ExitGames.Logging;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace Fight
{
    /**
	*
	* 功 能：服务器与客户端信息传输
	* 类 名： FightUnityClientPeer	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    public class FightUnityClientPeer:PeerBase
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public int roleID;

        /// <summary>
        /// 初始化fight client peer
        /// </summary>
        /// <param name="protocol">协议</param>
        /// <param name="unmanagedPeer">peer对象</param>
        public FightUnityClientPeer(IRpcProtocol protocol, IPhotonPeer unmanagedPeer) : base(protocol, unmanagedPeer)
        {
        }

        /// <summary>
        /// 发送消息给客户端
        /// </summary>
        /// <param name="operationRequest">操作请求</param>
        /// <param name="sendParameters">发送数据</param>
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
          
        }

        /// <summary>
        /// 客户端失去连接调用。
        /// 用于销毁客户端产生的资源
        /// </summary>
        /// <param name="reasonCode">断开代码</param>
        /// <param name="reasonDetail">断开原因细节</param>
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {            
        }
    }
}

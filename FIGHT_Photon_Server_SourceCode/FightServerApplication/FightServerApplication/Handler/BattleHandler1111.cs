using ExitGames.Logging;
using FightServer.Common;
using FightServer.Common.Tools;
using Photon.SocketServer;

namespace Fight.Handler
{
    /**
	*
	* 功 能： N/A
	* 类 名： BattleHandler	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/

    public class BattleHandler1111 : HandlerBase
    {
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        public override OperationCode OpCode { get; }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, FightUnityClientPeer peer, SendParameters sendParameters)
        {
            SubOperateionCode subCode = ParameterTool.GetSubOperateionCode(request.Parameters);
            ParameterTool.AddSubOperationCode(request.Parameters, subCode);
            switch (subCode)
            {

                case SubOperateionCode.SyncPositionAndRotation:
                    //object position = null;
                    //request.Parameters.TryGetValue((byte)ParameterCode.Position, out position);

                    //object eulerAngle = null;
                    //request.Parameters.TryGetValue((byte)ParameterCode.EulerAngle, out eulerAngle);
                    //foreach (FightUnityClientPeer unityClientPeer in FightServer.GetFightServer().roomList)
                    //{
                    //    if (unityClientPeer == peer) continue;
                    //    SendEventWithPeer(peer, OpCode, SubOperateionCode.SyncPositionAndRotation, peer.GetPlayerDetails.PlayerID, position,
                    //        eulerAngle);
                    //}
                    break;
                case SubOperateionCode.SyncAnimation:
                    break;
                case SubOperateionCode.SyncBallteStatus:
                    break;
            }
        }




        private void SendEventWithPeer(FightUnityClientPeer peer, OperationCode OpCode, SubOperateionCode subCode, int roleID, object position, object eulerAngle)
        {

        }
    }
}

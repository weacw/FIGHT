using ExitGames.Logging;
using FightServer.Common;
using Photon.SocketServer;

namespace Fight.Handler
{
    /**
	*
	* 功 能： N/A
	* 类 名： CreateRoomHandler	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    public class CreateRoomHandler:HandlerBase
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public override OperationCode OpCode => OperationCode.Room;

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, FightUnityClientPeer peer,
            SendParameters sendParameters)
        {
            //解析客户端发来的请求数据信息
           object roomObject;
            response.Parameters.TryGetValue((byte)ParameterCode.RoomSetting, out roomObject);
            if (roomObject == null) return;

            string roomName=(string)roomObject;

            //设置房间细节信息
            RoomSetting roomsetting =new RoomSetting();
            roomsetting.RoomName = roomName;
            roomsetting.RoomId = roomsetting.GetHashCode();

            if (FightServer.GetFightServer().roomDictionary.ContainsKey(roomsetting.RoomName))
            {
                Log.Info("The room is exited");
                return; 
            }

            Room room = new Room(peer, roomsetting);

            //创建房间成功，将房间加入房间列表
            FightServer.GetFightServer().roomDictionary.Add(room.Setting.RoomName, room);

            Log.Info(roomsetting.RoomName+" is created.\n The room master is "+room.RoomMaster.GetPlayerDetails.PlayerName);
        }
    }
}

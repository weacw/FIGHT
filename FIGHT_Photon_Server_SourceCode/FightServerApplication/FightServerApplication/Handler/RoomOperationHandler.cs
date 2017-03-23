using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Logging;
using FightServer.Common;
using FightServer.Common.Tools;
using LitJson;
using Photon.SocketServer;

namespace Fight.Handler
{
    /**
	*
	* 功 能： N/A
	* 类 名： RoomOperationHandler	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    class RoomOperationHandler : HandlerBase
    {

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();


        public override OperationCode OpCode => OperationCode.Room;
        private string roomName,roomPSD;
        private Room room;
        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, FightUnityClientPeer peer,
            SendParameters sendParameters)
        {
            SubOperateionCode subOperateionCode = ParameterTool.GetSubOperateionCode(request.Parameters);
            Log.Info(subOperateionCode);
            Log.Info(request.Parameters.ToString());
            ParameterTool.AddSubOperationCode(response.Parameters, subOperateionCode);
            switch (subOperateionCode)
            {
                case SubOperateionCode.CraeteRoom:
                    CreateRoom(response, peer);
                    break;
                case SubOperateionCode.JoinRoom:
                    JoinRoom(response, peer);
                    break;
                case SubOperateionCode.GetRoomList:
                    GetRoomList(response, peer);
                    break;

                case SubOperateionCode.SyncPlayerInRoom:
                    break;
                case SubOperateionCode.LeaveRoom:
                    LeaveRoom(response, peer);
                    break;
            }
        }




        /// <summary>
        /// 离开房间
        /// </summary>
        /// <param name="response"></param>
        /// <param name="peer"></param>
        private void LeaveRoom(OperationResponse response, FightUnityClientPeer peer)
        {
            object room;
            response.ReturnCode = (byte)ReturnCode.LeaveRoom;
            response.Parameters.TryGetValue((byte)ParameterCode.RoleID, out room);
            Room item = FightServer.GetFightServer().roomDictionary[room.ToString()];
            if (item == null) return;
            item.OnRoomExit(peer);            

            //TODO:房间内广播所有客户端 peer 已经离开房间了
        }

        /// <summary>
        /// 获取服务器中房间列表
        /// </summary>
        /// <param name="response"></param>
        /// <param name="peeer"></param>
        private void GetRoomList(OperationResponse response, FightUnityClientPeer peeer)
        {
            response.ReturnCode = (byte)ReturnCode.GetRoomList;
            response.Parameters = new Dictionary<byte, object>();
            List<RoomSetting> roomNameList = new List<RoomSetting>();
            foreach (KeyValuePair<string, Room> pair in FightServer.GetFightServer().roomDictionary)
            {
                roomNameList.Add(pair.Value.Setting);
            }


            ParameterTool.AddParameter(response.Parameters, ParameterCode.RoomListCount, FightServer.GetFightServer().roomDictionary.Count, false);

            //list to object 
            ParameterTool.AddParameter(response.Parameters, ParameterCode.RoomListDetails, roomNameList, true);
        }



        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="response">反馈至客户端</param>
        /// <param name="peer">加入房间的peer</param>
        private void JoinRoom(OperationResponse response, FightUnityClientPeer peer)
        {
            object roomNameObject,roomPsdObject;
            response.Parameters.TryGetValue((byte)ParameterCode.RoleID, out roomNameObject);
            response.Parameters.TryGetValue((byte)ParameterCode.RoomPassword, out roomPsdObject);
            
            if (roomNameObject == null) return;
            roomName = roomNameObject.ToString();
            roomPSD = roomPsdObject.ToString();
            response.ReturnCode = (byte)ReturnCode.JoinRoom;
            FightServer.GetFightServer().roomDictionary.TryGetValue(roomName, out room);
            Log.Info(room.Setting.RoomName);
            if (room != null) room.JoinRoom(peer,roomPSD);

            //TODO：房间内广播 peer已经加入房间了
        }

        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="response">反馈至客户端</param>
        /// <param name="peer">创建房间的peer</param>
        private void CreateRoom(OperationResponse response, FightUnityClientPeer peer)
        {
            Log.Info("Create room");
            //解析客户端发来的请求数据信息
            object roomObject;
            response.Parameters.TryGetValue((byte) ParameterCode.RoomParmeters, out roomObject);
            if (roomObject == null) return;

            //设置房间细节信息
            RoomSetting roomsetting = JsonMapper.ToObject<RoomSetting>(roomObject.ToString());

            if (FightServer.GetFightServer().roomDictionary.ContainsKey(roomsetting.RoomId.ToString()))
            {
                Log.Info("The room is exited");
                response.ReturnCode = (byte)ReturnCode.RoomExist;
                return;
            }
            room = new Room(peer, roomsetting);
            //创建房间成功，将房间加入房间列表
            FightServer.GetFightServer().roomDictionary.Add(room.Setting.RoomId.ToString(), room);
            Log.Info(roomsetting.RoomName + " is created.\n The room master is " +
                     room.RoomMaster.GetPlayerDetails.PlayerName);
            ParameterTool.AddParameter(response.Parameters, ParameterCode.RoomParmeters, room, true);
            response.ReturnCode = (byte) ReturnCode.Ok;
        }

        /// <summary>
        /// 发送事件至peer
        /// </summary>
        /// <param name="peer">发送的peer</param>
        /// <param name="opCode">操作码</param>
        /// <param name="subOperateionCode">子操作码</param>
        /// <param name="playerDetails">玩家信息</param>
        /// <param name="masterID">房主id</param>
        private void SendEventToPeer(FightUnityClientPeer peer, OperationCode opCode,
            SubOperateionCode subOperateionCode, List<PlayerDetails> playerDetails, int masterID)
        {
            EventData eventData = new EventData();
            eventData.Parameters = new Dictionary<byte, object>();
            ParameterTool.AddParameter(eventData.Parameters, ParameterCode.OperationCode, opCode, false);
            ParameterTool.AddSubOperationCode(eventData.Parameters, subOperateionCode);
            ParameterTool.AddParameter(eventData.Parameters, ParameterCode.PlayerListCount, playerDetails);
            ParameterTool.AddParameter(eventData.Parameters, ParameterCode.MasterID, masterID);
            peer.SendEvent(eventData, new SendParameters());
        }
    }
}

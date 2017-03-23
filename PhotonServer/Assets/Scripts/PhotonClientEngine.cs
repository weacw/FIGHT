/*
* 功 能： N/A
* 类 名： DynamicRandomMapCreator	
* Email:  paris3@163.com
* 作 者： NSWell-weacw 
* Copyright (c) weacw. All rights reserved.
*/

using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using ExitGames.Client.Photon;
using FightServer.Common;
using FightServer.Common.Tools;
using LitJson;

namespace WEACW
{
    public class PhotonClientEngine : MonoBehaviour, IPhotonPeerListener
    {

        private static PhotonClientEngine instance;

        public bool IsConnected { get; private set; }
        public bool IsLogin { get; private set; }

        public GetRoomlistHandler getRoomlistHandler;


        private PhotonPeer peer;
        private List<RoomSetting> roomList;
        public static PhotonClientEngine GetPhotonClientEngine()
        {
            return instance ?? (instance = FindObjectOfType<PhotonClientEngine>());
        }



        //Unity call back funtion

        private void Start()
        {
            peer = new PhotonPeer(this, ConnectionProtocol.Tcp);
            peer.Connect("127.0.0.1:4530", "Fight");
            Application.runInBackground = true;
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (peer != null)
                peer.Service();
        }






        #region Client send requset function

        /// <summary>
        /// 发送加入房间请求
        /// </summary>
        /// <param name="roomID">要加入的房间名称</param>
        public void JoinRoomRequest(string roomID, string psd)
        {
            Dictionary<byte, object> dict = new Dictionary<byte, object>();
            dict.Add((byte)ParameterCode.RoleID, roomID);
            dict.Add((byte)ParameterCode.RoomPassword, psd);
            dict.Add((byte)ParameterCode.SubOperationCode, SubOperateionCode.JoinRoom);
            peer.OpCustom((byte)OperationCode.Room, dict, true);
        }

        /// <summary>
        /// 发送登陆请求
        /// </summary>
        /// <param name="characterName">登陆用户名</param>
        public void LoginRequset(string characterName)
        {
            peer.OpCustom((byte)OperationCode.SignIn,
                new Dictionary<byte, object>() { { (byte)ParameterCode.CharacterName, characterName } }, true);
        }

        /// <summary>
        /// 发送创建房间请求
        /// </summary>
        /// <param name="roomName">房间名称</param>
        public void CraeteRoomRequest(string roomName, string password, int people)
        {
            int maxPeople = 0;
            Dictionary<byte, object> dict = new Dictionary<byte, object>();

            //设置房间参与的最多人数
            switch (people)
            {
                case 0:
                    maxPeople = 2;
                    break;
                case 1:
                    maxPeople = 4;
                    break;
                case 2:
                    maxPeople = 6;
                    break;
            }

            RoomSetting room = new RoomSetting();
            room.RoomId = GetHashCode();
            room.RoomName = roomName;
            room.RoomPassword = password;
            room.RoomPeople = maxPeople;

            //创建房间操作
            dict.Add((byte)ParameterCode.SubOperationCode, SubOperateionCode.CraeteRoom);
            ParameterTool.AddParameter(dict, ParameterCode.RoomParmeters, room, true);
            peer.OpCustom((byte)OperationCode.Room, dict, true);
        }

        /// <summary>
        /// 发送离开房间请求
        /// </summary>
        public void LeavingRoomRequset(string roomID)
        {
            Dictionary<byte, object> dict = new Dictionary<byte, object>();
            dict.Add((byte)ParameterCode.SubOperationCode, SubOperateionCode.LeaveRoom);
            dict.Add((byte)ParameterCode.RoleID, roomID);
            peer.OpCustom((byte)OperationCode.Room, dict, true);
        }


        /// <summary>
        /// 发送获取房间列表请求
        /// </summary>
        /// <param name="room">房间列表信息</param>
        public void GetRoomListRequest()
        {
            Dictionary<byte, object> dict = new Dictionary<byte, object>();
            dict.Add((byte)ParameterCode.SubOperationCode, SubOperateionCode.GetRoomList);
            peer.OpCustom((byte)OperationCode.Room, dict, true);
        }

        /// <summary>
        /// 同步房间内的玩家信息
        /// </summary>
        public void SyncPlayerInRoom()
        {

        }
        #endregion




        //------Photon call back function

        public void DebugReturn(DebugLevel level, string message)
        {
            Debug.Log(level + " : " + message);
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            Debug.Log(operationResponse.ReturnCode.ToString());
            object obj;
            switch (operationResponse.ReturnCode)
            {
                case (byte)ReturnCode.Ok:
                    ReturnCodeOK(operationResponse);
                    break;
                case (byte)ReturnCode.CreateRoom:
                    if (operationResponse.Parameters.ContainsKey((byte) ParameterCode.RoomListDetails))
                    {
                        operationResponse.Parameters.TryGetValue((byte) ParameterCode.RoomListDetails, out obj);
                        roomList = JsonMapper.ToObject<List<RoomSetting>>(obj.ToString());
                        if (null != getRoomlistHandler)
                            getRoomlistHandler.Invoke(0, roomList);
#if UNITY_EDITOR
                        Debug.Log("Join room returnCode:" + obj);
#endif
                    }
                    break;
            }
        }

        private void ReturnCodeOK(OperationResponse operationResponse)
        {
            object obj=null;

            switch (operationResponse.OperationCode)
            {
                case (byte)OperationCode.SignIn:
                    operationResponse.Parameters.TryGetValue((byte)ParameterCode.CharacterName, out obj);
                    IsLogin = true;
                    break;
                case (byte)OperationCode.Room:
                //TODO: room create
                Debug.Log("Room create");
                    break;
            }


#if UNITY_EDITOR
            Debug.Log("SignIn returnCode:" + obj);
#endif
        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.Connect:
                    IsConnected = true;
                    Debug.Log("The client is already connected to the Fight server");
                    break;
                case StatusCode.Disconnect:
                    break;
                case StatusCode.Exception:
                    break;
                case StatusCode.ExceptionOnConnect:
                    break;
                case StatusCode.SecurityExceptionOnConnect:
                    break;
                case StatusCode.QueueOutgoingReliableWarning:
                    break;
                case StatusCode.QueueOutgoingUnreliableWarning:
                    break;
                case StatusCode.SendError:
                    break;
                case StatusCode.QueueOutgoingAcksWarning:
                    break;
                case StatusCode.QueueIncomingReliableWarning:
                    break;
                case StatusCode.QueueIncomingUnreliableWarning:
                    break;
                case StatusCode.QueueSentWarning:
                    break;
                case StatusCode.InternalReceiveException:
                    break;
                case StatusCode.TimeoutDisconnect:
                    break;
                case StatusCode.DisconnectByServer:
                    break;
                case StatusCode.DisconnectByServerUserLimit:
                    break;
                case StatusCode.DisconnectByServerLogic:
                    break;
                case StatusCode.TcpRouterResponseOk:
                    break;
                case StatusCode.TcpRouterResponseNodeIdUnknown:
                    break;
                case StatusCode.TcpRouterResponseEndpointUnknown:
                    break;
                case StatusCode.TcpRouterResponseNodeNotReady:
                    break;
                case StatusCode.EncryptionEstablished:
                    break;
                case StatusCode.EncryptionFailedToEstablish:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("statusCode", statusCode, null);
            }
        }

        public void OnEvent(EventData eventData)
        {
        }

        public void OnDisable()
        {
            if (peer != null)
                peer.Disconnect();
        }
        public void OnApplicationQuit()
        {
            if (peer != null)
                peer.Disconnect();
        }

    }



    /// <summary>
    /// 房间设置
    /// </summary>
    public class RoomSetting
    {
        //房间名称
        public string RoomName { get; set; }

        //房间id
        public int RoomId { get; set; }

        //房间密码
        public string RoomPassword { get; set; }
        public int RoomPeople { get; set; }
    }
}

using System;
using System.Collections.Generic;
using ExitGames.Logging;
using FightServer.Common;
using Photon.SocketServer;


namespace Fight
{
    /**
	*
	* 功 能： 游戏房间单位
	* 类 名： Room	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Room
    {

        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        //房间中所有玩家（包含房主）
        public List<FightUnityClientPeer> clientList = new List<FightUnityClientPeer>();



        //房间拥有者（既房主）
        public FightUnityClientPeer RoomMaster { get; set; }

        //房间设置
        public RoomSetting Setting { get; private set; }


        /// <summary>
        /// 初始化房间单位
        /// </summary>
        /// <param name="maseterCLientPeer">房间拥有者</param>
        /// <param name="roomID">房间ID</param>
        /// <param name="roomName">房间名称</param>
        /// <param name="password">房间密码</param>
        /// <param name="roomSetting">房间设置</param>
        public Room(FightUnityClientPeer maseterCLientPeer, RoomSetting roomSetting)
        {
            Setting = roomSetting;
            RoomMaster = maseterCLientPeer;
            JoinRoom(maseterCLientPeer,roomSetting.RoomPassword);
        }

        /// <summary>
        /// 退出房间
        /// </summary>
        /// <param name="peer">退出的用户</param>
        public void OnRoomExit(FightUnityClientPeer peer)
        {

            clientList.Remove(peer);

            Log.Info("On Room Exit: peerName: "
                                 + peer.GetPlayerDetails.PlayerName + "\n peer ID: "
                                 + peer.GetPlayerDetails.PlayerID);


            if (RoomMaster == peer && clientList.Count < 1)
                Dismiss();
            else if (RoomMaster == peer)
                RoomMaster = clientList[0];

            //当用户退出房间后加入大厅列表
            if (!FightServer.GetFightServer().fightUnityClientPeers.ContainsKey(peer.GetPlayerDetails.PlayerName))
            {
                FightServer.GetFightServer().fightUnityClientPeers.Add(peer.GetPlayerDetails.PlayerName, peer);
                Log.Info(peer.GetPlayerDetails.PlayerName + " has left the room to enter the hall. ");
            }
            else
            {
                Log.Info("On Room Exit: peerName: " + peer.GetPlayerDetails.PlayerName + " is existed");
            }
        }

        /// <summary>
        /// 释放房间
        /// </summary>
        public void Dismiss()
        {
            RoomMaster = null;
            clientList.Clear();
            clientList = null;
            FightServer.GetFightServer().roomDictionary.Remove(Setting.RoomId.ToString());
            Log.Info("Room: " + Setting.RoomId + " - " + Setting.RoomName + " is dismiss!");
            Log.Info("Room list count: " + FightServer.GetFightServer().roomDictionary.Count);
        }

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="peer">加入用户的peer</param>
        public void JoinRoom(FightUnityClientPeer peer,string password)
        {
            if (!int.Parse(password).Equals(int.Parse(password)))
            {
                Log.Info("Password not match");
                return;
            }
            if (!clientList.Contains(peer))
            {
                clientList.Add(peer);
                Log.Info("Player: " + peer.GetPlayerDetails.PlayerName + " join " + Setting.RoomName);
            }
            else if (RoomMaster != peer)
            {
                clientList.Add(peer);
                Log.Info("Master: " + peer.GetPlayerDetails.PlayerName + " create " + Setting.RoomName);
            }

            //当用户加入房间后自动从大厅列表内移除
            if (FightServer.GetFightServer().fightUnityClientPeers.ContainsKey(peer.GetPlayerDetails.PlayerName))
            {
                FightServer.GetFightServer().fightUnityClientPeers.Remove(peer.GetPlayerDetails.PlayerName);
            }


            //标记当前peer已经加入房间
            peer.IsJoinedRoom = true;

            //标记当前peer加入的房间
            peer.joinedRoom = this;
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

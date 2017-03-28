using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Logging;
using PHOTONSERVER_FIGHT.Units;

namespace PHOTONSERVER_FIGHT.ApplicationBaseClass
{
    /**
	*
	* 功 能： N/A
	* 类 名： Room	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/

    public class Room
    {
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public Roomdata roomdata;

        public Room(Clientpeer _masterclientpeer, Roomdata _roomdata)
        {
            roomdata = _roomdata;
            roomdata.roommaster = _masterclientpeer;
            Joinroom(_masterclientpeer, roomdata.Roompassword);
        }


        public void Joinroom(Clientpeer _clientpeer, string _roompassword)
        {

            if (String.Compare(_roompassword, roomdata.Roompassword, StringComparison.Ordinal) != 0)
            {
                //TODO:发送密码错误消息回馈至客户端
                return;
            }

            string clientname = _clientpeer.playerdata.playername;
            if (roomdata.clientpeers.Count < 0)
            {
                if (!roomdata.clientpeers.Contains(_clientpeer) || !_clientpeer.Equals(roomdata.roommaster))
                {
                    roomdata.clientpeers.Add(_clientpeer);
                    log.Info("client: " + clientname + " join " + roomdata.Roomname + " room");
                }
                else
                {
                    log.Info("client: " + clientname + " is already in the " + roomdata.Roomname + " room");
                }
            }
            else
            {
                if (roomdata.clientpeers.Contains(_clientpeer)) return;
                roomdata.clientpeers.Add(_clientpeer);
                log.Info("Master " + clientname + " join " + roomdata.Roomname);
            }

            if (!FIGHTserverapplication.Getfightserverapplication().clientpeers.ContainsKey(clientname)) return;
            FIGHTserverapplication.Getfightserverapplication().clientpeers.Remove(clientname);
            FIGHTserverapplication.Getfightserverapplication().clientpeers.Add(clientname, null);
            _clientpeer.isjoinedroom = true;
            _clientpeer.joinedroom = this;
        }


        public void Exitintheroom(Clientpeer _clientpeer)
        {
            if (!roomdata.clientpeers.Contains(_clientpeer)) return;
            roomdata.clientpeers.Remove(_clientpeer);
            _clientpeer.isjoinedroom = false;
            _clientpeer.joinedroom = null;

            if (roomdata.roommaster.Equals(_clientpeer) && roomdata.clientpeers.Count <= 1)
            {
                Dismiss();
            }
            else if (roomdata.roommaster.Equals(_clientpeer) && roomdata.clientpeers.Count > 1)
            {
                roomdata.roommaster = roomdata.clientpeers[0];
            }
            else
            {
                string playername = _clientpeer.playerdata.playername;
                if (FIGHTserverapplication.Getfightserverapplication().clientpeers.ContainsKey(playername))
                {
                    FIGHTserverapplication.Getfightserverapplication().clientpeers.Remove(playername);
                    FIGHTserverapplication.Getfightserverapplication().clientpeers.Add(playername,_clientpeer);
                    log.Info(playername+ " has left the "+roomdata.Roomname+ " to the lobby. ");
                }
            }
            log.Info("exit in the room:  " + _clientpeer.playerdata.playerid + "-" + _clientpeer.playerdata.playername);

        }

        public void Dismiss()
        {
            roomdata.roommaster = null;
            roomdata.clientpeers.Clear();
            roomdata.clientpeers = null;
            FIGHTserverapplication.Getfightserverapplication().rooms.Remove(roomdata.Roomid);


            log.Info("room: " + roomdata.Roomid + " - " + roomdata.Roomname + " iss dismissed");

            roomdata = null;
        }
    }
}

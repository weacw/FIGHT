using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightServer.Common;
using FightServer.Common.Tools;
using LitJson;
using Photon.SocketServer;
using PHOTONSERVER_FIGHT.ApplicationBaseClass;
using PHOTONSERVER_FIGHT.Units;

namespace PHOTONSERVER_FIGHT.Handlers
{
    /**
	*
	* 功 能： 接受客户端关于房间操作的请求(创建房间、加入房间、离开房间、获取房间列表)
	* 类 名： Roomooperationhandler	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Roomooperationhandler : Handlerbase
    {
        public override Operationcode Opcode => Operationcode.ROOMOP;





        public override void OnHandlerMessage(OperationRequest _request, OperationResponse _response, Clientpeer _peer,
            SendParameters _sendparameters)
        {
            Suboperationcode suboperationcode = ParameterTool.Getsuboperateioncode(_request.Parameters);
            ParameterTool.Addsuboperationcode(_response.Parameters, suboperationcode);
            switch (suboperationcode)
            {
                case Suboperationcode.CREATEROOM:
                    Createroom(_request, _response, _peer);
                    break;
                case Suboperationcode.JOINROOM:
                    Joinroom(_request, _response, _peer);
                    break;
                case Suboperationcode.GETROOM:
                    Getroom(_response);
                    break;
                case Suboperationcode.LEAVEROOM:
                    Leaveroom(_request, _response, _peer);
                    break;
            }
        }

        /// <summary>
        /// 离开房间操作
        /// </summary>
        /// <param name="_request">客户端发起的请求</param>
        /// <param name="_response">服务器回馈客户端的请求</param>
        /// <param name="_clientpeer">客户端</param>
        private void Leaveroom(OperationRequest _request, OperationResponse _response, Clientpeer _clientpeer)
        {
            //解析客户端发送的请求操作
            object roomobj;
            _request.Parameters.TryGetValue((byte)Parametercode.ROOMID, out roomobj);
            if (roomobj == null) return;
            Room roomitem = FIGHTserverapplication.Getfightserverapplication().rooms[int.Parse(roomobj.ToString())];
            roomitem.Exitintheroom(_clientpeer);

            //服务器端回馈客户端操作码-离开房间的操作码
            _response.ReturnCode = (byte)Returncode.LEFTROOM;

            //服务器端回馈客户端的数据
            _response.Parameters = new Dictionary<byte, object>();
            ParameterTool.AddParameter(_response.Parameters, Parametercode.PLAYERDATA, roomitem, true);

        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="_response">服务器回馈客户端的请求</param>
        private void Getroom(OperationResponse _response)
        {
            //服务器端回馈客户端的操作码-获取房间列表的操作码
            _response.ReturnCode = (byte)Returncode.GETROOMLIST;

            //服务器端回馈客户端的数据
            _response.Parameters = new Dictionary<byte, object>();
            List<Roomdata> roomlist = new List<Roomdata>();
            foreach (KeyValuePair<int, Room> room in FIGHTserverapplication.Getfightserverapplication().rooms)
            {
                roomlist.Add(room.Value.roomdata);
            }
            ParameterTool.AddParameter(_response.Parameters, Parametercode.ROOMCOUNT, FIGHTserverapplication.Getfightserverapplication().rooms.Count);
            ParameterTool.AddParameter(_response.Parameters, Parametercode.ROOMDATA, roomlist);
        }

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="_request">如上</param>
        /// <param name="response">如上</param>
        /// <param name="_clientpeer">如上</param>
        private void Joinroom(OperationRequest _request, OperationResponse response, Clientpeer _clientpeer)
        {
            //解析客户端发送的请求
            object roomidobj, roompsdobj;
            _request.Parameters.TryGetValue((byte)Parametercode.ROOMID, out roomidobj);
            _request.Parameters.TryGetValue((byte)Parametercode.ROOMPSD, out roompsdobj);

            if (roomidobj == null) return;
            int roomid = int.Parse(roomidobj.ToString());
            string roompsd = roompsdobj.ToString();
            if (!FIGHTserverapplication.Getfightserverapplication().rooms.ContainsKey(roomid)) return;
            Room roomitem = FIGHTserverapplication.Getfightserverapplication().rooms[roomid];
            roomitem.Joinroom(_clientpeer, roompsd);


            //服务器端回馈客户端的操作码-加入房间的操作码
            response.ReturnCode = (byte)Returncode.JOINEDROOM;

            //服务器端回馈客户端的数据
            response.Parameters = new Dictionary<byte, object>();
            ParameterTool.AddParameter(response.Parameters, Parametercode.ROOMDATA, roomitem.roomdata);
        }

        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="_request">如上</param>
        /// <param name="_response">如上</param>
        /// <param name="_peer">如上</param>
        private void Createroom(OperationRequest _request, OperationResponse _response, Clientpeer _peer)
        {
            object roomobject;
            _request.Parameters.TryGetValue((byte)Parametercode.ROOMPARMETERS, out roomobject);
            if (roomobject == null) return;
            Roomdata roomdata = JsonMapper.ToObject<Roomdata>(roomobject.ToString());

        }
    }
}

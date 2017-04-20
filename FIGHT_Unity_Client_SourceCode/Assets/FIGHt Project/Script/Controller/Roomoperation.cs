using System.Collections.Generic;
using ExitGames.Client.Photon;
using FightServer.Common;
using FightServer.Common.Tools;
using LitJson;

namespace WEACW
{
    using UnityEngine;
    using System.Collections;


    /*
	* 功 能： N/A
	* 类 名： Roomoperation	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Roomoperation : Controllerbase
    {
        /// <summary>
        /// 加入房间回调
        /// </summary>
        public Globaldelegate.Onjoinedroom onjoinedroom;

        /// <summary>
        /// 离开房间回调
        /// </summary>
        public Globaldelegate.Onleftroom onleftroom;

        /// <summary>
        /// 每当新玩家加入/离开房间的回调
        /// </summary>
        public Globaldelegate.Onupdateroom onupdateroom;

        /// <summary>
        /// 点击获取房间按钮
        /// </summary>
        public Globaldelegate.Ongetroom ongetroom;


        private List<Playerdata> roomplayerdatas;
        private List<Roomdata> roomdatas;

        public override Operationcode Opcode
        {
            get
            {
                return Operationcode.ROOMOP;
            }
        }

        public override void OnOperationresponse(OperationResponse _response)
        {
            Debug.Log(_response.ReturnCode);
            switch (_response.ReturnCode)
            {
                case (byte)Returncode.GETROOMLIST:
                    //TODO:获取房间后
                    Getroomlist(_response);
                    if (ongetroom != null) ongetroom.Invoke(roomdatas);
                    break;
                case (byte)Returncode.JOINEDROOM:
                    //TODO: 加入房间后
                    object o;
                    _response.Parameters.TryGetValue((byte)Parametercode.ROOMDATA, out o);
                    if (o != null)
                    {
                        roomplayerdatas = JsonMapper.ToObject<List<Playerdata>>(o.ToString());
                        Debug.Log(roomplayerdatas.Count);
                    }
                    if (onjoinedroom != null) onjoinedroom.Invoke(roomplayerdatas);

                    break;
                case (byte)Returncode.ROOMEXITED:
                    break;
                case (byte)Returncode.LEFTROOM:
                    if (onleftroom != null) onleftroom.Invoke();                    
                    break;
            }
        }

        private void Getroomlist(OperationResponse _response)
        {
            roomdatas =
             ParameterTool.GetParameter<List<Roomdata>>(_response.Parameters, Parametercode.ROOMPARMETERS);
            if (roomdatas == null || roomdatas.Count <= 0) return;
            Debug.Log(roomdatas.Count.ToString());

        }

        public void Getroom()
        {
            Dictionary<byte, object> parameter = new Dictionary<byte, object>();
            parameter.Add((byte)Parametercode.SUBOPERATIONCODE, Suboperationcode.GETROOM);
            Clientengine.Getclientengine.SendRequest((byte)Operationcode.ROOMOP, parameter);
        }

        public void Createroom()
        {
            Dictionary<byte, object> parameter = new Dictionary<byte, object>();
            parameter.Add((byte)Parametercode.SUBOPERATIONCODE, Suboperationcode.CREATEROOM);
            Roomdata roomdata = new Roomdata();
            roomdata.Roomid = roomdata.GetHashCode();
            roomdata.Roomname = "Test-1";
            roomdata.Roompassword = "0";
            roomdata.Numberofgames = 6;
            ParameterTool.AddParameter(parameter, Parametercode.ROOMPARMETERS, roomdata);
            Clientengine.Getclientengine.SendRequest((byte)Operationcode.ROOMOP, parameter);
        }

        public void Joinroom(string _id)
        {
            Dictionary<byte, object> parameter = new Dictionary<byte, object>();
            ParameterTool.AddParameter(parameter, Parametercode.SUBOPERATIONCODE, Suboperationcode.JOINROOM, false);
            ParameterTool.AddParameter(parameter, Parametercode.ROOMID, _id, false);
            ParameterTool.AddParameter(parameter, Parametercode.ROOMPSD, "0", false);
            Clientengine.Getclientengine.SendRequest((byte)Operationcode.ROOMOP, parameter);
        }
    }
}

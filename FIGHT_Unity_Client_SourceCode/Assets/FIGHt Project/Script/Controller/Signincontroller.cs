

using FightServer.Common.Tools;

namespace WEACW
{
    using UnityEngine;
    using System.Collections.Generic;
    using ExitGames.Client.Photon;
    using FightServer.Common;

    /*
	* 功 能： N/A
	* 类 名： Signincontroller	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Signincontroller : Controllerbase
    {
        public Globaldelegate.Signinfailed signinfailed;
        public Globaldelegate.Signinsuccess signinsuccess;

        public override Operationcode Opcode
        {
            get { return Operationcode.SIGNIN; }
        }

        public void Signin(string _charactername)
        {
            Dictionary<byte, object> parameter = new Dictionary<byte, object>();
            parameter.Add((byte)Parametercode.CHARACTERNAME, _charactername);
            Clientengine.Getclientengine.SendRequest((byte)Operationcode.SIGNIN, parameter);
        }

        public override void OnOperationresponse(OperationResponse _response)
        {
            string tipcontents = null;
            switch (_response.ReturnCode)
            {
                case (short) Returncode.CHARACTERNAMEISEXIST:
                    Debug.Log("角色名称已存在");
                    tipcontents = string.Format("{0}已存在您无法使用！请更换您的角色昵称", "提示：");
                    if (signinfailed != null) signinfailed.Invoke(tipcontents);
                    break;
                case (short) Returncode.CHARACTERCREATED:
                    Debug.Log("角色名称注册成功");
                    if (signinsuccess != null) signinsuccess.Invoke();
                    break;
            }
        }
    }
}

using System.Collections.Generic;
using ExitGames.Client.Photon;
using FightServer.Common;

namespace WEACW
{
    using UnityEngine;
    using System.Collections;


    /*
	* 功 能： N/A
	* 类 名： Signupcontroller	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Signupcontroller : Controllerbase
    {

        public override Operationcode Opcode { get {return Operationcode.SIGNOUT;} }


        public override void OnOperationresponse(OperationResponse _response)
        {
            object charactername;
            _response.Parameters.TryGetValue((byte) Parametercode.CHARACTERNAME, out charactername);
            switch (_response.ReturnCode)
            {
                case (short)Returncode.SIGNOUTCHARACTER:
                    Debug.Log(charactername.ToString());
                    break;
            }
        }

        public void Signout(string _charactername)
        {
            Dictionary<byte,object> parameter = new Dictionary<byte, object>();
            parameter.Add((byte)Parametercode.CHARACTERNAME,_charactername);
            Clientengine.Getclientengine.SendRequest((byte)Operationcode.SIGNOUT,parameter);
        }
    }
}

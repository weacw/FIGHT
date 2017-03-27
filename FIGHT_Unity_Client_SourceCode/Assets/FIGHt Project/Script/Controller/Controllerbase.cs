using ExitGames.Client.Photon;
using FightServer.Common;

namespace WEACW
{
    using UnityEngine;
    using System.Collections;


    /*
	* 功 能： N/A
	* 类 名： Controllerbase	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public abstract class Controllerbase : MonoBehaviour
    {
        public abstract OperationCode Opcode { get; }

        public virtual void Start()
        {
            Clientengine.Getclientengine.Registercontrller(Opcode, this);
        }
        public virtual void OnDestroy() { }
        public virtual void OnEvent(EventData _eventData) { }
        public abstract void OnOperationresponse(OperationResponse _response);
    }
}

using System.Collections.Generic;

namespace WEACW
{
	using UnityEngine;
	using System.Collections;

	
	/*
	* 功 能： N/A
	* 类 名： Globaldelegate	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Globaldelegate 
    {
        public delegate void Displayui(GameObject _go);

        public delegate void Signinfailed(string _character);

        public delegate void Signinsuccess();

        public delegate void Onjoinedroom(List<Playerdata> _playerdatas);

        public delegate void Onleftroom();

        public delegate void Onupdateroom();

        public delegate void Ongetroom(List<Roomdata> _roomdatas);

        public delegate void Onconnected();

        public delegate void Onconnecting();

        public delegate void Uilogicdelegate();
    }
}

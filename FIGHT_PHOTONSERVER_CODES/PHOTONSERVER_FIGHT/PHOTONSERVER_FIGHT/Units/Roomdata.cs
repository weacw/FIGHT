using System.Collections.Generic;
using PHOTONSERVER_FIGHT.ApplicationBaseClass;

namespace PHOTONSERVER_FIGHT.Units
{
    /**
	*
	* 功 能： N/A
	* 类 名： Roomdata	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Roomdata
    {
        public string Roomname { get; set; }
        public int Roomid { get; set; }
        public string Roompassword { get; set; }
        public int Numberofgames { get; set; }

        public List<Clientpeer> clientpeers = new List<Clientpeer>();
        public Clientpeer roommaster { get; set; }

    }
}

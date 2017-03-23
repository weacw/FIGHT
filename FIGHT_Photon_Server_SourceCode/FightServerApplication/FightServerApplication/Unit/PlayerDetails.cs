using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fight
{
    /**
	*
	* 功 能： N/A
	* 类 名： PlayerDetails	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    public class PlayerDetails
    {
        public int PlayerID { get;  set; }
        public string PlayerName { get;  set; }
        public List<Skill> skillSlots= new List<Skill>();
    }

    public class Skill
    {
        public int skillID;
        public SkillType skillType;
        public enum SkillType:int
        {
            Attack
        }
    }
}

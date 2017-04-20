namespace WEACW
{
    using UnityEngine;
    using System.Collections;


    /*
	* 功 能： N/A
	* 类 名： Uiglobalenum	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Uiglobalenum
    {
        public enum Animtype:int
        {
            POP,
            NORMAL
        };

        public enum Windowtype : int
        {
            NORMALWINDOW,
            POPWINDOW,
            FIXEDWINDOW
        };

        public enum Windowmask : int
        {
            NONE,
            DEFAULT,
            WITHMASK_NOTRANSPARENT,
            WITHMASK_TRANSPARENT
        };

    }
}

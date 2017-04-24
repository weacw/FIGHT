using UnityEngine.UI;

namespace WEACW
{
	using UnityEngine;
	using System.Collections;

	
	/*
	* 功 能： N/A
	* 类 名： Roomview	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Roomview : Fixedwindow
    {
        public Button close;

        public override void Buttoneventbind()
        {
            base.Buttoneventbind();
            close.onClick.AddListener(() =>
            {
                Uimanager.Getintance.homeview.Onwindowdisplay();
                Uimanager.Getintance.roomview.Onwindowhide();
            });
        }
    }
}

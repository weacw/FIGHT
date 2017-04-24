using UnityEngine.UI;

namespace WEACW
{
    using UnityEngine;
    using System.Collections;


    /*
	* 功 能： N/A
	* 类 名： Homeview	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Homeview : Fixedwindow
    {
        public Button singlebtn;
        public Button multipbtn;
        public Button aboutbtn;
        public Button videobtn;
        public Button shoppingbtn;


         

        public override void Buttoneventbind()
        {
            base.Buttoneventbind();
            singlebtn.onClick.AddListener(()=>
            {
                Uimanager.Getintance.tipsview.Settipstring("模式暂时未开放，将于下个版本开放！");
            });

            multipbtn.onClick.AddListener(() =>
            {
                Uimanager.Getintance.roomview.Onwindowdisplay();
                Uimanager.Getintance.homeview.Onwindowhide();
            });


        }
    }
}

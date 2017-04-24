using UnityEngine.UI;

namespace WEACW
{
	using UnityEngine;
	using System.Collections;

	
	/*
	* 功 能： N/A
	* 类 名： Signinview	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Signinview : Fixedwindow
    {
        public InputField usernameinputfield;
        public Button signinbtn;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Initwindow()
        {            
            base.Initwindow();
        }
    
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void Onwindowdisplay()
        {
            base.Onwindowdisplay();
        }

        public override void Onwindowhide()
        {
            base.Onwindowhide();
        }

        public string Getusername()
        {
            return usernameinputfield.text;
        }

        public override void Buttoneventbind()
        {
            base.Buttoneventbind();            
            signinbtn.onClick.AddListener(() =>
            {
                Uimanager.Getintance.signin.Signin(Getusername());
            });
        }
    }
}

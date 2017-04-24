namespace WEACW
{
	using UnityEngine;
	using System.Collections;

	
	/*
	* 功 能： N/A
	* 类 名： Uimanager	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Uimanager : Singleton<Uimanager>
    {
        public Connectserverview connectview;
        public Signinview signinview;
        public Tipsview tipsview;
        public Homeview homeview;
        public Roomview roomview;



        internal Clientengine client;
        internal Signincontroller signin;
        private void Awake()
        {
            Connect();
            Signin();
        }

        private void Connect()
        {
            client = Clientengine.Getclientengine;
            client.onconnectingevent += connectview.Onwindowdisplay;
            client.onconnectedevent += connectview.Onwindowhide;
            client.onconnectedevent += signinview.Onwindowdisplay;
        }

        private void Signin()
        {
            signin = FindObjectOfType<Signincontroller>();
            signin.signinfailed += tipsview.Settipstring;
            signin.signinsuccess += homeview.Onwindowdisplay;
            signin.signinsuccess += signinview.Onwindowhide;
        }


        void OnDisable()
        {
            if (signin != null)
            {
                signin.signinfailed -= tipsview.Settipstring;
                signin.signinsuccess -= homeview.Onwindowdisplay;
                signin.signinsuccess -= signinview.Onwindowhide;
            }

            if (client != null)
            {
                client.onconnectingevent -= connectview.Onwindowdisplay;
                client.onconnectedevent -= connectview.Onwindowhide;
                client.onconnectedevent -= signinview.Onwindowdisplay;
            }
        }
    }
}

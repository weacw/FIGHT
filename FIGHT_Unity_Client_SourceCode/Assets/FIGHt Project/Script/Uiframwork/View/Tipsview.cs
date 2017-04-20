using UnityEngine.UI;

namespace WEACW
{
    using UnityEngine;
    using System.Collections;


    /*
	* 功 能： N/A
	* 类 名： Tipsview	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Tipsview : Popwindow
    {
        public Text tips;

        private IEnumerator coroutine;


        protected override void Initwindow()
        {
            base.Initwindow();
        }

        public void Settipstring(string _tipmsg)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            tips.text = _tipmsg;
            Onwindowdisplay();
        }

        public override void Onwindowdisplay()
        {
            base.Onwindowdisplay();
            coroutine = Autohide();
            StartCoroutine(coroutine);
        }

        public override void Onwindowhide()
        {
            base.Onwindowhide();
            tips.text = null;
        }

        private IEnumerator Autohide()
        {
            yield return new WaitForSeconds(1);
            Onwindowhide();
        }
    }
}

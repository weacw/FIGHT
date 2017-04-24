using DG.Tweening;

namespace WEACW
{
    using UnityEngine;
    using System.Collections;


    /*
	* 功 能： N/A
	* 类 名： Fixedwindow	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Fixedwindow : Basewindow
    {


        protected override void Awake()
        {
            windowtitle = "Home";
            base.Awake();
        }

        protected override void Initwindow()
        {
            orginalscale = self.localScale;
            windowmask = Uiglobalenum.Windowmask.NONE;
            windowtype = Uiglobalenum.Windowtype.FIXEDWINDOW;
            animtype = Uiglobalenum.Animtype.NORMAL;
            base.Initwindow();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void Buttoneventbind()
        {
            
        }

        public override void Onwindowdisplay()
        {
            Display();
        }

        public override void Onwindowhide()
        {
            Hide();
        }

        protected override void Update()
        {
            return;
        }



        protected override void Display()
        {
            self.gameObject.SetActive(true);
            Tweener tner = self.DOScale(targetscale, duration);
            tner.SetEase(displaycurve);
            canvasgroup.DOFade(1, duration);
        }

        protected override void Hide()
        {
            canvasgroup.DOFade(0, duration);
            Tweener tner= self.DOScale(orginalscale, duration);
            tner.SetEase(displaycurve);
            tner.OnComplete(() =>
            {
                self.gameObject.SetActive(false);
            });
        }
    }
}

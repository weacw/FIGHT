using DG.Tweening;

namespace WEACW
{
    using UnityEngine;
    using System.Collections;


    /*
	* 功 能： N/A
	* 类 名： Popwindow	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Popwindow : Basewindow
    {
        [Header("Dynamic parameters for pop window")]
        public Vector3 targetpos;
        private Vector3 orginalpos;


        protected override void Awake()
        {
            //Get the necessary variables
            base.Awake();
        }

        protected override void Initwindow()
        {
            //Setup window title
            windowtitle = "Login";

            //Init the orginal pos
            orginalpos = self.anchoredPosition3D;
            orginalscale = self.localScale;

            //Setup window type parameter
            windowmask = Uiglobalenum.Windowmask.NONE;
            windowtype = Uiglobalenum.Windowtype.POPWINDOW;
            animtype = Uiglobalenum.Animtype.POP;

            //Init window 
            base.Initwindow();
        }

        public override void Onwindowdisplay()
        {
            Display();
        }

        public override void Onwindowhide()
        {
            Hide();
            Debug.Log("HIDE");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void Update()
        {
            return;
        }

        protected override void Display()
        {
            self.gameObject.SetActive(true);
            Tweener tner = self.DOAnchorPos3D(targetpos, duration);
            tner.SetEase(displaycurve);
            self.DOScale(targetscale, duration);
            canvasgroup.DOFade(1, duration);
        }

        protected override void Hide()
        {
            canvasgroup.DOFade(0, duration);
            Tweener tn = self.DOAnchorPos3D(orginalpos, duration);
            tn.SetEase(hidecurve);
            self.DOScale(orginalscale, duration);
            tn.OnComplete(() =>
            {
                self.gameObject.SetActive(false);
                tn.Kill(true);
            });
        }
    }
}

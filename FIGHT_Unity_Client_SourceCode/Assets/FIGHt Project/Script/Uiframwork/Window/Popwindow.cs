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

        protected Tweener tweener;
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
            if (tweener != null)
            {
                tweener.Kill(true);
                tweener = null;
            }

            self.gameObject.SetActive(true);
            tweener = self.DOAnchorPos3D(targetpos, duration);
            tweener.SetEase(displaycurve);
            self.DOScale(targetscale, duration);
            canvasgroup.DOFade(1, duration);
        }

        protected override void Hide()
        {
            if (tweener != null)
            {
                tweener.Kill(true);
                tweener = null;
            }


            canvasgroup.DOFade(0, duration);
            tweener = self.DOAnchorPos3D(orginalpos, duration);
            tweener.SetEase(hidecurve);
            self.DOScale(orginalscale, duration);
            tweener.OnComplete(() =>
            {
                self.gameObject.SetActive(false);
                tweener.Kill(true);
            });
        }
    }
}

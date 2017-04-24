using DG.Tweening;
using UnityEngine.UI;

namespace WEACW
{
    using UnityEngine;
    using System.Collections;


    /*
	* 功 能： N/A
	* 类 名： Basewindow	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/

    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Basewindow : MonoBehaviour
    {
        protected GameObject rootcanvas;
        protected RectTransform self;
        protected CanvasGroup canvasgroup;
        protected Vector3 orginalscale;


        internal string windowtitle = "Base window";
        protected bool isinited;


        [Header("Window type parameters")]
        public Uiglobalenum.Windowtype windowtype;
        public Uiglobalenum.Windowmask windowmask;
        public Uiglobalenum.Animtype animtype;


        public Text windowtitlecomp;
        public bool autohide;
        [Header("Window animation parameters")]
        public Vector3 targetscale;
        public AnimationCurve displaycurve;
        public AnimationCurve hidecurve;
        [Range(0, 1)]
        public float duration = 0.5f;


        public abstract void Buttoneventbind();


        public virtual void Onwindowdisplay()
        {
            if (!isinited) Awake();
        }

        public virtual void Onwindowhide() { }

        protected virtual void Awake()
        {
            Buttoneventbind();
            self = GetComponent<RectTransform>();
            rootcanvas = self.root.gameObject;
            canvasgroup = GetComponent<CanvasGroup>();
            Initwindow();
        }


        protected virtual void Initwindow()
        {
            Setupwindow();
            isinited = true;
            if (autohide)
                self.gameObject.SetActive(false);
        }

        protected virtual void OnDestroy()
        {
            rootcanvas = null;
            canvasgroup = null;
            windowtitle = null;
            self = null;
            windowtitlecomp = null;
            isinited = false;
        }


        protected abstract void Update();

        protected abstract void Display();
        protected abstract void Hide();

        private void Setupwindow()
        {
            if (windowtitlecomp == null) return; 
            windowtitlecomp.text = windowtitle;
        }
    }
}


using System;
using DG.Tweening;

namespace WEACW
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;


    /*
	* 功 能： N/A
	* 类 名： Cameracontroller	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/

    public class Cameracontroller : MonoBehaviour
    {
        public string triggertargetname = "Triggerbtn";
        public float moverate;
        public Ease camearease;
        public bool needselecttarget;
        public GameObject uitarget;
        public Transform original;
        public List<Transform> targetlist;


        private Transform self;
        private Transform target;
        private bool ismoving;


        public Globaldelegate.Displayui Displayuidelgate;

        private void Start()
        {
            self = this.transform;
        }


        public void Domovement()
        {
            if (!target || !self) return;
            Tweener tw = self.DOMove(target.position, moverate);
            tw.SetEase(camearease);
            tw.OnComplete(() =>
            {
                ismoving = false;
                if (Displayuidelgate != null)
                    Displayuidelgate.Invoke(uitarget);
            });
        }

        public void Retuanback()
        {
            if (!needselecttarget || ismoving) return;
            target = original;
            Domovement();
        }


        private void Update()
        {
            if (!needselecttarget || ismoving || !Input.GetMouseButtonDown(0)) return;
            GameObject tmp = null;
            try
            {
                tmp = Userinput.Getuserinput.Physcialinput(triggertargetname);
                foreach (Transform t in targetlist)
                    if (tmp.name.StartsWith(t.name))
                        target = t;
            }
            catch (Exception _exception)
            {
                Debug.Log(_exception.Message);
                return;
            }
            Domovement();
        }
    }
}

using System;

namespace WEACW
{
    using UnityEngine;


    /*
	* 功 能： N/A
	* 类 名： Userinput	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Userinput : MonoBehaviour
    {
        private RaycastHit raycasthit;
        private Ray ray;
        private Camera maincamera;
        public LayerMask layermask;



        private static Userinput instance;

        public static Userinput Getuserinput
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<Userinput>();
                return instance;
            }
        }


        private void Start()
        {
            maincamera = Camera.main;            
        }





        public GameObject Physcialinput(string _tag)
        {
            ray = maincamera.ScreenPointToRay(Getusermouse());
            if (!Physics.Raycast(ray, out raycasthit, 10000, layermask)) return null;
            return string.Compare(raycasthit.collider.tag, _tag, StringComparison.Ordinal)==0 ? raycasthit.collider.gameObject : null;
        }

        private Vector3 Getusermouse()
        {
            return Input.mousePosition;
        }
    }
}

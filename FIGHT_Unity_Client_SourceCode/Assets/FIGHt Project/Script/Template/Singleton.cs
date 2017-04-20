namespace WEACW
{
	using UnityEngine;
	using System.Collections;

	
	/*
	* 功 能： N/A
	* 类 名： Singleton	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Singleton<T> : MonoBehaviour where T :MonoBehaviour
    {
        private static T instance;
        private static object locker =new object();
        public static T Getintance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                        instance = FindObjectOfType<T>();
                    if (instance != null) return instance;
                    GameObject go = new GameObject();
                    instance = go.AddComponent<T>();
                    return instance;
                }
            }
        }
    }
}

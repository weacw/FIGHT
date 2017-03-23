/*
* 功 能： N/A
* 类 名： DynamicRandomMapCreator	
* Email:  paris3@163.com
* 作 者： NSWell-weacw 
* Copyright (c) weacw. All rights reserved.
*/

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace WEACW
{
    public class UIOperation : MonoBehaviour
    {
        public GameObject roomlistview;
        public GameObject roomcraeteview;
        public GameObject loginview;
        public GameObject action;

        public InputField characterName;
        public InputField roomName;
        public Dropdown dropdown;
        public InputField password;

        public GameObject roomItem;
        public Transform roomItemParent;


        private Dictionary<string, RoomSetting> roomDictionary = new Dictionary<string, RoomSetting>();

        public void Login()
        {
            PhotonClientEngine.GetPhotonClientEngine().LoginRequset(characterName.text);
        }

        public void CraeteRoom()
        {
            PhotonClientEngine.GetPhotonClientEngine().CraeteRoomRequest(roomName.text,password.text,dropdown.value);
        }

        public void GetRoomListCount()
        {
            PhotonClientEngine.GetPhotonClientEngine().GetRoomListRequest();
        }

        public void ExitRoom()
        {
            PhotonClientEngine.GetPhotonClientEngine().LeavingRoomRequset(roomDictionary[roomName.text].RoomId.ToString());
        }

        public void JoinRoom()
        {
            PhotonClientEngine.GetPhotonClientEngine().JoinRoomRequest(roomName.text,password.text);
        }



        public void Start()
        {
            PhotonClientEngine.GetPhotonClientEngine().getRoomlistHandler += CreateRoomItem;
        }

        private void CreateRoomItem(int amount, List<RoomSetting> roomsetList)
        {
            for (int i = 0; i < roomsetList.Count; i++)
            {
                if (roomDictionary.ContainsKey(roomsetList[i].RoomName)) continue;
                roomDictionary.Add(roomsetList[i].RoomName, roomsetList[i]);
                GameObject item = Instantiate(roomItem, roomItemParent) as GameObject;
                item.name = "Room - " + i;
                item.transform.localScale = Vector3.one;
                //TODO：获取item元素用于呈现服务器端反馈回来的数据
                //RoomSetting roomSetting = roomsetList[i];
                RoomItem itemScript = item.GetComponent<RoomItem>();
                itemScript.roomID.text = roomsetList[i].RoomId.ToString();
                itemScript.roomName.text = roomsetList[i].RoomName;
                itemScript.roomPeople.text = roomsetList[i].RoomPeople.ToString();
                itemScript.roomStatus.text = roomsetList[i].RoomPassword;
            }
        }






        public void Update()
        {
            if (PhotonClientEngine.GetPhotonClientEngine().IsConnected && !loginview.activeInHierarchy && !PhotonClientEngine.GetPhotonClientEngine().IsLogin)
            {
                loginview.SetActive(true);
            }
            if (PhotonClientEngine.GetPhotonClientEngine().IsLogin && !action.activeInHierarchy)
            {
                loginview.SetActive(false);
                action.SetActive(true);
            }
        }
    }
}


namespace WEACW
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using ExitGames.Client.Photon;
    using FightServer.Common;

    /*
	* 功 能： N/A
	* 类 名： Clientengine	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class Clientengine : MonoBehaviour, IPhotonPeerListener
    {
        private PhotonPeer clientpeer;
        private Dictionary<byte,Controllerbase> controllerbases = new Dictionary<byte, Controllerbase>();
        [SerializeField]private bool isconnect;

        private static Clientengine instance;

        internal event Globaldelegate.Onconnected onconnectedevent;
        internal event Globaldelegate.Onconnecting onconnectingevent;

        public string ip = "localhost";
        public string port = "4530";
        public string servername = "FIGHTserverapplication";
        public static Clientengine Getclientengine
        {
            get
            {
                if (instance == null) instance = FindObjectOfType<Clientengine>();
                return instance;
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            Application.runInBackground = true;

            clientpeer = new PhotonPeer(this, ConnectionProtocol.Tcp);
            Debug.Log(clientpeer.Connect(ip + ":" + port, servername));

            
            if(null!=onconnectingevent) onconnectingevent.Invoke();
        }

        private void Update()
        {
            if (null != clientpeer)
            {
                clientpeer.Service();
            }
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            if(null !=clientpeer)
                clientpeer.Disconnect();
        }

        private void OnApplicationquit(bool _status)
        {
            Debug.Log(_status);

            if (null != clientpeer)
                clientpeer.Disconnect();
        }


        public void Registercontrller(Operationcode _opcode, Controllerbase _controllerbase)
        {
            controllerbases.Add((byte)_opcode,_controllerbase);
        }

        public void Unregistercontroller(Operationcode _operationcode)
        {
            controllerbases.Remove((byte) _operationcode);
        }




        public void SendRequest(byte _operatcode, Dictionary<byte, object> _dictionary)
        {
            clientpeer.OpCustom(_operatcode, _dictionary, true);
        }





        public void DebugReturn(DebugLevel _level, string _message)
        {
            Debug.Log(_level + " : " + _message);
        }

        public void OnOperationResponse(OperationResponse _operationResponse)
        {
            Controllerbase controller;
            controllerbases.TryGetValue(_operationResponse.OperationCode, out controller);
            if(controller != null)
            {
                controller.OnOperationresponse(_operationResponse);
            }
            else
            {
                Debug.LogWarning("Receive a unknown response . OperationCode :" + _operationResponse.OperationCode);
            }
        }

        public void OnStatusChanged(StatusCode _statusCode)
        {
            switch (_statusCode)
            {
                case StatusCode.Connect:
                    isconnect = true;
                    if(onconnectedevent!=null)
                        onconnectedevent.Invoke();
                    break;
                case StatusCode.Disconnect:
                    break;
                case StatusCode.Exception:
                    break;
                case StatusCode.ExceptionOnConnect:
                    break;
                case StatusCode.SecurityExceptionOnConnect:
                    break;
                case StatusCode.QueueOutgoingReliableWarning:
                    break;
                case StatusCode.QueueOutgoingUnreliableWarning:
                    break;
                case StatusCode.SendError:
                    break;
                case StatusCode.QueueOutgoingAcksWarning:
                    break;
                case StatusCode.QueueIncomingReliableWarning:
                    break;
                case StatusCode.QueueIncomingUnreliableWarning:
                    break;
                case StatusCode.QueueSentWarning:
                    break;
                case StatusCode.InternalReceiveException:
                    break;
                case StatusCode.TimeoutDisconnect:
                    break;
                case StatusCode.DisconnectByServer:
                    break;
                case StatusCode.DisconnectByServerUserLimit:
                    break;
                case StatusCode.DisconnectByServerLogic:
                    break;
                case StatusCode.TcpRouterResponseOk:
                    break;
                case StatusCode.TcpRouterResponseNodeIdUnknown:
                    break;
                case StatusCode.TcpRouterResponseEndpointUnknown:
                    break;
                case StatusCode.TcpRouterResponseNodeNotReady:
                    break;
                case StatusCode.EncryptionEstablished:
                    break;
                case StatusCode.EncryptionFailedToEstablish:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("_statusCode", _statusCode, null);
            }
        }

        public void OnEvent(EventData _eventData)
        {
        }

    }
}

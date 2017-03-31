namespace PHOTONSERVER_FIGHT
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using ExitGames.Logging;
    using ExitGames.Logging.Log4Net;
    using log4net;
    using log4net.Config;
    using Photon.SocketServer;
    using ApplicationBaseClass;
    using Handlers;

    /*
	* 功 能： N/A
	* 类 名： FIGHTserverapplication	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw 
	* Copyright (c) weacw. All rights reserved.
	*/
    public class FIGHTserverapplication : ApplicationBase
    {
        #region 全局变量区域
        public Dictionary<byte, Handlerbase> handlers = new Dictionary<byte, Handlerbase>();
        public Dictionary<string, Clientpeer> clientpeers = new Dictionary<string, Clientpeer>();
        public Dictionary<int, Room> rooms = new Dictionary<int, Room>();

        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        private static FIGHTserverapplication instance { get; set; }
        #endregion

        #region  基于Photon接口制作的逻辑

        /// <summary>
        /// 构造函数 用于初始化单例
        /// </summary>
        public FIGHTserverapplication()
        {
            instance = this;
        }

        /// <summary>
        /// 单例，提供快速便捷的其他类中调用本类中方法
        /// </summary>
        /// <returns></returns>
        public static FIGHTserverapplication Getfightserverapplication()
        {
            return instance;
        }

        /// <summary>
        /// 注册所有响应客户端的功能模块
        /// </summary>
        private void Registerhandler()
        {
            Type[] types = Assembly.GetAssembly(typeof(Handlerbase)).GetTypes();
            foreach (Type type in types)
            {
                if (type.FullName.EndsWith("handler"))
                    Activator.CreateInstance(type);
            }
        }
        #endregion

        #region Photon 接口
        /// <summary>
        /// 每当新用户链接至服务器时调用并创建一个peer实例用于与客户端进行数据交换
        /// </summary>
        /// <param name="_initRequest"></param>
        /// <returns></returns>
        protected override PeerBase CreatePeer(InitRequest _initRequest)
        {
            log.Info("Client peer has been created!");
            Clientpeer peer = new Clientpeer(_initRequest.Protocol, _initRequest.PhotonPeer);
            return peer;
        }

        /// <summary>
        /// 服务器开启调用，用于设置服务器端的log输出
        /// </summary>
        protected override void Setup()
        {
            ExitGames.Logging.LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            GlobalContext.Properties["LogFileName"] = this.ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));
            string serverlog = string.Format("\n\ndata {0} \n-----------\n", System.DateTime.Now);
            log.Info(serverlog + "Application setup complete.");

            Registerhandler();
        }

        /// <summary>
        /// 服务器关闭时调用
        /// </summary>
        protected override void TearDown()
        {
            string serverlog = string.Format("\ndata{0} \n-----------\n", System.DateTime.Now);
            log.Info(serverlog + "fight server application tear down");
        }
        #endregion
    }
}

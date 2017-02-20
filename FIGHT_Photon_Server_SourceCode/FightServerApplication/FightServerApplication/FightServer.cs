using System.IO;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using Photon.SocketServer;
using LogManager = ExitGames.Logging.LogManager;

namespace Fight
{
    /**
	*
	* 功 能：Fight Server程序模块入口
	* 类 名： FightServer	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    public class FightServer : ApplicationBase
    {

        //Fight Server 后台 log
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();



        /// <summary>
        /// 客户端连接服务器时调用。
        /// 实例化 FightUnityClientPeer 并且返回推送给Photon Server 进行管理服务器与客户端的信息传输
        /// </summary>
        /// <param name="initRequest">初始化请求</param>
        /// <returns></returns>
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            Log.Info("Fight server is creating client peer");

            return new FightUnityClientPeer(initRequest.Protocol, initRequest.PhotonPeer);
        }

        /// <summary>
        /// Photon服务器启动后并且启动Fight Server时调用。
        /// 用于设置Fight Server的一些相关自定义设置
        /// </summary>
        protected override void Setup()
        {
            //Fight Server 后台 log输出设置
            GlobalContext.Properties["LogFileName"] = ApplicationName;
            GlobalContext.Properties["Photon:ApplicationLogPath"] =Path.Combine(ApplicationRootPath, "log");
            string path = Path.Combine(BinaryPath, "log4net.config");
            var file = new FileInfo(path);
            if (file.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(file);
            }
      
            Log.Info("Fight server is ready");

        }

        /// <summary>
        /// 服务器关闭/崩溃时调用
        /// 便于开发者查询服务器问题
        /// </summary>
        protected override void TearDown()
        {
            Log.Info("Fight server Tear down");

        }
    }
}

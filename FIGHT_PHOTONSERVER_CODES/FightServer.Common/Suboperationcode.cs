namespace FightServer.Common
{
    /**
	*
	* 功 能： N/A
	* 类 名： Suboperationcode	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/
    /// <summary>
    /// 子操作码
    /// </summary>
    public enum Suboperationcode:byte
    {
        /// <summary>
        /// 创建房间
        /// </summary>
        CREATEROOM,
        /// <summary>
        /// 加入房间
        /// </summary>
        JOINROOM,
        /// <summary>
        /// 获取房间
        /// </summary>
        GETROOM,
        /// <summary>
        /// 同步房间
        /// </summary>
        SYNCROOM,
        /// <summary>
        /// 离开房间
        /// </summary>
        LEAVEROOM
    }
}

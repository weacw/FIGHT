namespace FightServer.Common
{
    /**
    *
    * 功 能： N/A
    * 类 名： Returncode	
    * Email:  paris3@163.com
    * 作 者： NSWell-weacw
    * Copyright (c) weacw. All rights reserved.
    */
    /// <summary>
    /// 回馈码
    /// </summary>
    public enum Returncode : byte
    {
        /// <summary>
        /// 角色名称已存在
        /// </summary>
        CHARACTERNAMEISEXIST,
        /// <summary>
        /// 角色创建成功
        /// </summary>
        CHARACTERCREATED,
        /// <summary>
        /// 离开房间
        /// </summary>
        LEFTROOM,
        /// <summary>
        /// 获取房间列表
        /// </summary>
        GETROOMLIST,
        /// <summary>
        /// 加入房间
        /// </summary>
        JOINEDROOM
    }
}

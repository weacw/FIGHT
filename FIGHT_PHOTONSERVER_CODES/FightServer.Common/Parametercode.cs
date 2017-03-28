namespace FightServer.Common
{
    /**
    *
    * 功 能： N/A
    * 类 名： Parametercode	
    * Email:  paris3@163.com
    * 作 者： NSWell-weacw
    * Copyright (c) weacw. All rights reserved.
    */


    /// <summary>
    /// 参数标记码
    /// </summary>
    public enum Parametercode : byte
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        CHARACTERNAME,
        /// <summary>
        /// 子操作
        /// </summary>
        SUBOPERATIONCODE,
        /// <summary>
        /// 操作码
        /// </summary>
        OPERATIONCODE,
        /// <summary>
        /// 角色ID
        /// </summary>
        ROLEID,
        /// <summary>
        /// 房间参数
        /// </summary>
        ROOMPARMETERS,
        /// <summary>
        /// 房间ID
        /// </summary>
        ROOMID,
        /// <summary>
        /// 玩家信息
        /// </summary>
        PLAYERDATA,
        /// <summary>
        /// 房间个数
        /// </summary>
        ROOMCOUNT,
        /// <summary>
        /// 房间信息
        /// </summary>
        ROOMDATA,
        /// <summary>
        /// 房间密码
        /// </summary>
        ROOMPSD
    }
}

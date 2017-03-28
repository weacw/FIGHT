namespace FightServer.Common
{
    /// <summary>
    /// 操作码
    /// </summary>
    public enum Operationcode:byte
    {
        /// <summary>
        /// 登陆操作码
        /// </summary>
        SIGNIN,
        /// <summary>
        /// 登出操作码
        /// </summary>
        SIGNOUT,
        /// <summary>
        /// 房间操作码
        /// </summary>
        ROOMOP
    }
}

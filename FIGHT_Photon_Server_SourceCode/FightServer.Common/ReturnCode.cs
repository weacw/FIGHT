namespace FightServer.Common
{
    public enum ReturnCode : byte
    {
        Ok = 0,
        RequestNotImplemented,

        //SignIn
        InvalidParameters,
        NameIsExist,

        //Room
        CreateRoom,
        LeaveRoom,
        JoinRoom,
        GetRoomList,
        RoomExist,
        PsdError,
        SyncPlayInRoom

    };
}

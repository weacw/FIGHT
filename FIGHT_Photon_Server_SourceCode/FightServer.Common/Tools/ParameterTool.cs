using System.Collections.Generic;
namespace FightServer.Common.Tools
{
    /**
	*
	* 功 能： N/A
	* 类 名： ParameterTool	
	* Email:  paris3@163.com
	* 作 者： NSWell-weacw
	* Copyright (c) weacw. All rights reserved.
	*/

    public class ParameterTool
    {
        /// <summary>
        /// 取出Dictionary 数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <param name="parameterCode"></param>
        /// <param name="isObject"></param>
        /// <returns></returns>
        public static T GetParameter<T>(Dictionary<byte, object> parameters, ParameterCode parameterCode,
            bool isObject = false)
        {
            object o = null;
            parameters.TryGetValue((byte)parameterCode, out o);
            if (isObject == false)
                return (T)o;
            return (T)o;
        }

        /// <summary>
        /// 添加  dictionary 数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isObject"></param>
        public static void AddParameter<T>(Dictionary<byte, object> parameters, ParameterCode key, T value,
            bool isObject = true)
        {
            parameters.Add((byte)key, value);
        }

        /// <summary>
        /// 往parameter中加入子操作函数数据
        /// </summary>
        /// <param name="parameters">suboperation code 数据字典</param>
        /// <param name="subOperateionCode">当前sub operation code 类型</param>
        public static void AddSubOperationCode(Dictionary<byte, object> parameters, SubOperateionCode subOperateionCode)
        {
            AddParameter(parameters, ParameterCode.SubOperationCode, subOperateionCode, false);
        }

        public static SubOperateionCode GetSubOperateionCode(Dictionary<byte, object> parameters)
        {
            return GetParameter<SubOperateionCode>(parameters, ParameterCode.SubOperationCode, false);
        }

        public static void AddSubOperationCodeWithRoleID(Dictionary<byte, object> parameters, OperationCode opCode,
            int roleID)
        {
            if (parameters.ContainsKey((byte)ParameterCode.OperationCode))
                parameters.Remove((byte)ParameterCode.OperationCode);
            if (parameters.ContainsKey((byte)ParameterCode.RoleID))
                parameters.Remove((byte)ParameterCode.RoleID);

            parameters.Add((byte)ParameterCode.OperationCode, opCode);
            parameters.Add((byte)ParameterCode.RoleID, roleID);
        }
    }
}

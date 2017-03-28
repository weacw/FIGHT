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
        /// <param name="__parameters"></param>
        /// <param name="_parametercode"></param>
        /// <param name="_isobject"></param>
        /// <returns></returns>
        public static T GetParameter<T>(Dictionary<byte, object> _parameters, Parametercode _parametercode,
            bool _isobject = false)
        {
            object o = null;
            _parameters.TryGetValue((byte)_parametercode, out o);
            if (_isobject == false)
                return (T)o;
            return (T)o;
        }

        /// <summary>
        /// 添加  dictionary 数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_parameters"></param>
        /// <param name="_key"></param>
        /// <param name="_value"></param>
        /// <param name="_isobject"></param>
        public static void AddParameter<T>(Dictionary<byte, object> _parameters, Parametercode _key, T _value,
            bool _isobject = true)
        {
            _parameters.Add((byte)_key, _value);
        }

        /// <summary>
        /// 往parameter中加入子操作函数数据
        /// </summary>
        /// <param name="_parameters">suboperation code 数据字典</param>
        /// <param name="_suboperateioncode">当前sub operation code 类型</param>
        public static void Addsuboperationcode(Dictionary<byte, object> _parameters, Suboperationcode _suboperateioncode)
        {
            AddParameter(_parameters, Parametercode.SUBOPERATIONCODE, _suboperateioncode, false);
        }

        public static Suboperationcode Getsuboperateioncode(Dictionary<byte, object> _parameters)
        {
            return GetParameter<Suboperationcode>(_parameters, Parametercode.SUBOPERATIONCODE, false);
        }

        public static void AddSubOperationCodeWithRoleID(Dictionary<byte, object> _parameters, Operationcode _opcode,
            int _roleid)
        {
            if (_parameters.ContainsKey((byte)Parametercode.OPERATIONCODE))
                _parameters.Remove((byte)Parametercode.OPERATIONCODE);
            if (_parameters.ContainsKey((byte)Parametercode.ROLEID))
                _parameters.Remove((byte)Parametercode.ROLEID);

            _parameters.Add((byte)Parametercode.OPERATIONCODE, _opcode);
            _parameters.Add((byte)Parametercode.ROLEID, _roleid);
        }
    }
}

/*
BaseUIRegisterInfo
By: @sunjiahaoz, 2016-3-24

用于保存窗口注册信息
 * 信息包括窗口ID（唯一），比如用枚举进行管理
 * 其他信息比如prefab名称，用于UImanager的加载等
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace sunjiahaoz.UI
{
    // T 窗口ID
    // U 注册信息结构
    public class BaseUIRegisterInfo<T, U>
    {
        // 窗口的注册信息
        protected Dictionary<T, U> _dictFrameRegInfo = new Dictionary<T, U>();

        // 子类中实现该方法，进行将用到的frame注册
        public virtual void InitRegisterAll()
        {
            _dictFrameRegInfo.Clear();
            // 注册UI todo
        }

        // 获得注册信息
        public virtual U GetReginfo(T idFrame)
        {
            if (_dictFrameRegInfo.ContainsKey(idFrame))
            {
                return _dictFrameRegInfo[idFrame];
            }
            return default(U);
        }

        // 是否包含
        public virtual bool ContainReg(T idFrame)
        {
            if (_dictFrameRegInfo.ContainsKey(idFrame))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 注册UI，每个UI在使用之前都必须进行注册
        /// </summary>
        /// <param name="nFrameID">id</param>
        /// <param name="strPrefabName">对应的Prefab名称</param>
        protected virtual void RegisterUI(T idFrame, U regInfo)
        {            
            _dictFrameRegInfo[idFrame] = regInfo;
        }
    }
}

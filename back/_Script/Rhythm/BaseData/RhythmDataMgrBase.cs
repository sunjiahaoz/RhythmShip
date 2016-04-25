using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum RhythmPointType
//{
//    Normal,     // 普通节奏点
//    Long,          // 长音
//}
//public class RhythmPointBase
//{
//    public virtual RhythmPointType type
//    {
//        get { return RhythmPointType.Normal; }
//    }

//    public RhythmPointBase(float fTimePoint)
//    {
//        _fTimePoint = fTimePoint;
//    }
//    float _fTimePoint;

//    public float TimePoint
//    {
//        get { return _fTimePoint; }
//    }
//}
/// <summary>
/// 节奏点数据管理的基本框架
/// </summary>
public class RhythmDataMgrBase
{    
    // 清空，重置
    public virtual void Clear() { }
    // 添加一个节奏点
    public virtual void Add(float point) { }

    public virtual int Count() { return 0; }
    public virtual float GetPointByIndex(int nIndex, int nExtraData = 0) { return 0f; }

    // 生成用于保存到文件的内容
    public virtual string GenerateConfig()
    {
        return "";
    }
    // 通过传入的配置内容生成数据
    public virtual bool LoadByConfigData(string strConfig)
    {
        return false;
    }

    public virtual bool LoadByConfigData(string[] strValues)
    {
        return false;
    }
    // 测试输出Log
    public virtual void DebugLogOut() { }
}




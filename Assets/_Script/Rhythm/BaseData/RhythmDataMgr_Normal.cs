using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RhythmDataMgr_Normal : RhythmDataMgrBase{
#region _Params_
    // 用来保存节奏点的录制时间
    protected List<float> _lstRhythmPoint = new List<float>();
#endregion

#region _RhythmDataMgrBase_
    public override void Add(float fPoint)
    {
        _lstRhythmPoint.Add(fPoint);
    }
    public override float GetPointByIndex(int nIndex, int nExtraData = 0)
    {
        if (nIndex < 0
            || nIndex >= _lstRhythmPoint.Count)
        {
            return 0;
        }
        return _lstRhythmPoint[nIndex];
    }

    public override int Count()
    {
        return _lstRhythmPoint.Count;
    }
    public override void Clear()
    {
        _lstRhythmPoint.Clear();
    }
    public override void DebugLogOut()
    {
        base.DebugLogOut();
        for (int i = 0; i < _lstRhythmPoint.Count; ++i )
        {
            Debug.Log(i + ":" + _lstRhythmPoint[i]);
        }
    }
    public override string GenerateConfig()
    {  
        string strInfo = "";
        for (int i = 0; i < _lstRhythmPoint.Count; ++i )
        {
            strInfo += _lstRhythmPoint[i] + ((i == _lstRhythmPoint.Count - 1) ? "" : "\n");            
        }
        return strInfo;
    }

    public override bool LoadByConfigData(string strConfig)
    {
        base.LoadByConfigData(strConfig);
        Clear();
        string[] strSplit = strConfig.Split('\n');
        return LoadByConfigData(strSplit);
    }
    public override bool LoadByConfigData(string[] strValues)
    {
        try
        {
            float fValue = 0f;
            for (int i = 0; i < strValues.Length; ++i)
            {
                if (strValues[i].Length == 0)
                {
                    continue;
                }
                if (float.TryParse(strValues[i], out fValue))
                {
                    Add(fValue);
                }
                else
                {
                    Debug.LogWarning("<color=orange>[Warning]</color>---" + "时间数据解析错误：" + i + ":" + strValues[i]);
                }
            }
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("<color=red>[Error]</color>---" + "解析数据错误：" + strValues);
            return false;
        }
    }
#endregion
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RhythmDataMgr_Long : RhythmDataMgr_Normal
{
    List<float> _lstRhythm = new List<float>();

#region _Params_
    protected float _fStartTime = -1;
    protected float _fEndTime = -1;
#endregion
#region _RhythmDataMgrBase_   
    public override string GenerateConfig()
    {
        if (_lstRhythm.Count % 2 != 0)
        {
            Debug.LogError("<color=red>[Error]</color>---" + "数据必须是成对的，但现在的数据不是！！！");
            return "";
        }
        return base.GenerateConfig();        
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
        if (strValues.Length % 2 != 0)
        {
            Debug.LogError("<color=red>[Error]</color>---" + "加载的数据必须是成对的，但现在这个不是");
            return false;
        }

        return base.LoadByConfigData(strValues);
    }
#endregion
}

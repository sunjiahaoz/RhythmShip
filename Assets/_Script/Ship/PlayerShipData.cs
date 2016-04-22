using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class PlayerShipData
{
    // 普通射击技能
    int _nShootFirePointId = 1;
    public int EquipedShootFirePoint
    {
        get { return _nShootFirePointId; }
        set { _nShootFirePointId = value; }
    }

    // 装备的特殊技能名称
    int _nEquipedSkillId = 1;

    public int EquipedSkillId
    {
        get { return _nEquipedSkillId; }
        set { _nEquipedSkillId = value; }
    }

    public void Save()
    {
        TagLog.Log(LogIndex.Data, "保存PlayerShipData");
    }

    public void Load()
    {
        TagLog.Log(LogIndex.Data, "加载PlayerShipData");
    }
}

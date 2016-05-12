using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class PlayerEquipedFirePointMgr
{
    public void Load()
    {
        TagLog.Log(LogIndex.Data, "加载玩家firePoint数据");
    }

    //Dictionary<int, EquipData> _dictShootFirePoint = new Dictionary<int, EquipData>();
    private const string FirePointFolder = "PrefabFirePoint";
    public void CreateFirePoint(int nId, System.Action<BaseShootMotorFirePoint> actionAfterCreate)
    {
        if (!Equip_FirePointConfig.dic.ContainsKey(nId))
        {
            TagLog.LogError(LogIndex.Data, "找不到FirePoint的配置:" + nId);
        }
        ResourceManager.instance.AddRequest(Equip_FirePointConfig.dic[nId].prefabName, FirePointFolder, OnCreateFirePoint, actionAfterCreate);
    }

    void OnCreateFirePoint(UnityEngine.Object assert, System.Object arg)
    {
        System.Action<BaseShootMotorFirePoint> actionCreate = (System.Action<BaseShootMotorFirePoint>)arg;
        GameObject go = ObjectPoolController.Instantiate(assert as GameObject);
        BaseShootMotorFirePoint fp = go.GetComponent<BaseShootMotorFirePoint>();
        if (fp != null)
        {
            actionCreate(fp);
        }
        else
        {
            TagLog.LogError(LogIndex.Data, go + "没有FirePoint组件！！！");
        }
    }
}

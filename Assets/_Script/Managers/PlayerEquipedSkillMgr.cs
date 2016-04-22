using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class PlayerEquipedSkillMgr {
    public void Load()
    {
        TagLog.Log(LogIndex.Data, "加载Skill");
    }
    //Dictionary<int, EquipData> _dictSkill = new Dictionary<int, EquipData>();
    private const string SkillFolder = "PrefabSkill";

    public void CreateSkill(int nId, System.Action<PlayerShipBaseSkill> actionAfterCreate)
    {
        if (!Equip_SkillConfig.dic.ContainsKey(nId))
        {
            TagLog.LogError(LogIndex.Data, "找不到Skill的配置:" + nId);
        }
        ResourceManager.instance.AddRequest(Equip_SkillConfig.dic[nId].prefabName, SkillFolder, OnCreateSkill, actionAfterCreate);
    }

    void OnCreateSkill(UnityEngine.Object assert, System.Object arg)
    {
        System.Action<PlayerShipBaseSkill> actionCreate = (System.Action<PlayerShipBaseSkill>)arg;
        GameObject go = ObjectPoolController.Instantiate(assert as GameObject);
        PlayerShipBaseSkill fp = go.GetComponent<PlayerShipBaseSkill>();
        if (fp != null)
        {
            actionCreate(fp);
        }
        else
        {
            TagLog.LogError(LogIndex.Data, go + "没有PlayerShipBaseSkill组件！！！");
        }
    }
}

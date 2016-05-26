using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class Elem_SkillSelect : MonoBehaviour {
    public UIControl_List _lst;
    public Elem_SkillItem _prefabItem;

    public void Init()
    {
        _lst.ResetContent();
        foreach (var item in Equip_SkillConfig.dic)
        {
            Elem_SkillItem shootItem = _lst.CreateAndAddItem(_prefabItem) as Elem_SkillItem;
            shootItem.Init(item.Key);
        }
    }
}

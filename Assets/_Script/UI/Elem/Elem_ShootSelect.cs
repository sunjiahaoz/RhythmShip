using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class Elem_ShootSelect : MonoBehaviour {
    public UIControl_List _lst;
    public Elem_ShootItem _prefabItem;

    public void Init()
    {
        _lst.ResetContent();
        foreach (var item in Equip_FirePointConfig.dic)
        {
            Elem_ShootItem shootItem = _lst.CreateAndAddItem(_prefabItem) as Elem_ShootItem;
            shootItem.Init(item.Key);
        }
    }

    public void InitWithIds(params int[] ids)
    {
        _lst.ResetContent();
        for (int i = 0; i < ids.Length; ++i )
        {
            if (Equip_FirePointConfig.dic.ContainsKey(ids[i]))
            {
                Elem_ShootItem shootItem = _lst.CreateAndAddItem(_prefabItem) as Elem_ShootItem;
                shootItem.Init(ids[i]);
            }
        }
    }
}

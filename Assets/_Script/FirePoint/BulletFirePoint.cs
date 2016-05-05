using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class BulletFirePoint : BaseFirePoint {

    //public BaseFireThing _prefabBullet;    // 子弹prefab

    protected override void CreateObject(Vector3 createPos, System.Action<BaseFireThing> afterCreate = null)
    {
        base.CreateObject(createPos, (go) => 
        {
            BaseBullet bullet = go.GetComponent<BaseBullet>();
            if (bullet != null)
            {                
                if (afterCreate != null)
                {
                    afterCreate(go);
                }
            }
            else
            {
                TagLog.LogWarning(LogIndex.FirePoint, "子弹Prefab没有BaseBullet组件！！", this);
            }
        });
    }
}

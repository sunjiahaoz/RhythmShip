using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class EnemyCreator : FirePointRhythm {    
    protected override void CreateObject(Vector3 pos, System.Action<BaseFireThing> afterCreate = null)
    {
        base.CreateObject(pos, (thing) => 
        {
            BaseEnemyShip ship = thing.GetComponent<BaseEnemyShip>();
            if (ship == null)
            {
                //TagLog.LogError(LogIndex.FirePoint, thing.name + "中找不到BaseEnemyShip组件！");
            }
            else
            {                
                if (afterCreate != null)
                {
                    afterCreate(thing);
                }
            }
        });
    }
}

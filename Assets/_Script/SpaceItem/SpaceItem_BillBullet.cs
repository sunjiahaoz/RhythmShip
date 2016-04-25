/*
SpaceItem_BillBullet
By: @sunjiahaoz, 2016-4-25

魂斗罗bill，吃掉该道具后改变射击子弹为散弹
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class SpaceItem_BillBullet : BaseSpaceItem {
    public LayerMask _checkLayer;
    Enemy_Bill _ship = null;

    protected override void Awake()
    {
        base.Awake();
        // TEST CODE
        //OnThingCreate();
    }

    public override void OnThingCreate()
    {
        base.OnThingCreate();
        _ship = null;
    }

    protected override void OnSthTriggerEnter(GameObject go)
    {
        if (ToolsUseful.CheckLayerContainedGo(_checkLayer, go))
        {
            _ship = go.GetComponentInParent<Enemy_Bill>();
            if (_ship != null)
            {
                OnItemUsed();
            }            
        }
    }

    protected override void OnItemUsed()
    {
        // Bill改变射击子弹 todo
        TagLog.Log(LogIndex.SpaceItem, "ItemUsed !!!!!");

        _ship.SetBillShootType(BillBulletType.SanDan);
        base.OnItemUsed();        
    }
}

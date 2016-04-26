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
    public tk2dSprite _body;

    Enemy_Bill _ship = null;
    BillBulletType _curType = BillBulletType.RedBall;
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
        _curType = (BillBulletType)Random.Range(1, 4);
        SetBodyByType(_curType);
    }

    protected override void OnSthTriggerEnter(GameObject go)
    {
        if (ToolsUseful.CheckLayerContainedGo(_checkLayer, go))
        {
            _ship = go.GetComponentInParent<Enemy_Bill>();
            if (_ship != null)
            {
                OnItemUsed(go);
            }            
        }
    }

    protected override void OnItemUsed(GameObject goUse)
    {
        // Bill改变射击子弹 todo
        TagLog.Log(LogIndex.SpaceItem, "ItemUsed !!!!!");        
        
        _ship.SetBillShootType(_curType);
        base.OnItemUsed(goUse);        
    }

    void SetBodyByType(BillBulletType eType)
    {
        switch (eType)
        {
            case BillBulletType.Star:
                break;
            case BillBulletType.RedBall:
                _body.spriteId = _body.GetSpriteIdByName("M");
                break;
            case BillBulletType.SanDan:
                _body.spriteId = _body.GetSpriteIdByName("S");
                break;
            case BillBulletType.Rotate:
                _body.spriteId = _body.GetSpriteIdByName("F");
                break;
            default:
                break;
        }        
    }
}

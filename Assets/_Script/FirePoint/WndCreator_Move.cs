using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class WndCreator_Move : EnemyCreator {
    public ObjectAnim_Move _animMoveIn;
    public ObjectAnim_Move _animMoveOut;

    protected override void CreateObject(Vector3 pos, System.Action<BaseFireThing> afterCreate = null)
    {
        base.CreateObject(pos, (thing) => 
        {
            EnemyShip_BaseWnd wnd = thing.GetComponent<EnemyShip_BaseWnd>();            
            if (wnd != null)
            {
                wnd.transform.parent = transform;
                wnd.InitContent(WndConfigDataMgr.Instance.GetNextWndIndex(), _animMoveOut._fDur + _animMoveOut._fDelay);
                wnd._trLeftTop.SetPosition(pos);
                _animMoveIn._trTarget = wnd.transform;
                _animMoveIn.Run();
                wnd._actionWndDestroy = OnWndDestroy;
            }
            if (afterCreate != null)
            {
                afterCreate(thing);
            }
        });
    }

    void OnWndDestroy(EnemyShip_BaseWnd wnd)
    {
        _animMoveOut._trTarget = wnd.transform;
        wnd._actionWndDestroy = null;
        _animMoveOut.Run();
    }
}

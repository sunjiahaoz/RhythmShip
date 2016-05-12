using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class BaseShip : BaseFireThing {
    public List<EventDelegate> _lstOnShipCreateEvent;
    public List<EventDelegate> _lstOnShipDestroyEvent;
    [Header("pos自动设置")]
    public EffectParam _effectDestroy;
    public BaseLifeCom _lifeCom;

    protected virtual void Awake()
    {
        if (_lifeCom == null)
        {
            _lifeCom = GetComponentInChildren<BaseLifeCom>();
        }
        //生命值变化
        _lifeCom._event._eventOnAddValue += _event__eventOnAddValue;
        // 死亡
        _lifeCom._event._eventOnDead += _event__eventOnDead;
    }

    protected virtual void OnDestroy()
    {
        _lifeCom._event._eventOnAddValue -= _event__eventOnAddValue;
        _lifeCom._event._eventOnDead -= _event__eventOnDead;
    }
#region _BaseFireThing_
    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);
        // 生命值重置
        _lifeCom.ReSet(_lifeCom.MaxValue, _lifeCom.MaxValue);
        // 其他
        EventDelegate.Execute(_lstOnShipCreateEvent);
    }
    public override void OnThingDestroy()
    {
        base.OnThingDestroy();
        ShotDestroyEffectWhenThingDestroy();
        ObjectPoolController.Destroy(gameObject);
        // 其他
        EventDelegate.Execute(_lstOnShipDestroyEvent);
    }
#endregion   

    protected virtual void ShotDestroyEffectWhenThingDestroy()
    {
        if (_effectDestroy._strName.Length > 0)
        {
            //TagLog.Log(LogIndex.Ship, "OnShipDestroy播放特效");
            _effectDestroy._pos = transform.position;
            ShotEffect.Instance.Shot(_effectDestroy);
        }
    }

    protected virtual void _event__eventOnAddValue(int nOffValue)
    {
        //TagLog.Log(LogIndex.Ship, "OnAddValue:" + nOffValue);
    }
    
    protected virtual void _event__eventOnDead()
    {        
        //TagLog.Log(LogIndex.Ship, "OnDead!!!!!");
        OnThingDestroy();
    }
}

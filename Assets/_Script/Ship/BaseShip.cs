using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class BaseShip : BaseFireThing {    
    [Header("pos自动设置")]
    public EffectParam _effectDestroy;
    public BaseLifeCom _lifeCom;

    protected virtual void Awake()
    {
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

    // 创建时的处理
    public virtual void OnShipCreate()
    {
        // 生命值重置
        _lifeCom.ReSet(_lifeCom.MaxValue, _lifeCom.MaxValue);
    }
    // 被摧毁时的处理
    public virtual void OnShipDestroy()
    {
        if (_effectDestroy._strName.Length > 0)
        {
            //TagLog.Log(LogIndex.Ship, "OnShipDestroy播放特效");
            _effectDestroy._pos = transform.position;
            ShotEffect.Instance.Shot(_effectDestroy);
        }
        ObjectPoolController.Destroy(gameObject);
    }

    protected virtual void _event__eventOnAddValue(int nOffValue)
    {
        //TagLog.Log(LogIndex.Ship, "OnAddValue:" + nOffValue);
    }
    
    protected virtual void _event__eventOnDead()
    {        
        //TagLog.Log(LogIndex.Ship, "OnDead!!!!!");
        OnShipDestroy();
    }
}

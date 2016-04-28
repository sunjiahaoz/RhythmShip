/*
BaseSpaceItem
By: @sunjiahaoz, 2016-4-25

SpaceItem的基类
 * 主要是有个持续时间，该时间过后就自动销毁
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class BaseSpaceItem : BaseFireThing {
    public float _fLifeTime = 10f;  // 生命时间
    [Header("道具使用后的效果")]
    public EffectParam _itemUsedEffect;         // 道具使用后的效果
    [Header("生命值为0时被毁的效果")]
    public EffectParam _itemLifeDeadEffect;      // 道具被销毁的时候的效果，虽然使用后也可能销毁，但这个只在生命值为0时调用

    BaseLifeCom _lifeCom;
    ColliderTrigger[] _triggers;
    protected virtual void Awake()
    {
        // 设置Layer
        //ToolsUseful.SetGoLayer(gameObject, GloabalSet.LayerName_Item, true);

        _lifeCom = GetComponentInChildren<BaseLifeCom>();
        _lifeCom._event._eventOnDead += _event__eventOnDead;
        _triggers = GetComponentsInChildren<ColliderTrigger>();
        for (int i = 0; i < _triggers.Length; ++i )
        {
            _triggers[i]._actionTriggerEnter += OnSthTriggerEnter;
            _triggers[i]._actionTriggerExit += OnSthTriggerExit;
            _triggers[i]._actionTriggerStay += OnSthTriggerStay;
        }
    }

    protected virtual void OnDestroy()
    {
        _lifeCom._event._eventOnDead -= _event__eventOnDead;
        for (int i = 0; i < _triggers.Length; ++i)
        {
            _triggers[i]._actionTriggerEnter -= OnSthTriggerEnter;
            _triggers[i]._actionTriggerExit -= OnSthTriggerExit;
            _triggers[i]._actionTriggerStay -= OnSthTriggerStay;
        }
    }

    float _fCurLifeTime = 0;
    void Update()
    {
        _fCurLifeTime += Time.deltaTime;
        if (_fCurLifeTime >= _fLifeTime)
        {
            _event__eventOnDead();
        }
    }

    protected virtual void OnSthTriggerEnter(GameObject go)
    {
       
    }

    protected virtual void OnSthTriggerExit(GameObject go)
    {

    }
    protected virtual void OnSthTriggerStay(GameObject go)
    {

    }

    void _event__eventOnDead()
    {
        if (_itemLifeDeadEffect._strName.Length > 0)
        {
            _itemLifeDeadEffect._pos = transform.position;
            ShotEffect.Instance.Shot(_itemLifeDeadEffect);
        }
        OnThingDestroy();
    }


#region _BaseFireThing_
    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);
        _fCurLifeTime = 0;
        _lifeCom.ReSet(_lifeCom.MaxValue, _lifeCom.MaxValue);
    }

    public override void OnThingDestroy()
    {
        base.OnThingDestroy();
        PoolableObject.Destroy(gameObject);
    }
#endregion

    // 道具被使用时使用
    protected virtual void OnItemUsed(GameObject goUse)
    {
        if (_itemUsedEffect._strName.Length > 0)
        {
            _itemUsedEffect._pos = goUse.transform.position;
            ShotEffect.Instance.Shot(_itemUsedEffect);
        }
        // 默认使用一次之后就销毁
        OnThingDestroy();        
    }

}

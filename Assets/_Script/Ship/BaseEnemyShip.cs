using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class BaseEnemyShip : BaseShip{    
    [Header("碰撞其他东西的时候")]
    public LayerMask _colliderLayer;
    public bool _bAutoGetColliderTriggers = true;
    public ColliderTrigger[] _trigger;
    public int _nHurtValue = 3;

    protected override void Awake()
    {
        base.Awake();

        if (_bAutoGetColliderTriggers)
        {
            _trigger = GetComponentsInChildren<ColliderTrigger>();
            if (_trigger.Length == 0)
            {
                TagLog.LogWarning(LogIndex.Enemy, "没有找到任何ColliderTrigger");
            }
        }
        for (int i = 0; i < _trigger.Length; ++i )
        {
            _trigger[i]._actionTriggerEnter = OnShipTriggerEnter;
            _trigger[i]._actionTriggerStay = OnShipTriggerStay;
            _trigger[i]._actionTriggerExit = OnShipTriggerExit;
        }
        if (_colliderLayer.value == 0)
        {
            _colliderLayer.value = ToolsUseful.GetLayerValue(GloabalSet.LayerName_Player);
            TagLog.LogWarning(LogIndex.Enemy, "ColliderLayer需要设置，现在自动设置为Player");
        }
    }

    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);
    }

    // ColliderTrigger的开关
    public virtual void SwitchColliderTrigger(bool bOpen)
    {
        for (int i = 0; i < _trigger.Length; ++i )
        {
            _trigger[i].SetEnable(bOpen);
        }
    }

    void OnShipTriggerEnter(GameObject go)
    {
        if (ToolsUseful.CheckLayerContainedGo(_colliderLayer, go))
        {
            BaseLifeCom life = go.GetComponent<BaseLifeCom>();
            if (life != null)
            {
                life.AddValue(-_nHurtValue);
                _lifeCom.AddValue(-_nHurtValue);
            }
        }
    }

    void OnShipTriggerStay(GameObject go)
    {
        if (ToolsUseful.CheckLayerContainedGo(_colliderLayer, go))
        {
            BaseLifeCom life = go.GetComponent<BaseLifeCom>();
            if (life != null)
            {
                life.AddValue(-_nHurtValue);
                _lifeCom.AddValue(-_nHurtValue);
            }
        }
    }

    void OnShipTriggerExit(GameObject go)
    {
        
    }
}

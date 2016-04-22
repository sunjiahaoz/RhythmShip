/*
BaseRhythmEnemyShip
By: @sunjiahaoz, 2016-4-20

根据本音乐通用节奏点发生事件的基类
*/
using UnityEngine;
using System.Collections;

public class BaseRhythmEnemyShip : BaseEnemyShip {
    public BulletFirePoint _firepoint;
    protected override void Awake()
    {
        base.Awake();
    }

    //void Start()
    //{
    //    // TEST CODE
    //    OnShipCreate();
    //}

    public override void OnShipCreate()
    {
        base.OnShipCreate();
        GamingData.Instance.sceneConfig._event._eventOnRhythmNormal += OnTriggerRhythmNormal;
    }

    public override void OnShipDestroy()
    {
        base.OnShipDestroy();
        GamingData.Instance.sceneConfig._event._eventOnRhythmNormal -= OnTriggerRhythmNormal;
    }

    protected virtual void OnTriggerRhythmNormal()
    {        
        if (_firepoint != null)
        {
            _firepoint.Fire();
        }
    }
}

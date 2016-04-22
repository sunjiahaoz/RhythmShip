/*
EnemyShip_ToTarget
By: @sunjiahaoz, 2016-4-19

可以朝目标移动的
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz.SteerTrack;
using sunjiahaoz;

public class EnemyShip_ToTarget : BaseEnemyShip {
    // 目标tr
    Transform _trTarget = null;
    // 默认方向
    Vector3 _posDefault = new Vector3(0, -1000, 0);
    //如果此值为true，则只获得一次目标点进行移动
    // 否则就会一直获得目标点进行移动，目标点移动了，则飞行轨迹也跟着移动
    public bool _bOnce = true;
    // 更新间隔,once为false时有意义
    public float _fUpdateInterval = 1f;

    SteeringCurveTarget _steer = null;    
    protected override void Awake()
    {
        base.Awake();
        _steer = GetComponent<SteeringCurveTarget>();
        if (_steer == null)
        {
            TagLog.LogError(LogIndex.Ship, "找不到SteeringCurveTarget组件！！！");
        }
    }

    public override void OnShipCreate(EnemyCreator creator)
    {
        base.OnShipCreate(creator);
        InitTarget();
    }

    protected virtual void InitTarget()
    {
        // 默认是玩家飞机
        _trTarget = GamingData.Instance.gameBattleManager.playerShip.transform;
        if (_trTarget == null)
        {
            _steer.SetTarget(_posDefault);
        }
        else
        {
            _steer.SetTarget(_trTarget.position);
        }        
    }

    float _fCurIntefal = 0;
    void Update()
    {
        // 如果是只执行一次，那就不管了，在create的时候已经执行过了
        if (_bOnce)
        {
            return;
        }
        else
        {
            _fCurIntefal += Time.deltaTime;
            if (_fCurIntefal >= _fUpdateInterval)
            {
                _fCurIntefal = 0;
                if (_trTarget != null)
                {
                    _steer.SetTarget(_trTarget.position);
                }
                else
                {
                    _steer.SetTarget(_posDefault);
                }
            }            
        }
    }
}

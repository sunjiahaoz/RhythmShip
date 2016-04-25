/*
Enemy_Bill
By: @sunjiahaoz, 2016-4-25

魂斗罗Bill
 * 都一段路，射击一下，走一段路，射击一下
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz;
using sunjiahaoz.SteerTrack;

public class Enemy_Bill : BaseRhythmEnemyShip {

    SteeringDirLine _dirLine;
    FirePointBill _fpb = null;
    protected override void Awake()
    {
        base.Awake();
        _dirLine = GetComponent<SteeringDirLine>();
        _fpb = _firepoint as FirePointBill;        
    }

    public override void OnThingCreate()
    {
        base.OnThingCreate();
        SetBillShootType(BillBulletType.Star);
    }

    public float _fIntervalTime = 2f;

    float _fCurIntervalTime = 0;
    int _nCurState = 0; // 0 move, 1 shoot

    void Update()
    {
        _fCurIntervalTime += Time.deltaTime;
    }

    protected override void OnTriggerRhythmNormal()
    {   
        if (_fCurIntervalTime >= _fIntervalTime)
        {
            if (_nCurState == 0)
            {
                PlayShoot();
                _nCurState = 1;
            }
            else
            {
                PlayMove();
                _nCurState = 0;
            }
            _fCurIntervalTime = 0;
        }
    }

    // 移动
    void PlayMove()
    {
        _dirLine.enabled = true;
        // PlayMoveAnim todo
        TagLog.Log(LogIndex.Enemy, "Play Move Anim");
    }
    // 射击
    void PlayShoot()
    {
        _dirLine.enabled = false;
        // Play Shoot Anim todo
        TagLog.Log(LogIndex.Enemy, "Play Shoooooot Anim");
        _fpb.Fire();
    }

    public void SetBillShootType(BillBulletType nType)
    {
        FirePointBill fpb = (FirePointBill)_firepoint;
        fpb._nBulletType = nType;
    }
}

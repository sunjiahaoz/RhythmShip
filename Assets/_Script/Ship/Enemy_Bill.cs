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
using DG.Tweening;

public class Enemy_Bill : BaseRhythmEnemyShip {
    public tk2dSpriteAnimator _anim;
    SteeringDirLine _dirLine;    
    FirePointBill _fpb = null;

    Vector2 _srcDir = Vector2.zero;
    protected override void Awake()
    {
        base.Awake();
        _dirLine = GetComponent<SteeringDirLine>();
        _fpb = _firepoint as FirePointBill;        
    }

    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);        
        StopCoroutine("OnDead");
        SetBillShootType(BillBulletType.Star);
        _nCurState = 0;
        _fCurIntervalTime = 0;
        PlayMove();
    }

    public float _fMoveTimeDur = 2f;
    public float _fShootTimeDur = 1f;
    public float _fDeadJumpDistance = 100f;

    float _fCurIntervalTime = 0;
    int _nCurState = 0; // 0 move, 1 shoot, 2 dead    

    void Update()
    {
        _fCurIntervalTime += Time.deltaTime;        
    }

    protected override void OnTriggerRhythmNormal()
    {   
        // 在Move中
        if (_nCurState == 0)
        {
            if (_fCurIntervalTime >= _fMoveTimeDur)
            {
                PlayShoot();
                _nCurState = 1;
                _fCurIntervalTime = 0;
            }            
        }
        else if(_nCurState == 1)
        {
            if (_fCurIntervalTime >= _fShootTimeDur)
            {
                PlayMove();
                _nCurState = 0;
                _fCurIntervalTime = 0;
            }
        }       
    }

    // 移动
    void PlayMove()
    {
        _dirLine.Agent.enabled = true;
        // PlayMoveAnim todo
        TagLog.Log(LogIndex.Enemy, "Play Move Anim");
        _anim.Play("BillMove");
    }
    // 射击
    void PlayShoot()
    {
        _dirLine.Agent.enabled = false;
        // Play Shoot Anim todo
        TagLog.Log(LogIndex.Enemy, "Play Shoooooot Anim");
        _anim.Play("BillShoot");
        _fpb.Fire();
    }

    public override void OnThingDestroy()
    {   
        if (_nCurState == 2)
        {
            return;
        }
        StartCoroutine("OnDead");        
    }

    IEnumerator OnDead()
    {
        _dirLine.Agent.enabled = false;
        _nCurState = 2;
        yield return 0;
        //_anim.transform.localScale = Vector3.one * 3.5f;   // 死亡资源太小了
        _anim.Play("BillDead");
        transform.DOMove(transform.position - new Vector3(_fDeadJumpDistance, 0, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        //_anim.transform.localScale = Vector3.one;
        base.OnThingDestroy();
    }

    public void SetBillShootType(BillBulletType nType)
    {
        FirePointBill fpb = (FirePointBill)_firepoint;
        fpb._nBulletType = nType;
    }
}

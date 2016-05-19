using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

[RequireComponent(typeof(MusicRecorderBase))]
public class EnemyRhythmRecordShip : BaseEnemyShip {
    [Header("===EnemyRhythmRecordShip===")]
    public bool _bRecord = false;
    public List<EventDelegate> _lstRhythmPointEvent;    
    MusicRecorderBase _record;

    protected override void Awake()
    {
        base.Awake();
        _record = GetComponent<MusicRecorderBase>();
    }

    void Start()
    {
        if (_bRecord)
        {
            _record._event._eventOnPlayOneShot += OnPlayOneShot;
            _record._event._eventOnPlayEnd += OnPlayEnd;
            GamingData.Instance.gameBattleManager._event._eventOnState_Start += _eventOnGameStart;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_bRecord)
        {
            _record._event._eventOnPlayOneShot -= OnPlayOneShot;
            _record._event._eventOnPlayEnd -= OnPlayEnd;
            GamingData.Instance.gameBattleManager._event._eventOnState_Start -= _eventOnGameStart;
        }
    }

    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);
        if (!GamingData.Instance.gameBattleManager
            || !GamingData.Instance.gameBattleManager.CurAO.IsPlaying())
        {
            OnThingDestroy();
        }
        else
        {
            _record._event._eventOnPlayOneShot += OnPlayOneShot;
            _record._event._eventOnPlayEnd += OnPlayEnd;
            _record.PlayOrRecord(GamingData.Instance.gameBattleManager.CurAO);
        }
    }

    public override void OnThingDestroy()
    {
        base.OnThingDestroy();
        _record._event._eventOnPlayOneShot -= OnPlayOneShot;
        _record._event._eventOnPlayEnd -= OnPlayEnd;
    }

    protected virtual void OnPlayOneShot(int nIndex)
    {
        if (_lstRhythmPointEvent != null)
        {
            EventDelegate.Execute(_lstRhythmPointEvent);
        }
        //TagLog.Log(LogIndex.Enemy, "OnPlayOneShot:" + gameObject.name + " index:" + nIndex, this);
    }

    protected virtual void OnPlayEnd()
    {
        //TagLog.Log(LogIndex.Enemy, "PlayEnd:" + gameObject.name, this);
    }

    void _eventOnGameStart()
    {
        _record.PlayOrRecord(GamingData.Instance.gameBattleManager.CurAO);
    }
}

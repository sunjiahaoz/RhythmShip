using UnityEngine;
using System.Collections;
using sunjiahaoz;

[RequireComponent(typeof(MusicRecorderBase))]
public class FirePointRhythm : BaseFirePoint
{
    MusicRecorderBase _record;
    public MusicRecorderBase record
    {
        get { return _record; }
    }
    int _nLastShotIndex = 0;    // 最近一次接收到的节奏点索引
#region _Mono_

    protected override void Awake()
    {
        base.Awake();
        _record = GetComponent<MusicRecorderBase>();
        _record._event._eventOnPlayOneShot += OnPlayOneShot;
        _record._event._eventOnPlayEnd += OnPlayEnd;
    }

    protected virtual void Start()
    {
        GamingData.Instance.gameBattleManager._event._eventOnState_Start += _eventOnGameStart;
    }

    protected virtual void OnDestroy()
    {
        _record._event._eventOnPlayOneShot -= OnPlayOneShot;
        _record._event._eventOnPlayEnd -= OnPlayEnd;
        GamingData.Instance.gameBattleManager._event._eventOnState_Start -= _eventOnGameStart;
    }
#endregion
    // 需要实现这个
    protected override void CreateObject(Vector3 pos, System.Action<BaseFireThing> afterCreate = null)
    {
        base.CreateObject(pos, afterCreate);        
    }

    protected virtual void StartRhythm(AudioObject ao)
    {
        _record.PlayOrRecord(ao);
    }

#region _EventTrigger_
    // 游戏开始
    void _eventOnGameStart()
    {
        StartRhythm(GamingData.Instance.gameBattleManager.CurAO);
    }

    protected virtual void OnPlayOneShot(int nIndex)
    {
        _nLastShotIndex = nIndex;
        Fire();
    }

    protected virtual void OnPlayEnd()
    {
        TagLog.Log(LogIndex.FirePoint, "PlayEnd:" + gameObject.name, this);
    }
#endregion

#region _Tools_
    // 获得下一个节奏点的间隔时间
    public float GetNexInterval()
    {
        return GetNextRhythmPointTimeInterval(_nLastShotIndex);
    }
    // 获得某个节奏点的时间
    protected float GetRhythmPointTime(int nIndex)
    {
        return _record.RhythmMgr.GetPointByIndex(nIndex);
    }
    // 获得当前节奏点与下一个节奏点之间的时间差，如果没有下一个节奏点了，返回-1
    protected float GetNextRhythmPointTimeInterval(int nCurIndex)
    {
        float fInterval = GetRhythmPointTime(nCurIndex + 1) - GetRhythmPointTime(nCurIndex);
        if (fInterval < 0)
        {
            return -1;
        }
        return fInterval;
    }
#endregion
}

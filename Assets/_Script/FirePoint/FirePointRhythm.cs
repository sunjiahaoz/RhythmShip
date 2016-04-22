using UnityEngine;
using System.Collections;
using sunjiahaoz;

[RequireComponent(typeof(MusicRecorderBase))]
public class FirePointRhythm : BaseFirePoint
{
    MusicRecorderBase _record;
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
        Fire();
    }

    protected virtual void OnPlayEnd()
    {
        TagLog.Log(LogIndex.FirePoint, "PlayEnd:" + gameObject.name, this);
    }
#endregion
}

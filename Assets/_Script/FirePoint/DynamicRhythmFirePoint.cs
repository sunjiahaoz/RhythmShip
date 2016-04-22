using UnityEngine;
using System.Collections;
using sunjiahaoz;

[RequireComponent(typeof(MusicRecorderBase))]
public class DynamicRhythmFirePoint : BaseFirePoint {
    MusicRecorderBase _record;
    #region _Mono_

    protected override void Awake()
    {
        base.Awake();
        _record = GetComponent<MusicRecorderBase>();        
    }

    
    protected virtual void OnEnable()
    {        
        _record._event._eventOnPlayOneShot += OnPlayOneShot;
        _record._event._eventOnPlayEnd += OnPlayEnd;
        StartRhythm(GamingData.Instance.gameBattleManager.CurAO);
    }

    protected virtual void OnDisable()
    {        
        _record.Stop();
        _record._event._eventOnPlayOneShot -= OnPlayOneShot;
        _record._event._eventOnPlayEnd -= OnPlayEnd;
    }
    #endregion
    // 需要实现这个
    protected override void CreateObject(Vector3 pos, System.Action<BaseFireThing> afterCreate = null)
    {
        base.CreateObject(pos, afterCreate);
    }

    protected virtual void StartRhythm(AudioObject ao)
    {
        TagLog.Log(LogIndex.FirePoint, "开始 Rhythm!!");
        _record.PlayOrRecord(ao);
    }

    #region _EventTrigger_    

    protected virtual void OnPlayOneShot(int nIndex)
    {
        TagLog.Log(LogIndex.FirePoint, "Rhythm shot !!");
        Fire();
    }

    protected virtual void OnPlayEnd()
    {
        TagLog.Log(LogIndex.FirePoint, "PlayEnd:" + gameObject.name, this);
    }
    #endregion
}

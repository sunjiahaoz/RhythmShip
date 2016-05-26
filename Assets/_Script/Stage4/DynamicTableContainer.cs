using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

[RequireComponent(typeof(BeatDetect))]
public class DynamicTableContainer : MonoBehaviour {
    public bool _bShuffle = false;
    DynamicTable[] _tables = null;
    BeatDetect _comBeat;

    List<DynamicTable> _lstTables = new List<DynamicTable>();

    void Awake()
    {
        _comBeat = GetComponent<BeatDetect>();
        _tables = GetComponentsInChildren<DynamicTable>();
        _lstTables.Clear();
        _lstTables.AddRange(_tables);
        if (_bShuffle)
        {
            ToolsUseful.Shuffle(_lstTables);
        }        
    }

    void Start()
    {
        GamingData.Instance.gameBattleManager._event._eventOnState_Start += _event__eventOnState_Start;
    }

    void OnDestroy()
    {
        GamingData.Instance.gameBattleManager._event._eventOnState_Start -= _event__eventOnState_Start;
    }

    void _event__eventOnState_Start()
    {
        _comBeat.Init(GamingData.Instance.gameBattleManager.CurAO.primaryAudioSource);
        _comBeat._OnBeat.AddListener(OnBeatDetected);
    }

    void OnBeatDetected(int nIndex)
    {
        if (nIndex < 0
            || nIndex >= _lstTables.Count)
        {
            return;
        }
        _lstTables[nIndex].AddForce();
    }
}

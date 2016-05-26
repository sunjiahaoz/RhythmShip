using UnityEngine;
using System.Collections;

public class DynamicTableByBeatDetected : DynamicTable {

    public int _nListenIndex = 0;
    public BeatDetect _beatDetect;

    void Awake()
    {
        _beatDetect._OnBeat.AddListener(OnBeatDetected);
    }

    void OnDestroy()
    {
        _beatDetect._OnBeat.RemoveListener(OnBeatDetected);
    }

    public void OnBeatDetected(int nIndex)
    {
        if (nIndex == _nListenIndex)
        {
            AddForce();
        }
    }
}

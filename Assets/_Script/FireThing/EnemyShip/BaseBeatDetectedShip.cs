using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class BaseBeatDetectedShip : BaseEnemyShip {
    [Header("===========BaseBeatDetectedShip==============")]
    public BeatDetect _beatDetected;
    public int _nCheckBeatIndex = 1;
    [SerializeField]
    public UnityEvent _OnBeatProcess;

    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);
        if (_beatDetected == null)
        {
            _beatDetected = BallManager.Instance._beatDetected;
        }
        _beatDetected._OnBeat.AddListener(OnBeatDetected);
    }

    public override void OnThingDestroy()
    {
        base.OnThingDestroy();
        _beatDetected._OnBeat.RemoveListener(OnBeatDetected);
    }

    void OnBeatDetected(int nIndex)
    {
        if (nIndex == _nCheckBeatIndex)
        {
            OnBeatDetectedProcess();
        }
    }

    protected virtual void OnBeatDetectedProcess()
    {
        _OnBeatProcess.Invoke();
    }
}

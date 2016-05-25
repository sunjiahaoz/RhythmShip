using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class BallManager : SingletonMonoBehaviour<BallManager> {

    public PicContainer[] _picContainers;

#region _Event_
    [System.Serializable]
    public class OnBallDestroyEvent : UnityEvent<Enemy_BallBase> { }
    [System.Serializable]
    public class OnPicContainerFinish : UnityEvent { }
    [SerializeField]
    public OnBallDestroyEvent _OnBallDestroyEvent;
    [SerializeField]
    public OnPicContainerFinish _OnPicContainerFinish;
#endregion
    int _nCurPicContainerIndex = 0;

    void Start()
    {
        _nCurPicContainerIndex = -1;
        PlayNextContainers();
    }

    public void PlayNextContainers()
    {        
        _nCurPicContainerIndex++;
        if (_nCurPicContainerIndex >= _picContainers.Length)
        {
            _nCurPicContainerIndex = 0;
        }
        _picContainers[_nCurPicContainerIndex].gameObject.SetActive(true);
        _picContainers[_nCurPicContainerIndex].PicContainerStart();        
    }


    //// 球分裂后的速度力
    public float _fAddUpForceMin = 200;
    public float _fAddUpForceMax = 400;
    public float _fAddLeftOrRightForceMin = 150;
    public float _fAddRightOrRightForceMax = 300;

    public float GetRandomUpForce()
    {
        return Random.Range(_fAddUpForceMin, _fAddUpForceMax);
    }
    public float GetRandomLeftOrRightForce()
    {
        return Random.Range(_fAddLeftOrRightForceMin, _fAddRightOrRightForceMax);
    }
}

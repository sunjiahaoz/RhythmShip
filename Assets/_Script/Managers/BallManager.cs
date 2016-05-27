using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class BallManager : SingletonMonoBehaviour<BallManager> {

    public EnemyCreator[] _ballCreator;
    public BeatDetect _beatDetected;
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
        GamingData.Instance.gameBattleManager._event._eventOnState_End += _event__eventOnState_End;
    }


    public void PlayNextContainers()
    {
        if (_nCurPicContainerIndex >= 0
            && _nCurPicContainerIndex < _picContainers.Length)
        {
            _picContainers[_nCurPicContainerIndex].OnPicContainerEnd();
        }

        _nCurPicContainerIndex++;
        if (_nCurPicContainerIndex >= _picContainers.Length)
        {
            _nCurPicContainerIndex = 0;
        }
        _picContainers[_nCurPicContainerIndex].gameObject.SetActive(true);
        _picContainers[_nCurPicContainerIndex].PicContainerStart();        
    }

    void _event__eventOnState_End()
    {
        for (int i = 0; i < _ballCreator.Length; ++i )
        {
            _ballCreator[i].gameObject.SetActive(false);
        }
        //Enemy_BallBase[] balls = GameObject.FindObjectsOfType<Enemy_BallBase>();
        //for (int i = 0; i < balls.Length; ++i )
        //{
        //    if (balls[i].gameObject.activeInHierarchy)
        //    {
        //        balls[i].OnThingDestroy();
        //    }
        //}
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

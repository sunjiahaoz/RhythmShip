using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class BallManager : SingletonMonoBehaviour<BallManager> {
#region _Event_
    [System.Serializable]
    public class OnBallDestroyEvent : UnityEvent<Enemy_BallBase> { }
    [SerializeField]
    public OnBallDestroyEvent _OnBallDestroyEvent;

    //public class BallManagerEvent
    //{
    //    public delegate void OnBallDestroy(Enemy_BallBase ball);
    //    public event OnBallDestroy _eventOnBallDestroy;
    //    public void OnballDestroyEvent(Enemy_BallBase ball)
    //    {
    //        if (_eventOnBallDestroy != null)
    //        {
    //            _eventOnBallDestroy(ball);
    //        }
    //    }
    //}
    //public BallManagerEvent _event = new BallManagerEvent();
#endregion
    // 用于标记每个球，相当于球的唯一ID
    static int _nBallIndex = 0;
    public static int GetNextBallIndex()
    {
        return _nBallIndex++;
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

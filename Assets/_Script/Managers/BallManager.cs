using UnityEngine;
using System.Collections;

public class BallManager : SingletonMonoBehaviour<BallManager> {
    //public int MAX_LEVEL = 4;

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

    //public class BallManagerEvent
    //{
    //    // 有球被删掉了
    //    public delegate void DestroyBall(Enemy_BallBase ball);
    //    public event DestroyBall _eventDestroyBall;
    //    public void DestroyBallEvent(Enemy_BallBase ball)
    //    {
    //        if (_eventDestroyBall != null)
    //        {
    //            _eventDestroyBall(ball);
    //        }
    //    }
    //}
    //public BallManagerEvent _event;
    //ArrayList _lstBalls;

    //void Awake()
    //{
    //    _event = new BallManagerEvent();
    //    _lstBalls = new ArrayList();
    //}

    //void Start()
    //{
    //    //GlobalDataManager.It._event._eventRestart += new GlobalDataManager.GlobalEvent.Restart(_event__eventRestart);
    //    //GlobalDataManager.It._event._eventGameOver += new GlobalDataManager.GlobalEvent.GameOver(_event__eventGameOver);
    //}

    //public void AddBall(Enemy_BallBase ball)
    //{
    //    // 如果游戏已经结束了，就不再生成球了，直接删除
    //    if (_bIsGameOver)
    //    {
    //        Destroy(ball.gameObject);
    //        return;
    //    }

    //    if (_lstBalls.Contains(ball))
    //    {
    //        Debug.Log(ball.gameObject + " had Contained ");
    //        return;
    //    }
    //    for (int i = 0; i < _lstBalls.Count; ++i)
    //    {
    //        Physics.IgnoreCollision((_lstBalls[i] as Enemy_BallBase).GetComponent<Collider>(), ball.GetComponent<Collider>());
    //    }

    //    // 如果增加的是一个顶级小球，给这个球增加一个类型        
    //    if (ball.Level == MAX_LEVEL)
    //    {
    //        //DecorateBall(ball, SpriteSceneCreator.It.curScene.GetRandomNeedType());
    //    }

    //    _lstBalls.Add(ball);

    //    //Debug.Log("CurBallCount:" + _lstBalls.Count);

    //    // 
    //    ball.transform.parent = transform;
    //}

    //void DecorateBall(Enemy_BallBase ball, SmallBallType type)
    //{
    //    switch (type)
    //    {
    //        case SmallBallType.nextBigBall:
    //            DecorateBall_NextBigBall(ball);
    //            break;
    //        case SmallBallType.normal:
    //            DecorateBall_Normal(ball);
    //            break;
    //    }
    //}

    //// 装饰：一般类型
    //void DecorateBall_Normal(Enemy_BallBase ball)
    //{
    //    ball.ballType = SmallBallType.normal;
    //    ball.comSprite.color = Color.yellow;
    //}
    //// 装饰：生成下一个大球类型
    //void DecorateBall_NextBigBall(Enemy_BallBase ball)
    //{
    //    ball.ballType = SmallBallType.nextBigBall;
    //    // todo 其他效果
    //    ball.comSprite.color = Color.red;
    //}

    //public void DestroyBall(Enemy_BallBase ball)
    //{
    //    Debug.Log("DestroyBall: " + ball.gameObject);

    //    DestroyBallDecorate(ball);

    //    _lstBalls.Remove(ball);
    //    ball.GetComponent<Rigidbody>().isKinematic = true;
    //    DestroyBallAct(ball);
    //    Destroy(ball.gameObject);
    //}

    //public void DestroyBallDecorate(Enemy_BallBase ball)
    //{
    //    switch (ball.ballType)
    //    {
    //        case SmallBallType.nextBigBall:
    //            //BallCreator.It.CreateNextBall();
    //            break;
    //    }
    //}

    //void DestroyBallAct(Enemy_BallBase ball)
    //{
    //    _event.DestroyBallEvent(ball);
    //}

    //void _event__eventRestart()
    //{
    //    _bIsGameOver = false;
    //    // 删除所有球
    //    for (int i = 0; i < _lstBalls.Count; ++i)
    //    {
    //        Destroy((_lstBalls[i] as Enemy_BallBase).gameObject);
    //    }
    //    _lstBalls.Clear();

    //    //BallCreator.It.CreateNextBall();

    //    GenerateForce();
    //}

    //// 游戏结束
    //public Transform _PrefabDestroyBallEffect;
    //bool _bIsGameOver = false;
    //void _event__eventGameOver()
    //{
    //    // todo 可能有点特效什么的？
    //    // 删除所有球
    //    for (int i = 0; i < _lstBalls.Count; i++)
    //    {
    //        Transform effect = Instantiate(_PrefabDestroyBallEffect, (_lstBalls[i] as Enemy_BallBase).gameObject.transform.position, Quaternion.identity) as Transform;
    //        effect.Rotate(90, 0, 0);
    //        Destroy((_lstBalls[i] as Enemy_BallBase).gameObject);
    //    }
    //    _lstBalls.Clear();

    //    _bIsGameOver = true;
    //}
}

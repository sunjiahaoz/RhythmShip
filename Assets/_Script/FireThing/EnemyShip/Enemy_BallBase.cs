using UnityEngine;
using System.Collections;

public class Enemy_BallBase : BaseEnemyShip {
    public bool _bSmallest = false;

    com_AddForce _comAddForce;

    private int _nBallIndex = -1;
    public int BallIndex
    {
        get { return _nBallIndex; }
    }
    protected override void Awake()
    {
        base.Awake();
        _comAddForce = GetComponent<com_AddForce>();
    }
    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);
        _nBallIndex = BallManager.GetNextBallIndex();

        _comAddForce.AddForceByData(com_AddForce.AddDir.Up, BallManager.Instance.GetRandomUpForce());
        _comAddForce.AddForceByData(Random.Range(0, 10) > 5 ? com_AddForce.AddDir.Left : com_AddForce.AddDir.Right, BallManager.Instance.GetRandomLeftOrRightForce());
    }

    public override void OnThingDestroy()
    {
        BallManager.Instance._OnBallDestroyEvent.Invoke(this);        
        base.OnThingDestroy();        
    }
}

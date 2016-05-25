using UnityEngine;
using System.Collections;

public class Enemy_BallBase : BaseEnemyShip {
    public bool _bSmallest = false;

    com_AddForce _comAddForce;
    protected override void Awake()
    {
        base.Awake();
        _comAddForce = GetComponent<com_AddForce>();
    }
    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);        

        _comAddForce.AddForceByData(com_AddForce.AddDir.Up, BallManager.Instance.GetRandomUpForce());
        _comAddForce.AddForceByData(Random.Range(0, 10) > 5 ? com_AddForce.AddDir.Left : com_AddForce.AddDir.Right, BallManager.Instance.GetRandomLeftOrRightForce());
    }

    public override void OnThingDestroy()
    {
        BallManager.Instance._OnBallDestroyEvent.Invoke(this);
        base.OnThingDestroy();        
    }
}

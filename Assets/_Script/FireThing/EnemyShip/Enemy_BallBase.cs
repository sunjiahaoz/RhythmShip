using UnityEngine;
using System.Collections;

public enum SmallBallType
{
    normal,                         // 一般的类型，该球消失后会生成场景精灵
    nextBigBall,                // 该球消失后会触发生成下一个大球，只能是最高等级的小球有这个type
    spriteToObstcal,        // 该球消失后会生成障碍
}

public class Enemy_BallBase : BaseEnemyShip {
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
    //public Enemy_BallBase _prefabCell;
    //tk2dSprite _sprite;
    //public tk2dSprite comSprite
    //{
    //    get
    //    {
    //        if (_sprite == null)
    //        {
    //            _sprite = GetComponent<tk2dSprite>();
    //        }
    //        return _sprite;
    //    }
    //}


    //[SerializeField]
    //int _nLevel = 1;    // 等级，即大小什么的，1最大，2次之，MAX_LEVEL最小，MAX_LEVEL不可再分
    //public int Level
    //{
    //    set { _nLevel = value; }
    //    get { return _nLevel; }
    //}

    //SmallBallType _type = SmallBallType.normal;
    //public SmallBallType ballType
    //{
    //    get { return _type; }
    //    set { _type = value; }
    //}

    //void SplitCell()
    //{
    //    if (_nLevel == BallManager.Instance.MAX_LEVEL)
    //    {
    //        //Debug.Log("Three All !!!!!!!!!!!!!!!!");
    //        BallManager.Instance.DestroyBall(this);
    //        return;
    //    }

    //    Enemy_BallBase leftBall = ObjectPoolController.Instantiate(_prefabCell.gameObject, transform.position, Quaternion.identity).GetComponent<Enemy_BallBase>();
    //    Enemy_BallBase rightBall = ObjectPoolController.Instantiate(_prefabCell.gameObject, transform.position, Quaternion.identity).GetComponent<Enemy_BallBase>();
    //    leftBall.Level = _nLevel + 1;
    //    rightBall.Level = _nLevel + 1;

    //    // 分裂后大小缩小
    //    leftBall.transform.localScale = transform.localScale * 0.7f;
    //    rightBall.transform.localScale = transform.localScale * 0.7f;

    //    // 添加到管理器处理
    //    BallManager.Instance.AddBall(leftBall);
    //    BallManager.Instance.AddBall(rightBall);

    //    // 增加力
    //    com_AddForce comLeft = leftBall.gameObject.AddComponent<com_AddForce>();
    //    comLeft.AddForceByData(com_AddForce.AddDir.Up, BallManager.Instance.GetRandomUpForce());
    //    comLeft.AddForceByData(com_AddForce.AddDir.Left, BallManager.Instance.GetRandomLeftOrRightForce());
    //    com_AddForce comRight = rightBall.gameObject.AddComponent<com_AddForce>();
    //    comRight.AddForceByData(com_AddForce.AddDir.Up, BallManager.Instance.GetRandomUpForce());
    //    comRight.AddForceByData(com_AddForce.AddDir.Right, BallManager.Instance.GetRandomLeftOrRightForce());

    //    // 删除自身
    //    BallManager.Instance.DestroyBall(this);
    //}
}

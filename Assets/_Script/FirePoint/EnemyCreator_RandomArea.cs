using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class EnemyCreator_RandomArea : EnemyCreator {
    public Transform _trLeftPos;
    public Transform _trRightBottom;

    // 是否在调用GetDir时使用固定的dir，即基本的Dir计算方式
    // 如果为false就是通过随机的点进行方向计算
    public bool _bFixDir = false;

    Vector3 _posRand = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();        
    }

    public override void Fire()
    {
        _posRand = GenerateRandomPos();
        base.Fire();
    }

    public override Vector3 GetDir()
    {
        if (!_bFixDir)
        {
            return base.GetDir();
        }
        else
        {
            return (base.GetCreatePos() - _trFirePointBodyPos.position).normalized;        
        }
    }

    protected override Vector3 GetCreatePos()
    {
        return _posRand;
    }

    Vector3 GenerateRandomPos()
    {
        Vector3 pos = Vector3.zero;
        if (_trLeftPos != null
            && _trRightBottom != null)
        {
            pos.x = Random.Range(_trLeftPos.position.x, _trRightBottom.position.x);
            pos.y = Random.Range(_trLeftPos.position.y, _trRightBottom.position.y);
        }
        else
        {
            TagLog.LogWarning(LogIndex.FirePoint, "RandomArea Creator 没有LT或BR:" + gameObject.name);            
            pos = _trCreatePos.position;
        }        
        return pos;
    }    
}

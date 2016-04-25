using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class EnemyCreator_RandomArea : EnemyCreator {
    public Transform _trLeftPos;
    public Transform _trRightBottom;

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

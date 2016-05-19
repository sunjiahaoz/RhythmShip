/*
RotateToTarget
By: @sunjiahaoz, 2016-4-26

所挂接的对象朝向target,只改变z轴
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class RotateToTarget : MonoBehaviour {
    public Transform _target;               // 如果不为空，就用这个
    public FindLogicType _targetType = FindLogicType.None;   // 如果_target为空，就用这个
    public float _fOffAngle = 180;

    Vector3 _targetPos = Vector3.zero;
    void Awake()
    {
        if (_target == null)
        {
            SetTarget(_targetType);
        }
    }

    public void SetTarget(Vector3 pos)
    {
        // 如果直接设置位置，则target为null
        _target = null;
        _targetPos = pos;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetTarget(FindLogicType eType)
    {
        GameObject[] go = TargetFindMgr.Instance.GetFindLogic(eType).Find();
        if (go != null
            && go.Length > 0)
        {
            _target = go[0].transform;
        }
    }

    Vector3 v3 = Vector3.one;
    void Update()
    {
        if (_target != null)
        {
            _targetPos = _target.transform.position;
        }
        v3 = (_targetPos - transform.position).normalized;        
        if (v3.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, _fOffAngle + Mathf.Acos(v3.x) * 180 / Mathf.PI);            
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -_fOffAngle - Mathf.Acos(v3.x) * 180 / Mathf.PI);
        }
    }
}

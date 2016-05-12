using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;
using sunjiahaoz.SteerTrack;
using DG.Tweening;

public class FirePointCircle : FirePointRhythm {
    public Transform _trFirePointAnim;
    public float _fFirePonintAnimShowAanimTime = 0.3f;

    public int _nCount = 0;
    // 如果为true,则nCOunt个发射完后会再从头开始，否则就执行结束动画
    public bool _bLoopFire = false; 
    public bool _bDebugDraw = false;
    public float _fRadius = 10;

    public float _fToTargetPosDur = 0.5f;

    Vector3[] _v3Pos = null;
    protected override void Awake()
    {
        base.Awake();
        _trFirePointAnim.transform.localScale = Vector3.zero;
        _v3Pos = UtilityTool.RegularPolygonPosPoint(transform.position, _fRadius, _nCount);
    }

    protected override void CreateObject(Vector3 pos, System.Action<BaseFireThing> afterCreate = null)
    {
        base.CreateObject(pos, (thing) => 
        {
            EnemyText_Ha ha = thing.GetComponent<EnemyText_Ha>();
            if (ha == null)
            {
                return;
            }
            ha.SetTargetPos(GetTargetPos(), _fToTargetPosDur);
        });
    }

    public override void Fire()
    {
        if ((_nCurIndex - 1) >= _v3Pos.Length)
        {            
            return;
        }

        // 如果是第一个fire, 则只将展示动画放出来
        if (_nCurIndex == 0)
        {
            ShowFirePointAnim();
            _nCurIndex++;
            return;
        }
        _shootEffect._rotate = transform.localEulerAngles;
        base.Fire();
    }

    void ShowFirePointAnim()
    {
        _trFirePointAnim.DOScale(Vector3.one, _fFirePonintAnimShowAanimTime);
    }

    void HideFirePointAnim()
    {
        _trFirePointAnim.DOScale(Vector3.zero, _fFirePonintAnimShowAanimTime);
    }

    int _nCurIndex = 0;
    Vector3 GetTargetPos()
    {
        if (_v3Pos == null
        || _v3Pos.Length == 0)
        {
            return base.GetCreatePos();
        }
        // -1 是因为0位置操作的是展示动画
        Vector3 res = _v3Pos[_nCurIndex - 1];
        _nCurIndex++;
        if ((_nCurIndex - 1) >= _v3Pos.Length)
        {
            // 如果循环发射，则继续发射
            if (_bLoopFire)
            {
                _nCurIndex = 1;
            }
                // 否则就执行隐藏动画
            else
            {
                HideFirePointAnim();
            }            
        }
        return res;
    }

    void OnDrawGizmos()
    {
        if (_bDebugDraw
            && _v3Pos != null
            && _v3Pos.Length > 0)
        {
            for (int i = 0; i < _v3Pos.Length; ++i)
            {
                Gizmos.DrawWireSphere(_v3Pos[i], 40);
            }
        }
    }
}

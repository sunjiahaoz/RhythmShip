﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;
using Dest.Math;
using DG.Tweening;

public class Enemy_WhiteBall : FirePointRhythm {

    public Transform _trBody;
    public EffectParam _ScatterEffect;
    public Elem_SmallWhiteBall _PrefabSmallBall;
    public float _fScatterAreaRadius = 1f;   // 分散范围半径
    public float _fSmallballStartInhaleDest = 100;  // 快速吸小球时的距离
    public float _fSmallballCombineDest = 30;       // 与小球融合时的距离
    public int _nPtCount = 10;
    public float _fSmallBallScatterDur = 0.1f;
    public float _fSrcScale = 5;
    
    List<Vector3> _lstScatterPoints = new List<Vector3>();
    List<Elem_SmallWhiteBall> _lstSmallBalls = new List<Elem_SmallWhiteBall>();
    List<Elem_SmallWhiteBall> _lstWillDestroy = new List<Elem_SmallWhiteBall>();
    void GenerateScatterPoint(Vector3 posCenter, float fScatterRadiusMin, float fScatterRadiusMax, int nCount)
    {
        _lstScatterPoints.Clear();
        _lstScatterPoints.AddRange(UtilityTool.GenerateScatterPoint(posCenter, fScatterRadiusMin, fScatterRadiusMax, nCount));
    }
    IEnumerator OnCreateSmallBall()
    {
        _trBody.localScale = Vector3.one;
        _ScatterEffect._pos = transform.position;
        ShotEffect.Instance.Shot(_ScatterEffect);
        //_lstSmallBalls.Clear();
        //ClearSmallBalls();

        for (int i = 0; i < _lstScatterPoints.Count; ++i )
        {
            GameObject go = ObjectPoolController.Instantiate(_PrefabSmallBall.gameObject, transform.position, Quaternion.identity);
            go.gameObject.SetActive(true);
            go.transform.localScale = Vector2.Lerp(Vector2.one * 0.5f, Vector2.one * 1.3f, Random.Range(0f, 1f));
            Elem_SmallWhiteBall sb = go.GetComponent<Elem_SmallWhiteBall>();
            _lstSmallBalls.Add(sb);
            sb.Init(this,
                _lstScatterPoints[i], _fSmallBallScatterDur,
                _fSmallballStartInhaleDest,
                _fSmallballCombineDest);

            sb._lifeCom._event._eventOnAddValue += _event_OnSmallballHurted;
        }
        yield return 0;
    }

    IEnumerator OnGather()
    {        
        while (_lstSmallBalls.Count > 0)
        {            
            if (_lstSmallBalls[0] == null
                || !_lstSmallBalls[0].gameObject.activeInHierarchy)
            {
                _lstSmallBalls.RemoveAt(0);
                continue;
            }
            Vector3 vecShake = Vector3.Lerp(new Vector3(0.5f, 0.5f, 0), Vector3.zero, (float)_lstSmallBalls.Count / (float)_lstScatterPoints.Count);
            transform.DOMove(_lstSmallBalls[0].transform.position, 0.1f).SetEase(Ease.InOutCubic);
            transform.DOShakeScale(0.1f, vecShake);
            yield return new WaitForSeconds(0.2f);
        }
        transform.localScale = Vector3.one;
        _lstSmallBalls.Clear();
        ClearSmallBalls();
    }

    public void OnInHale(Elem_SmallWhiteBall smallball)
    {
        if (smallball != null)
        {
            _lstSmallBalls.Remove(smallball);
            smallball._lifeCom._event._eventOnAddValue -= _event_OnSmallballHurted;
            smallball.gameObject.SetActive(false);
            _lstWillDestroy.Add(smallball);
            _trBody.localScale = Vector3.Lerp(Vector3.one * _fSrcScale, Vector3.one, (float)_lstSmallBalls.Count / (float)_lstScatterPoints.Count);
        }
    }

    void _event_OnSmallballHurted(int nValue)
    {
        if (nValue >= 0)
        {
            return;
        }

        TagLog.Log(LogIndex.Enemy, "SmallBallHurted:" + nValue);
    }

    void ClearSmallBalls()
    {
        for (int i = 0; i < _lstWillDestroy.Count; i++)
        {
            _lstWillDestroy[i]._lifeCom._event._eventOnAddValue -= _event_OnSmallballHurted;
            if (_lstWillDestroy[i] != null)
            {
                ObjectPoolController.Destroy(_lstWillDestroy[i].gameObject);
            }
        }
    }

    ////TEST CODE ↓↓↓↓↓↓↓///////////////////////////////////////////////////////////////////////

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Home))
        {
            GenerateScatterPoint(transform.position, _fScatterAreaRadius/5f, _fScatterAreaRadius, _nPtCount);
            StartCoroutine(OnCreateSmallBall());
        }
        if (Input.GetKeyUp(KeyCode.End))
        {
            StartCoroutine(OnGather());
        }
    }

    public float _fDrawSphereR = 1f;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _fScatterAreaRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _fSmallballStartInhaleDest);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _fSmallballCombineDest);
        Gizmos.color = Color.green;
        for (int i = 0; i < _lstScatterPoints.Count; i++)
        {
            Gizmos.DrawLine(transform.position, _lstScatterPoints[i]);
            Gizmos.DrawSphere(_lstScatterPoints[i], _fDrawSphereR);
        }
    }

}

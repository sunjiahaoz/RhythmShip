using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class FibonacciMono : MonoBehaviour {
    public float _fRadius = 0.3f;
    public int _nCount = 6;
    public int _nScale = 1;
    Fibonacci _fib = new Fibonacci();

    List<Vector3> _fibPos = null;
    List<FibonacciCell> _fibCell = null;

    void Update()
    {
        _fib.Generate(_nCount, _nScale);
        _fibPos = _fib.GetFibnacciPointPos();
        _fibCell = _fib.GetFibnacciCell();

        int nCount = Mathf.Min(transform.childCount, _fibPos.Count);
        for (int i = 0; i < nCount; ++i)
        {
            transform.GetChild(i).position = _fibPos[i];
        }
    }

    void OnDrawGizmos()
    {
        if (_fibPos != null)
        {
            for (int i = 0; i < _fibPos.Count; ++i)
            {
                Gizmos.DrawSphere(_fibPos[i], (i + 1) * _fRadius);
            }
        }

        if (_fibCell != null)
        {
            for (int i = 0; i < _fibCell.Count; ++i)
            {
                _fibCell[i].OnGizmoDraw();                
            }
        }
    }
}

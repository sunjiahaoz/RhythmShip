using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class GridFirePoint : FirePointRhythm
{
    [Header("随机点或按顺序循环")]
    public bool _bRandom = true;
    [Header("随机的时候是否不能重复")]
    public bool _bRandomNoRepeat = true;
    public Transform _posRoot;
    int _nCurIndex = 0;

    List<Transform> _lstPos = null;
    // 用于保存一份随机的索引队列
    List<int> _lstPosIndex = new List<int>();
    protected override void Awake()
    {
        base.Awake();
        if (_posRoot == null)
        {
            TagLog.LogError(LogIndex.FirePoint, "GridFirePoint 需要 _posRoot");
            return;
        }
        _lstPos = ToolsUseful.GetComponentsInChildren<Transform>(_posRoot);
        
        if (_lstPos.Count == 0)
        {
            TagLog.LogError(LogIndex.FirePoint, "GridFirePoint 需要子节点");
            return;
        }

        ResetListPosIndex();
        _nCurIndex = 0;
    }

    void ResetListPosIndex()
    {
        _lstPosIndex.Clear();
        for (int i = 0; i < _lstPos.Count; ++i)
        {
            _lstPosIndex.Add(i);
        }
        ToolsUseful.Shuffle(_lstPosIndex);
    }

    public override void Fire()
    {
        Vector3 pos = GetNextPos();
        if (_bCreateBulletAfterAnim)
        {
            FireEffect(pos, true);
        }
        else
        {
            CreateObject(pos);
            FireEffect(pos);
        }
    }

    protected virtual Vector3 GetNextPos()
    {
        if (_bRandom)
        {
            int nIndex = Random.Range(0, _lstPos.Count);
            if (_bRandomNoRepeat)
            {
                if (_lstPosIndex.Count == 0)
                {
                    ResetListPosIndex();                    
                }

                if (_lstPosIndex.Count > 0)
                {
                    nIndex = _lstPosIndex[0];
                    _lstPosIndex.RemoveAt(0);
                }                
            }
            return _lstPos[nIndex].position;
        }
        else
        {
            if (_nCurIndex >= _lstPos.Count)
            {
                _nCurIndex = 0;
            }
            return _lstPos[_nCurIndex++].position;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class GridFirePoint : FirePointRhythm
{
    [Header("随机点或按顺序循环")]
    public bool _bRandom = true;
    public Transform _posRoot;
    int _nCurIndex = 0;

    List<Transform> _lstPos = null;
    protected override void Awake()
    {
        base.Awake();
        if (_posRoot == null)
        {
            TagLog.LogError(LogIndex.FirePoint, "GridFirePoint 需要 _posRoot");
            return;
        }
        _lstPos = ToolsUseful.GetComponentsInChildren<Transform>(_posRoot);
        //_lstPos.Sort((tr1, tr2) => 
        //{
        //    return tr1.name.CompareTo(tr2.name);
        //});

        TagLog.Log(LogIndex.FirePoint, "GridFirePoint posLength:" + _lstPos.Count);
        ToolsUseful.DebugOutList<Transform>(_lstPos);
        if (_lstPos.Count == 0)
        {
            TagLog.LogError(LogIndex.FirePoint, "GridFirePoint 需要子节点");
            return;
        }
        _nCurIndex = 0;
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
            return _lstPos[Random.Range(0, _lstPos.Count)].position;
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

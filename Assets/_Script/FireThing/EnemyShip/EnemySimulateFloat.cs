using UnityEngine;
using System.Collections;
using sunjiahaoz;
using DG.Tweening;

public class EnemySimulateFloat : EnemyRhythmRecordShip {
    [Header("====EnemySimulateFloat=====")]
    public bool _bDrawGizmo = false;
    public Transform _trStartPos;
    public Transform _trGoalPos;

    public float _fMovePerPointDur = 0.2f;
    public IntRange _pointCountRange;
    public FloatRange _HOffRange;
    public bool _bDestroyWhenEnd = true;

    Vector3[] _path = null;

    protected override void Awake()
    {
        base.Awake();        
    }

    Tweener _twnerMove = null;
    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);
    }

    public override void OnThingDestroy()
    {
        base.OnThingDestroy();
        if (_twnerMove != null)
        {
            _twnerMove.Kill();
            _twnerMove = null;
        }
    }

    public void GeneratePath()
    {
        
        // 如果没有开始位置，就以自身为开始位置
        if (_trStartPos == null)
        {
            _trStartPos = transform;
        }
        // 如果没有结束为止，就让playerSHip为目标位置
        if (_trGoalPos == null)
        {
            GameObject[] gos = TargetFindMgr.Instance.GetFindLogic(FindLogicType.PlayerShip).Find();
            if (gos == null
                || gos.Length == 0)
            {
                OnThingDestroy();
            }
            else
            {
                _trGoalPos = gos[0].transform;
            }
        }
        transform.position = _trStartPos.position;
        _path = UtilityTool.GenerateSimulateFloatPath(_trStartPos.position, _trGoalPos.position, _pointCountRange.RandomValue, _HOffRange.Min, _HOffRange.Max);
        _nPathIndex = 1;    // 0是自身位置
    }

    int _nPathIndex = 0;
    protected override void OnPlayOneShot(int nIndex)
    {
        base.OnPlayOneShot(nIndex);
        if (_bRecord)
        {
            return;
        }
        if (_nPathIndex >= _path.Length)
        {
            if (_bDestroyWhenEnd)
            {
                OnThingDestroy();
            }
            return;
        }
        //TagLog.Log(LogIndex.Enemy, "OneShotIndex:" + _nPathIndex);
        float fDur = GetNextRhythmPointTimeInterval(nIndex);
        if (fDur <= 0)
        {
            fDur = _fMovePerPointDur;
        }
        _twnerMove = transform.DOMove(_path[_nPathIndex], fDur);
        _nPathIndex++;
        if (_bDestroyWhenEnd
            && _nPathIndex >= _path.Length)
        {
            _twnerMove.OnComplete(() => 
            {
                OnThingDestroy();
            });
        }
    }

    protected override void OnPlayEnd()
    {
        base.OnPlayEnd();
        OnThingDestroy();
    }

    void OnDrawGizmos()
    {
        if (_bDrawGizmo)
        {
            if (_path != null)
            {
                for (int i = 0; i < _path.Length; ++i )
                {                    
                    Gizmos.DrawSphere(_path[i], 30);
                    if (i + 1 < _path.Length)
                    {
                        Gizmos.DrawLine(_path[i], _path[i + 1]);
                    }
                }
            }            
        }
    }
}

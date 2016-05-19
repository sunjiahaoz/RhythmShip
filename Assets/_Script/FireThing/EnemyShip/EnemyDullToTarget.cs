using UnityEngine;
using System.Collections;
using sunjiahaoz;
using sunjiahaoz.SteerTrack;
using DG.Tweening;

[RequireComponent(typeof(SteeringDirLine))]
public class EnemyDullToTarget : BaseEnemyShip
{
    public FloatRange _MoveToDullPosRange;
    public float _fMoveToDullDur = 0.5f;
    public Ease _easeMoveToDull = Ease.Linear;
    // 发呆时间
    public float _fDullDur = 0.5f;
    SteeringDirLine _comSteer;

    protected override void Awake()
    {
        base.Awake();
        _comSteer = GetComponent<SteeringDirLine>();
    }

    bool _bIsInProcess = false;
    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);
        _comSteer._vec2DirOff = Vector2.zero;
        StartCoroutine("OnProcess", fp.GetDir());
    }

    Tweener _twnerMove = null;
    IEnumerator OnProcess(Vector3 dir)
    {
        _bIsInProcess = true;

        // 生成位置
        Vector3 posOff = dir * _MoveToDullPosRange.RandomValue;
        // 先移动到发呆位置
        _twnerMove = transform.DOMove(transform.position + posOff, _fMoveToDullDur).SetEase(_easeMoveToDull);
        yield return new WaitForSeconds(_fMoveToDullDur);
        _twnerMove = null;
        // 盯住目标
        GameObject[] gos = TargetFindMgr.Instance.GetFindLogic(FindLogicType.PlayerShip).Find();
        if (gos == null
            || gos.Length == 0)
        {
            yield break;
        }
        Vector3 toDir = ToolsUseful.LookDir(transform.position, gos[0].transform.position);
        yield return new WaitForSeconds(_fDullDur);
        _comSteer._vec2DirOff = toDir;
        _bIsInProcess = false;
    }

    public override void OnThingDestroy()
    {
        base.OnThingDestroy();
        if (_bIsInProcess)
        {            
            StopCoroutine("OnProcess");
            if (_twnerMove != null)
            {
                _twnerMove.Kill();
                _twnerMove = null;
            }
        }
        _comSteer._vec2DirOff = Vector2.zero;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;
using sunjiahaoz.SteerTrack;
using RUL;
using DG.Tweening;

public class EnemyShip_Rail : BaseRhythmEnemyShip
{
    [Header("====EnemyShip_Rail====")]
    public EffectParam _PathPointShowEffect;
    public bool _bDebugDraw = true;
    public ObjectAnim_FollowPath _trail;
    public Transform _disappearHead;
    public AutoLineColliderCom[] _lineCollider;

    public float _fTrailMaxLength = 2000;
    public float _fLineColliderSize = 100;
    public float _fTailDur = 1f;        // 路径完整之后持续多长时间死掉
    public float _fDiappearDur = 1.5f;  // 死掉动画持续时间

    [Header("只生成水平rail的概率")]
    [Range(0f, 1f)]
    public float _fOnlyHProb = 0.5f;
    [Header("起始点是左边或上边的概率")]
    [Range(0f, 1f)]
    public float _fOnlyStartIsLeftTopProb = 0.5f;

    static Vector3 _leftTop = Vector3.zero;
    static Vector3 _rightBottom = Vector3.zero;
    static float _fMinIntervalDistWidth = 0;
    static float _fMinIntervalDistHeight = 0;

    Vector3[] _lstPoses = null;
    SplineTrailRenderer _trailRenderer;
    
    protected override void Awake()
    {
        base.Awake();
        _leftTop = GamingData.Instance.CamMgr.GetAnchorPos(CameraAnchorPos.LeftTop).position;
        _rightBottom = GamingData.Instance.CamMgr.GetAnchorPos(CameraAnchorPos.RightBottom).position;
        _fMinIntervalDistWidth = GamingData.Instance.CamMgr.GetWidthDistance() / 6;
        _fMinIntervalDistHeight = GamingData.Instance.CamMgr.GetHeightDistance() / 6;
        _trailRenderer = _trail.GetComponent<SplineTrailRenderer>();
    }
    
    public override void OnThingCreate(IFirePoint creator)
    {
        base.OnThingCreate(creator);
        RunTail();
    }

    public void RunTail()
    {
        // 重置
        for (int i = 0; i < _lineCollider.Length; ++i)
        {
            _lineCollider[i].Reset();
        }
        _trailRenderer.Clear();
        _trailRenderer.maxLength = _fTrailMaxLength;

        // 计算路径点
        _lstPoses = UtilityTool.GenerateRail(
            _leftTop, _rightBottom, 
            _fMinIntervalDistWidth, _fMinIntervalDistHeight,
            Random.Range(0f, 1f) < _fOnlyHProb ? false : true,
            Random.Range(0f, 1f) < _fOnlyStartIsLeftTopProb ? true : false
            );        
        // 执行路径点
        _trail.transform.position = _lstPoses[0];        
        _trail._path = _lstPoses;
        _trail._actionWayPointChange = OnWayPointChange;
        _trail.Run();
        // 路径点提示特效
        if (_PathPointShowEffect._strName.Length > 0)
        {
            for (int i = 0; i < _lstPoses.Length; ++i)
            {
                _PathPointShowEffect._pos = _lstPoses[i];
                ShotEffect.Instance.Shot(_PathPointShowEffect);
            }
        }        
    }

    void OnWayPointChange(int nIndex)
    {        
        if (nIndex > 0)
        {
            _lineCollider[0].CalCollider(_lstPoses[0], _lstPoses[1], _fLineColliderSize, _fLineColliderSize);
        }
        if (nIndex > 1)
        {
            _lineCollider[1].CalCollider(_lstPoses[1], _lstPoses[2], _fLineColliderSize, _fLineColliderSize);
        }
        if (nIndex > 2)
        {
            _lineCollider[2].CalCollider(_lstPoses[2], _lstPoses[3], _fLineColliderSize, _fLineColliderSize);

            Invoke("DisappearTail", _fTailDur);
        }
        //TagLog.Log(LogIndex.Enemy, "RailLength:" + _trailRenderer.spline.Length());
    }

    // 退出舞台
    void DisappearTail()
    {
        DOVirtual.Float(_trailRenderer.spline.Length(), 0, _fDiappearDur, (val) =>
        {
            if (val > _trailRenderer.maxLength)
            {
                //TagLog.LogWarning(LogIndex.Enemy, "当前值居然比最大值大了？？？！！" + "cur:" + val + " MaxLength:" + _trailRenderer.maxLength);
                return;
            }
            _trailRenderer.maxLength = val;
            //TagLog.LogWarning(LogIndex.Enemy, "cur:" + val + " MaxLength:" + _trailRenderer.maxLength);
        }).SetEase(Ease.Linear);        
        StartCoroutine(OnDisappearTail(_fDiappearDur));
    }

    IEnumerator OnDisappearTail(float fDur)
    {
        float fTotalLength = _trailRenderer.spline.Length();
        float fDistanceSeg0 = _lineCollider[0].lineCollider.size.x;
        float fDistanceSeg1 = _lineCollider[1].lineCollider.size.x;
        float fDistanceSeg2 = _lineCollider[2].lineCollider.size.x;
        float fTotalDistance = fDistanceSeg0 + fDistanceSeg1 + fDistanceSeg2;
        float fRatio0 = fDistanceSeg0 / fTotalDistance;
        float fRatio1 = fDistanceSeg1 / fTotalDistance;
        float fRatio2 = fDistanceSeg2 / fTotalDistance;

        yield return DisappearSeg(0, fRatio0 * fDur);
        yield return DisappearSeg(1, fRatio1 * fDur);
        yield return DisappearSeg(2, fRatio2 * fDur);
        // 移除
        OnThingDestroy();
    }
    
    IEnumerator DisappearSeg(int nIndex, float fDur)
    {
        _disappearHead.position = _lstPoses[nIndex];
        _disappearHead.DOMove(_lstPoses[nIndex+1], 0.5f)
            .OnUpdate(() =>
            {
                _lineCollider[nIndex].CalCollider(_disappearHead.position, _lstPoses[nIndex+1], _fLineColliderSize, _fLineColliderSize);
            });
        yield return new WaitForSeconds(fDur);
        _lineCollider[nIndex].Reset();                
    }


    void OnDrawGizmos()
    {
        if (_bDebugDraw
            && _lstPoses != null
            && _lstPoses.Length > 0)
        {
            for (int i = 0; i < _lstPoses.Length; ++i)
            {
                Gizmos.DrawSphere(_lstPoses[i], 50);
            }
        }        
    }
}

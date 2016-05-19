using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BulletEleRayBall : BaseBullet {
    [Header("移动到参数")]
    public FloatRange _moveToTargetDest;
    public float _fMoveToTargetDur = 0.5f;
    public Ease _easeMoveTo = Ease.Linear;
    [Header("扩大")]
    public float _fSrcScale = 5;
    public float _fScaleDur = 0.5f;
    public Ease _easeScale = Ease.InSine;
    [Header("射线")]
    public RayController _ray;    
    public float _fRayDur = 5f;
    [Header("出现参数")]
    public float _fRayLength;
    public float _fRayWidth;
    public float _fAppearDur = 0.2f;
    public Ease _rayAppearEase = Ease.Linear;
    public RayAppearType _rayAppearType = RayAppearType.H;
    [Header("消失参数")]
    public float _fDisappearDur = 0.2f;
    public Ease _rayDisappearEase = Ease.Linear;
    public RayDisappearType _rayDisappearType = RayDisappearType.H;

    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);

        _ray.gameObject.SetActive(false);
        transform.localScale = Vector3.one;
        StartCoroutine(OnProgress(fp.GetDir()));
    }    
    
    IEnumerator OnProgress(Vector3 dir)
    {        
        // 先移动到一个随机点
            // 生成随机点
        Vector3 posOff = dir * _moveToTargetDest.RandomValue;
        transform.DOMove(transform.position + posOff, _fMoveToTargetDur).SetEase(_easeMoveTo);
        yield return new WaitForSeconds(_fMoveToTargetDur);
            // sacle扩大
        transform.DOScale(Vector3.one * _fSrcScale, _fScaleDur).SetEase(_easeScale);
        yield return new WaitForSeconds(_fScaleDur);
        // 展开
            // 随机方向
        Vector3 rayDir = RUL.RulVec.RandUnitVector2();
        _ray.gameObject.SetActive(true);
        _ray.PlayRayAppear(rayDir, _fRayLength, _fRayWidth, _fAppearDur, _rayAppearEase, _rayAppearType);            
        yield return new WaitForSeconds(_fAppearDur + _fRayDur);
        // 展开之后就收缩
        _ray.PlayRayDisappear(_fDisappearDur, _rayDisappearEase, _rayDisappearType);
        transform.DOScale(Vector3.one, _fDisappearDur);
        yield return new WaitForSeconds(_fDisappearDur);
        // 收缩之后就摧毁
        OnThingDestroy();        
    }
}

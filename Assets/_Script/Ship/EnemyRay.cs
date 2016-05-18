using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyRay : BaseEnemyShip {
    public RayController _rayBody;
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
        SwitchColliderTrigger(false);
        _rayBody.PlayRayAppear(fp.GetDir(), _fRayLength, _fRayWidth, _fAppearDur, _rayAppearEase, _rayAppearType, () => 
        {
            SwitchColliderTrigger(true);
            vp_Timer.In(_fRayDur, () => 
            {
                OnThingDestroy();
            });
        });
    }

    public override void OnThingDestroy()
    {        
        _rayBody.PlayRayDisappear(_fDisappearDur, _rayDisappearEase, _rayDisappearType, () => 
        {
            base.OnThingDestroy();
        });
    }
}

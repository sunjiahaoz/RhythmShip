using UnityEngine;
using System.Collections;
using DG.Tweening;
using sunjiahaoz;

public enum RayAppearType
{
    H,  // 水平展开
    V,  // 垂直展开
    HV, // 水平，垂直同时展开
}

public enum RayDisappearType
{
    H,      // 水平收缩
    V_ToTarget, // 垂直收缩，往线另一端点方向收缩
    V_ToLocal,  // 垂直收缩
    HV_ToTarget,
    HV_ToLocal,
}
public class RayController : MonoBehaviour
{
    public float _fSpeed = -2f;
    public Transform _body;

    Vector3 _srcScale = Vector3.zero;
    NcUvAnimation _comAnim;
    IKAnchor _bodyIKAnchor;
    void Awake()
    {
        _srcScale = _body.localScale;
        _comAnim = _body.GetComponentInChildren<NcUvAnimation>();
        _comAnim.m_fScrollSpeedY = _fSpeed;
        _bodyIKAnchor = _body.GetComponentInChildren<IKAnchor>();
        _bodyIKAnchor._trParent = _body;
    }

    // 设置图片滚动速度
    public void SetRayScrollSpeed(float fSpeed)
    {
        _fSpeed = fSpeed;
        _comAnim.m_fScrollSpeedY = _fSpeed;
    }
    public void SetLength(float fLength)
    {
        Vector3 scale = _body.localScale;
        scale.z = fLength;
        _body.localScale = scale;
    }
    public void SetWidth(float fWidth)
    {
        Vector3 scale = _body.localScale;
        scale.x = fWidth;
        _body.localScale = scale;
    }


    // 执行激光展开动画
    public void PlayRayAppear(float fLength, float fWidth, float fDur, Ease ease = Ease.Linear, RayAppearType appearType = RayAppearType.HV)
    {
        switch (appearType)
        {
            case RayAppearType.H:
                SetLength(fLength);
                break;
            case RayAppearType.V:
                SetWidth(fWidth);
                break;
            case RayAppearType.HV:
                break;
            default:
                break;
        }
        Vector3 scaleTarget = _body.localScale;
        scaleTarget.x = fWidth;
        scaleTarget.z = fLength;
        _body.DOScale(scaleTarget, fDur).SetEase(ease);
    }

    // 激光收缩动画
    public void PlayRayDisappear(float fDur, Ease ease = Ease.Linear, RayDisappearType disappearType = RayDisappearType.V_ToLocal)
    {
        Vector3 scaleTarget = _body.localScale;        
        bool bUseIK = false;
        switch (disappearType)
        {
            case RayDisappearType.H:
                scaleTarget.x = 0;
                break;
            case RayDisappearType.V_ToTarget:
                bUseIK = true;
                scaleTarget.z = 0;
                break;
            case RayDisappearType.V_ToLocal:
                bUseIK = false;
                scaleTarget.z = 0;
                break;
            case RayDisappearType.HV_ToTarget:
                scaleTarget.x = 0;
                scaleTarget.z = 0;
                bUseIK = true;
                break;
            case RayDisappearType.HV_ToLocal:
                scaleTarget.x = 0;
                scaleTarget.z = 0;
                bUseIK = false;
                break;
            default:
                break;
        }
        Vector3 pos = _bodyIKAnchor.transform.position;
        Tweener twner = _body.DOScale(scaleTarget, fDur)
            .SetEase(ease)
             .OnComplete(() =>
            {
                _body.localPosition = Vector3.zero;
                _body.localScale = _srcScale;
            });

        if (bUseIK)
        {
            twner.OnUpdate(() =>
            {
                _bodyIKAnchor.SetPosition(pos);
            });
        }
    }

    

    // TEST CODE
    [Header("测试用代码")]
    public float _fTestLength = 110;
    public float _fTestWidth = 100;
    public float _fTestDur = 0.2f;
    public Ease _easeTest = Ease.Linear;
    public RayAppearType _testAppType = RayAppearType.H;
    public RayDisappearType _testDisappType = RayDisappearType.V_ToLocal;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Home))
        {
            PlayRayAppear(_fTestLength, _fTestWidth, _fTestDur, _easeTest, _testAppType);
        }
        if (Input.GetKeyUp(KeyCode.End))
        {
            //SetRayScrollSpeed(-_fSpeed);
            PlayRayDisappear(_fTestDur, _easeTest, _testDisappType);
        }
    }
}

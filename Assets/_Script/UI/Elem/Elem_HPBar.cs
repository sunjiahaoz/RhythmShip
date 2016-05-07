using UnityEngine;
using System.Collections;
using sunjiahaoz;
using DG.Tweening;

public class Elem_HPBar : MonoBehaviour {
    public UISlider _sliderBar;
    public float _fChangeSpeed = 100;   // 进度条变化速度

    BaseLifeCom _lifeComListened = null;
    public void Init(BaseLifeCom lifeCom)
    {
        _lifeComListened = lifeCom;
        _lifeComListened._event._eventOnAddValue += _event__eventOnAddValue;
        UpdateHPBar();        
    }

    void OnDestroy()
    {
        _lifeComListened._event._eventOnAddValue -= _event__eventOnAddValue;
    }

    void _event__eventOnAddValue(int nOffValue)
    {
        UpdateHPBar();        
    }

    float _fProgress = 0;    
    float _fTargetValue = 1;
    void Update()
    {
        _fProgress += Time.deltaTime/_fChangeSpeed;
        if(_fProgress > 1) _fProgress = 1;
        _sliderBar.value = Mathf.Lerp(_sliderBar.value, _fTargetValue, _fProgress);
    }

    void UpdateHPBar()
    {
        _fTargetValue = _lifeComListened.CurPercent();        
    }
}

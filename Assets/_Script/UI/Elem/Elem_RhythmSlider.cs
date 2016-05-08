using UnityEngine;
using System.Collections;

public class Elem_RhythmSlider : MonoBehaviour {
    public UISlider _slider;
    public UILabel _lbCurTime;

    AudioObject _curAo = null;
    void Start()
    {
        _curAo = GamingData.Instance.gameBattleManager.CurAO;
        GamingData.Instance.gameBattleManager._event._eventOnState_Start += _event__eventOnState_Start;
    }

    void OnDestroy()
    {
        GamingData.Instance.gameBattleManager._event._eventOnState_Start -= _event__eventOnState_Start;
    }

    void _event__eventOnState_Start()
    {
        _curAo = GamingData.Instance.gameBattleManager.CurAO;
    }

    void Update()
    {
        if (_bIsPressThumb
            || _slider == null
            || _curAo == null
            || !_curAo.IsPlaying())
        {
            return;
        }
        _slider.value = _curAo.audioTime / _curAo.clipLength;
        _lbCurTime.text = sunjiahaoz.ToolsUseful.Retain2Decimals(_curAo.audioTime) + "/" + sunjiahaoz.ToolsUseful.Retain2Decimals(_curAo.clipLength);
    }


    bool _bIsPressThumb = false;
    public void OnThumbPress()
    {
        _bIsPressThumb = true;
    }

    public void OnThumbRelease()
    {
        _curAo.audioTime = _slider.value * _curAo.clipLength;
        _bIsPressThumb = false;
    }
}

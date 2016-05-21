using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class Elem_HPHeartCircle : MonoBehaviour {    
    Elem_HPHeart[] _hearts = null;
    int _nNextLoseIndex = 0;
    BaseLifeCom _lifeCom;

    int _nLoseHeartPerCount = 0;    // 每减少这么多血的时候lose一个heart
    int _nCurHeartCountPerLose = 0;
    public void Init(BaseLifeCom lifeCom)
    {
        _lifeCom = lifeCom;
        int nMaxHP = _lifeCom.MaxValue;        
        _hearts = GetComponentsInChildren<Elem_HPHeart>();
        for (int i = 0; i < _hearts.Length; ++i )
        {
            _hearts[i].Init();
        }
        _nNextLoseIndex = 0;
        _nLoseHeartPerCount = nMaxHP / _hearts.Length;
        _nCurHeartCountPerLose = 0;

        _lifeCom._event._eventOnAddValue += _event_OnShipHurt;
    }

    void OnDestroy()
    {
        _lifeCom._event._eventOnAddValue -= _event_OnShipHurt;
    }

    void _event_OnShipHurt(int nOff)
    {
        if (nOff > 0)
        {
            TagLog.Log(LogIndex.UI, "加血的效果没有！");
        }
        else if(nOff < 0)
        {
            _nCurHeartCountPerLose += nOff;
            if (_nCurHeartCountPerLose <= 0)
            {
                LoseLife();
                _nCurHeartCountPerLose = _nLoseHeartPerCount;
            }            
        }
    }

    void LoseLife()
    {
        if (_nNextLoseIndex < 0
        || _nNextLoseIndex >= _hearts.Length)
        {
            return;
        }
        Elem_HPHeart loseHeart = _hearts[_nNextLoseIndex++];
        StartCoroutine(OnLoaseHeart(loseHeart));
    }



    IEnumerator OnLoaseHeart(Elem_HPHeart loseHeart)
    {   
        iTween.RotateTo(gameObject, new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -loseHeart.transform.localEulerAngles.z), 0.3f);
        yield return new WaitForSeconds(0.3f);
        loseHeart.PlayLoseHeart();
    }    
}

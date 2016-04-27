using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefaultRhythmAction : MonoBehaviour {

    public List<EventDelegate> _lstAction;
    // 延迟几个节奏之后才开始
    public int _nDelayCount = 0;
    // 每收到几次节奏执行一次
    public int _nIntervalCount = 1;
	// Use this for initialization
	void OnEnable () 
    {
        GamingData.Instance.sceneConfig._event._eventOnRhythmNormal += _event__eventOnRhythmNormal;
	}	

    void OnDisable()
    {
        if (GamingData.Instance.sceneConfig != null)
        {
            GamingData.Instance.sceneConfig._event._eventOnRhythmNormal -= _event__eventOnRhythmNormal;
        }        
    }

    int _nCurDelayCount = 0;
    int _nCurIntervalCount = 0;
    void _event__eventOnRhythmNormal()
    {
        // 如果需要delay
        if (_nDelayCount > 0)
        {
            _nCurDelayCount++;
            if (_nCurDelayCount < _nDelayCount)
            {
                return;
            }
        }
        
        _nCurIntervalCount++;
        if (_nCurIntervalCount >= _nIntervalCount)
        {
            EventDelegate.Execute(_lstAction);
            _nCurIntervalCount = 0;
        }        
    }
}

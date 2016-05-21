/*
*FirePointRhythm_PrepareAction
*by sunjiahaoz 2016-5-21
*
*可以在节奏上穿插其他动作
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PrepareAction
{
    // 索引，即在fire第几次的时候执行动作,从1开始
    public int _nIndex = 0;
    // 执行动作是否打断正常的fire流程
    public bool _bBreakFire = true;
    // 事件
    public List<EventDelegate> _lstAction;    
}
public class FirePointRhythm_PrepareAction : FirePointRhythm {
    public List<PrepareAction> _lstAction;
    Dictionary<int, PrepareAction> _dictAction = new Dictionary<int, PrepareAction>();

    // 当前fire的次数
    int _nCurIndex = 0;
    protected override void Awake()
    {
        base.Awake();
        _dictAction.Clear();
        for (int i = 0; i < _lstAction.Count; ++i )
        {
            _dictAction.Add(_lstAction[i]._nIndex, _lstAction[i]);
        }
        _nCurIndex = 0;
    }

    public override void Fire()
    {
        _nCurIndex++;
        if (_dictAction.ContainsKey(_nCurIndex))
        {
            EventDelegate.Execute(_dictAction[_nCurIndex]._lstAction);
            if (_dictAction[_nCurIndex]._bBreakFire)
            {                
                return;
            }
        }
        base.Fire();        
    }

}

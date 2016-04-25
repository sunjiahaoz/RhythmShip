/*
PlayerShipSkillGuideTypeBase
By: @sunjiahaoz, 2016-4-19

引导型技能
 * 按着右键每隔一段时间触发一次 
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class PlayerShipSkillGuideTypeBase : PlayerShipBaseSkill
{
    [Header("能量总值")]
    public float _fTotalFill = 100f;     // 能量总值
    [Header("执行一次消耗的能量值    ")]
    public float _fCostPerCast = 30f;  // 执行一次消耗的能量值    
    [Header("能量恢复速度")]
    public float _fRecoverSpeed = 1f; // 能量恢复速度
    [Header("执行一次后的间隔时间(秒)")]
    public float _fCastInterval = 1f;  // 执行一次后的间隔时间

    float _fCurFill = 100;
    float _fCurInterval = 0;

    public override void InitSkill(PlayerShip ship)
    {
        base.InitSkill(ship);
        _fCurFill = _fTotalFill;
        _fCurInterval = 0;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _fCurInterval += Time.deltaTime;            
            if (_fCurInterval >= _fCastInterval)
            {
                CastSubSkill();
                _fCurInterval = 0;
            }
        }
        else
        {
            _fCurInterval = 0;
            Update_Recover();
        }
        
    }

    protected virtual void CastSubSkill()
    {
        if (_fCurFill < _fCostPerCast)
        {
            TagLog.Log(LogIndex.Skill, "当前能量值不足，不执行技能");            
            return;
        }
        _fCurFill -= _fCostPerCast;
        TagLog.Log(LogIndex.Skill, "执行技能！！！！！");        
    }

    void Update_Recover()
    {
        if (_fCurFill >= _fTotalFill)
        {
            return;
        }
        _fCurFill += (Time.deltaTime * _fRecoverSpeed);
        _fCurFill = Mathf.Min(_fCurFill, _fTotalFill);
    }

}
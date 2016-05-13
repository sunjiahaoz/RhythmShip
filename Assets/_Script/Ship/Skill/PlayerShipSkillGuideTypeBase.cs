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
#region _Event_
    public class PlayerShipSkillGuideTypeEvent
    {
        // 0 成功释放技能， 1 能量不足
        public delegate void OnCastSkill(int nCastState);
        public event OnCastSkill _eventOnCastSkill;
        public void OnCastSkillEvent(int nCastState)
        {
            if (_eventOnCastSkill != null)
            {
                _eventOnCastSkill(nCastState);
            }
        }

        public delegate void OnCurEnergyValueChange(float fOff);
        public event OnCurEnergyValueChange _eventOnCurEnergyValueChange;
        public void OnCurEnergyValueChangeEvent(float fOff)
        {
            if (_eventOnCurEnergyValueChange != null)
            {
                _eventOnCurEnergyValueChange(fOff);
            }
        }
    }

    public PlayerShipSkillGuideTypeEvent _event = new PlayerShipSkillGuideTypeEvent();
#endregion

    [Header("能量总值")]
    public float _fTotalFill = 100f;     // 能量总值
    [Header("执行一次消耗的能量值    ")]
    public float _fCostPerCast = 30f;  // 执行一次消耗的能量值    
    [Header("能量恢复速度")]
    public float _fRecoverSpeed = 1f; // 能量恢复速度
    [Header("执行一次后的间隔时间(秒)")]
    public float _fCastInterval = 1f;  // 执行一次后的间隔时间

    [SerializeField]
    float _fCurFill = 100;
    public float CurFill
    {
        get { return _fCurFill; }
        set
        {
            float fOff = value - _fCurFill;
            _fCurFill = value;
            _event.OnCurEnergyValueChangeEvent(fOff);
        }
    }
    float _fCurInterval = 0;

    public override SkillType skillType
    {
        get
        {
            return SkillType.GuideType;
        }
    }

    public override void InitSkill(PlayerShip ship)
    {
        base.InitSkill(ship);
        CurFill = _fTotalFill;
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

    void CastSubSkill()
    {
        if (CurFill < _fCostPerCast)
        {
            DoWhenEnergyNotEnough();                        
            return;
        }
        CurFill -= _fCostPerCast;
        DoSubSKill();
    }
    void Update_Recover()
    {
        if (CurFill >= _fTotalFill)
        {
            return;
        }
        CurFill += (Time.deltaTime * _fRecoverSpeed);
        CurFill = Mathf.Min(CurFill, _fTotalFill);
    }


    // 主要实现以下这两个方法即可
    protected virtual void DoWhenEnergyNotEnough()
    {
        TagLog.Log(LogIndex.Skill, "当前能量值不足，不执行技能");
        _event.OnCastSkillEvent(1);
    }
    protected virtual void DoSubSKill()
    {
        TagLog.Log(LogIndex.Skill, "执行技能！！！！！");
        _event.OnCastSkillEvent(0);
    }

    // 当前能量百分比
    public float GetCurEnergyPercent()
    {
        return CurFill / _fTotalFill;
    }
}
/*
PlayerShipFillPowerTypeBase
By: @sunjiahaoz, 2016-4-19

蓄力型技能
 * 需要先进行技能的充能蓄力，充满后才可以释放执行
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class PlayerShipFillPowerTypeBase : PlayerShipBaseSkill{
    public float _fNeedTotalPower = 100;
    public float _fFillSpeed = 10f;
    public float _fDepleteSpeed = 5f;

    [SerializeField]
    float _fCurFillPower = 0;

    public override SkillType skillType
    {
        get
        {
            return SkillType.FillType;
        }
    }
    public override void InitSkill(PlayerShip ship)
    {
        base.InitSkill(ship);        
        _fCurFillPower = 0;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            // 已经冲满了
            if (_fCurFillPower >= _fNeedTotalPower)
            {
                return;
            }
            _fCurFillPower += (_fFillSpeed * Time.deltaTime);
            _fCurFillPower = Mathf.Min(_fCurFillPower, _fNeedTotalPower);
        }
        else
        {
            if (Mathf.Approximately(_fCurFillPower, _fNeedTotalPower))
            {
                CastSkill();
            }
            else
            {
                if (_fCurFillPower <= 0)
                {
                    return;
                }
                _fCurFillPower -= (_fDepleteSpeed * Time.deltaTime);
                _fCurFillPower = Mathf.Max(0, _fCurFillPower);
            }
        }
    }

    protected virtual void CastSkill()
    {
        _fCurFillPower = 0;
        TagLog.Log(LogIndex.Skill, "执行技能！！");
    }
}

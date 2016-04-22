/*
PlayerShipBaseSkillMotor
By: @sunjiahaoz, 2016-4-15

飞船技能（触发型）基类
 * 主要实现了技能的CD以及技能流程：
 * 右键弹起时执行技能，判断技能CD及其他检测条件，都通过之后执行技能。
 * 子类需要实现以下接口
 * bool CheckCanCast(); // 执行技能时的检测是否可以执行（除了CD判断）
 * void CastSkill();        // 执行技能
 * void OnInCD();           // 如果右键弹起时正在CD中则执行此函数
 * 
 * 
 * ship每次只能装备一个特殊技能，所以一个特殊技能就是一个脚本逻辑。
 * 在ship初始化的时候根据装备的特殊技能找到对应的脚本add上去
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class PlayerShipSkillTriggerTypeBase : PlayerShipBaseSkill {
    [Header("技能CD时间(毫秒)")]
    public int _nCDTime = 1000;    // 技能CD时间
    protected ActionCD _skillCD = new ActionCD();

    public override void InitSkill(PlayerShip ship)
    {
        base.InitSkill(ship);
    }
    
    
    protected virtual void Update()
    {
        _skillCD.Update(Time.time);
        // 右键
        if (Input.GetMouseButtonUp(1))
        {
            if (_skillCD.IsFinished
                && CheckCanCast())
            {
                CastSkill();
                _skillCD.StartCooldown(_nCDTime);
            }
            else
            {
                OnInCD();
            }
        }
    }

    /// <summary>
    /// 执行技能时的检测是否可以执行（除了CD判断）
    /// </summary>
    /// <returns></returns>
    protected virtual bool CheckCanCast()
    {
        return true;
    }
    /// <summary>
    /// 执行技能
    /// </summary>
    protected virtual void CastSkill()
    {
        TagLog.Log(LogIndex.Skill, "Cast Skill !!!!!!!!!!!!");
    }

    /// <summary>
    /// 如果右键弹起时正在CD中则执行此函数
    /// </summary>
    protected virtual void OnInCD()
    {
        TagLog.Log(LogIndex.Skill, "In CD !!!!");
    }
}

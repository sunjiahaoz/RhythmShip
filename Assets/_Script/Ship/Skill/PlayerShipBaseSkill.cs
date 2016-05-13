using UnityEngine;
using System.Collections;
using sunjiahaoz;

public enum SkillType
{
    TriggerType,    // 触发型
    GuideType,      // 引导型
    FillType,       // 蓄力型
}

public class PlayerShipBaseSkill : MonoBehaviour {
    public virtual SkillType skillType
    {
        get { return SkillType.FillType; }
    }
    PlayerShip _ship;
    public PlayerShip ship
    {
        get { return _ship; }
    }
    public virtual void InitSkill(PlayerShip ship)
    {
        _ship = ship;
    }
}

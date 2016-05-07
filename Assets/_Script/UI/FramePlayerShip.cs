using UnityEngine;
using System.Collections;

public class FramePlayerShip : CommonBaseFrame 
{
    public Elem_HPBar _slShipHP;
    public Elem_SkillGT _skillGT;

    public override void OnFrameShow()
    {
        base.OnFrameShow();
        _slShipHP.Init(GamingData.Instance.gameBattleManager.playerShip._lifeCom);

        _skillGT.Init(GamingData.Instance.gameBattleManager.playerShip._skill.CurSkill as PlayerShipSkillGuideTypeBase);
    }
}

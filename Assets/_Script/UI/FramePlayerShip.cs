using UnityEngine;
using System.Collections;

public class FramePlayerShip : CommonBaseFrame 
{
    public Elem_HPBar _slShipHP;
    public Elem_HPHeartCircle _cicleHP;

    public Elem_SkillGT _skillGT;
    public Elem_SkillTTBase _skillTT;

    public override void OnFrameShow()
    {
        base.OnFrameShow();
        if (_slShipHP != null)
        {
            _slShipHP.gameObject.SetActive(true);
            _slShipHP.Init(GamingData.Instance.gameBattleManager.playerShip._lifeCom);
        }
        else if (_cicleHP != null)
        {
            _cicleHP.gameObject.SetActive(true);
            _cicleHP.Init(GamingData.Instance.gameBattleManager.playerShip._lifeCom);
        }

        switch (GamingData.Instance.gameBattleManager.playerShip._skill.CurSkill.skillType)
        {
            case SkillType.TriggerType:
                {
                    _skillTT.gameObject.SetActive(true);
                    _skillTT.Init(GamingData.Instance.gameBattleManager.playerShip._skill.CurSkill as PlayerShipSkillTriggerTypeBase);
                }
                break;
            case SkillType.GuideType:
                {
                    _skillGT.gameObject.SetActive(true);
                    _skillGT.Init(GamingData.Instance.gameBattleManager.playerShip._skill.CurSkill as PlayerShipSkillGuideTypeBase);
                }
                break;
            case SkillType.FillType:
                break;
            default:
                break;
        }
    }
}

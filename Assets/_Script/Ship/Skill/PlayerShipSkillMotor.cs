using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class PlayerShipSkillMotor : MonoBehaviour
{

    PlayerShipBaseSkill _skill;

    public void EquipSkillMotor(int nID)
    {
        TagLog.Log(LogIndex.Ship, "装备技能ID：" + nID);
        GamingData.Instance.playerEquipedSkillMgr.CreateSkill(nID, (skill) =>
        {
            _skill = skill;
            _skill.InitSkill(GetComponentInParent<PlayerShip>());
            _skill.transform.parent = transform;
            _skill.transform.localPosition = Vector3.zero;
            _skill.transform.localScale = Vector3.one;
        });
    }
}

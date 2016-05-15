/*
Skill_TT_Defender
By: @sunjiahaoz, 2016-4-26

防护罩技能
 * 触发型技能
 * 在飞船所在位置撑起防护罩
 * 防护罩可以对指定Layer对象造成伤害
*/
using UnityEngine;
using System.Collections;

public class Skill_TT_Defender : PlayerShipSkillTriggerTypeBase {
    public SkillElem_Defender _prefabDefender;
    public string _strCastSound = "BulletEffect7";

    protected override void CastSkill()
    {
        base.CastSkill();
        SkillElem_Defender dfder = ObjectPoolController.Instantiate(_prefabDefender.gameObject, transform.position, Quaternion.identity).GetComponent<SkillElem_Defender>();
        dfder.gameObject.SetActive(true);
        dfder.InitDefend();
        AudioController.Play(_strCastSound);
    }
}

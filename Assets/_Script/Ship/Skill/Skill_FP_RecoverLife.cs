/*
Skill_FP_RecoverLife
By: @sunjiahaoz, 2016-4-20

恢复生命的技能
 * 蓄力型技能
 * 按指定比例进行恢复生命
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class Skill_FP_RecoverLife : PlayerShipFillPowerTypeBase {
    [Range(0f, 1f)]
    public float _fPercent = 0.5f;  // 恢复血量比例

    public EffectParam _effectCastSkill;

    void Awake()
    {
        _effectCastSkill._trBind = transform;
    }

    protected override void CastSkill()
    {
        base.CastSkill();
        float fAddValue = ship._lifeCom.MaxValue * _fPercent;
        ship._lifeCom.AddValue((int)fAddValue);
        TagLog.Log(LogIndex.Skill, "恢复生命:" + (int)fAddValue);

        if (_effectCastSkill._strName.Length > 0)
        {
            _effectCastSkill._pos = transform.position;
            ShotEffect.Instance.Shot(_effectCastSkill);
        }        
    }
}

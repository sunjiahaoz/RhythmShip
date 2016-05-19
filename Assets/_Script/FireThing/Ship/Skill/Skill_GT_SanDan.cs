/*
*Skill_GT_SanDan
*by sunjiahaoz 2016-5-6
*
*散弹
 *引导型技能
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class Skill_GT_SanDan : PlayerShipSkillGuideTypeBase
{
    public BulletFirePoint _fire;
    public ObjectAnim_Scale _firePoint;

    protected override void DoSubSKill()
    {
        base.DoSubSKill();
        _fire.Fire();
    }

    bool _bIsAnimRunning = false;
    protected override void DoWhenEnergyNotEnough()
    {
        if (_bIsAnimRunning)
        {
            return;
        }

        _bIsAnimRunning = true;
        _firePoint._actionComplete = OnNotEnouthAnimEnd;
        _firePoint.Run();
        //StartCoroutine(OnNotEnoughAnim());
    }

    void OnNotEnouthAnimEnd()
    {
        _bIsAnimRunning = false;
    }

    IEnumerator OnNotEnoughAnim()
    {
        _bIsAnimRunning = true;
        _firePoint.Run();
        yield return new WaitForSeconds(_firePoint._fDelay + _firePoint._fDur + 0.1f);
        _bIsAnimRunning = false;
    }
}

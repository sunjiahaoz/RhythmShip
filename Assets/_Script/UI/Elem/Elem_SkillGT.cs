using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class Elem_SkillGT : MonoBehaviour {
    public Elem_SkillGTCircle _circle;
    public Elem_GTEnergyBar _bar;

    PlayerShipSkillGuideTypeBase _guideSkill = null;    
    public void Init(PlayerShipSkillGuideTypeBase guideSkill)
    {
        _guideSkill = guideSkill;
        _circle.Init(guideSkill);
        _bar.Init(guideSkill);
    }
}

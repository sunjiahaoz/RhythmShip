using UnityEngine;
using System.Collections;

public class Elem_GTEnergyBar : MonoBehaviour {
    public UISlider _sliderBar;

    PlayerShipSkillGuideTypeBase _guideSkill = null;
    public void Init(PlayerShipSkillGuideTypeBase guidSkill)
    {
        _guideSkill = guidSkill;        
    }

    void Update()
    {
        _sliderBar.value = _guideSkill.GetCurEnergyPercent();
    }
}

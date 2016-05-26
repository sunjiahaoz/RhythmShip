using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class Elem_SkillItem :  UIControl_ListItemBase{
    public UILabel _lbID;
    int _nID = 0;
    public void Init(int nID)
    {
        _nID = nID;
        _lbID.text = nID.ToString();
    }

    public void OnBnClick()
    {
        if (!Equip_SkillConfig.dic.ContainsKey(_nID))
        {
            return;
        }

        GamingData.Instance.gameBattleManager.playerShip._skill.EquipSkillMotor(_nID);
    }
}

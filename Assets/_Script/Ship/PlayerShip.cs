using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class PlayerShip : BaseShip {

    public PlayerShipShootMotor _shooter = null;
    public PlayerShipSkillMotor _skill = null;

    public int _nEquipedFirePoint = 0;
    public int _nEquipedSkill = 0;

#region _Mono_
    void Start()
    {        
        OnThingCreate(null);
        _lifeCom._event._eventOnAddValue += _event__eventOnAddValue;
    }   
 
    void OnDestroy()
    {
        _lifeCom._event._eventOnAddValue -= _event__eventOnAddValue;
    }
#endregion

#region _BaseShip_
    // 初始化ship
    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);

        _skill.EquipSkillMotor(_nEquipedSkill);
        _shooter.EquipFirePoint(_nEquipedFirePoint);
        //_skill.EquipSkillMotor(GamingData.Instance.shipData.EquipedSkillId);
        //_shooter.EquipFirePoint(GamingData.Instance.shipData.EquipedShootFirePoint);
    }
#endregion    

    void _event__eventOnAddValue(int nValue)
    {
        if (nValue < 0)
        {
            GamingData.Instance.CamMgr._cShake.DoShake();
        }
    }

}

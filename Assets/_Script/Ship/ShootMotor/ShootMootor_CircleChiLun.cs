using UnityEngine;
using System.Collections;

public class ShootMootor_CircleChiLun : BaseShootMotorFirePoint
{
    public Elem_ChiLun[] _chilun;
    
    public override void OnEquiped()
    {
        base.OnEquiped();
        for (int i = 0; i < _chilun.Length; ++i )
        {
            _chilun[i].GenerateTradePos();
        }        
    }

    public override void Fire()
    {
        base.Fire();
        for (int i = 0; i < _chilun.Length; ++i)
        {
            _chilun[i].PushEnergy();
        }
    }
}

using UnityEngine;
using System.Collections;

public class ShootMootor_ManyGun : BaseShootMotorFirePoint
{
    public BaseFirePoint[] _guns;

    public override void Fire()
    {
        base.Fire();
        for (int i = 0; i < _guns.Length; ++i)
        {
            _guns[i].Fire();
        }
    }
}

﻿using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class PlayerShipShootMotor : MonoBehaviour 
{
    public int _nNorShootInterval = 1000;    // 普通射击间隔时间，毫秒
    protected ActionCD _cdNorShoot = new ActionCD();

    BulletFirePoint _firePoint;
    public BulletFirePoint firePoint
    {
        get { return _firePoint; }
    }

    public void EquipFirePoint(int nFirePointID)
    {
        TagLog.Log(LogIndex.Ship, "装备射击ID：" + nFirePointID);
        GamingData.Instance.playerEquipedFirePointMgr.CreateFirePoint(nFirePointID, (fp) => 
        {
            _firePoint = fp;
            _firePoint.transform.parent = transform;
            _firePoint.transform.localPosition = Vector3.zero;
            _firePoint.transform.localScale = Vector3.one;
        });
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_cdNorShoot.IsFinished)
            {
                _firePoint.Fire();
                _cdNorShoot.StartCooldown(_nNorShootInterval);
            }
        }

        _cdNorShoot.Update(Time.time);
    }
}

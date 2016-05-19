using UnityEngine;
using System.Collections;
using sunjiahaoz.SteerTrack;

public class BaseBulletNormalDir : BaseBullet {
    public SteeringDirLine _stDir;
    
    public override void OnThingCreate(IFirePoint fp)
    {
        //base.OnBulletCreate(fp);        
        base.OnThingCreate(fp);
        _stDir._vec2DirOff = fp.GetDir();
    }
}
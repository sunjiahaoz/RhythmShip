using UnityEngine;
using System.Collections;
using sunjiahaoz.SteerTrack;
public class BaseBulletNormalDir : BaseBullet {
    public SteeringDirLine _stDir;
    
    public override void OnBulletCreate(BaseFirePoint fp)
    {
        base.OnBulletCreate(fp);        
        _stDir._vec2DirOff = fp.GetDir();
    }
}

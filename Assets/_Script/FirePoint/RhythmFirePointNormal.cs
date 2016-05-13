using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class RhythmFirePointNormal : FirePointRhythm {
    public List<EventDelegate> _lstRhythm;

    public override void Fire()
    {
        base.Fire();
        EventDelegate.Execute(_lstRhythm);        
    }
}

using UnityEngine;
using System.Collections;
using sunjiahaoz.SteerTrack;

public class EnemyText_Small : EnemyText
{
    public Com_RandomSteerDir _randomDir;

    public override void OnThingCreate(IFirePoint fp)
    {
        //base.OnThingCreate(fp);
        //_dir = RUL.RulVec.RandUnitVector2();
    }

}

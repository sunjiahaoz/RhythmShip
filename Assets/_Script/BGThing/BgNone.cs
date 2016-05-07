/*
*BgNone
*by sunjiahaoz 2016-5-7
*
*空的东西，创建出来接着摧毁
*/
using UnityEngine;
using System.Collections;

public class BgNone : BaseFireThing 
{
    public override void OnThingCreate(IFirePoint pt)
    {
        base.OnThingCreate(pt);
        OnThingDestroy();
    }
}

using UnityEngine;
using System.Collections;
using sunjiahaoz;

[RequireComponent(typeof(ObjectAnim_2dSpriteAlpha))]
[RequireComponent(typeof(ObjeMoveAnim))]
public class BGCreatorLight : BaseFireThing {

    public float _fDur = 1f;
    public float _fMoveDest = 500;
    
    ObjectAnim_2dSpriteAlpha _animAlpha;
    ObjeMoveAnim _animMove;

    void Awake()
    {
        _animAlpha = GetComponent<ObjectAnim_2dSpriteAlpha>();
        _animMove = GetComponent<ObjeMoveAnim>();
    }

    public override void OnThingCreate(IFirePoint pt)
    {
        base.OnThingCreate(pt);
        FirePointRhythm fpp = pt as FirePointRhythm;
        float fInterval = 0;
        if (fpp == null
            || fpp.record._bRecord
            || fpp.GetNexInterval() <= 0)
        {
            fInterval = _fDur; 
        }
        else
        {
            fInterval = fpp.GetNexInterval();
            if (fInterval <= 0)
            {
                fInterval = _fDur;
            }            
        }
        _animMove._fDuration = fInterval;
        _animAlpha._fDur = fInterval;

        _animMove._vecStart = pt.transform.position;
        _animMove._vecGoal = _animMove._vecStart + pt.GetDir() * _fMoveDest;
        
        _animAlpha.Run();
        _animMove._actionComplete = () => 
        {            
            OnThingDestroy();
        };
        _animMove.Run();
    }

    public override void OnThingDestroy()
    {
        base.OnThingDestroy();
        _animAlpha.Stop();
        _animMove._actionComplete = null;
        ObjectPoolController.Destroy(gameObject);
    }
}

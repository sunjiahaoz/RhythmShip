using UnityEngine;
using System.Collections;
using DG.Tweening;
using sunjiahaoz;
using sunjiahaoz.SteerTrack;

[RequireComponent(typeof(SteeringDirLine))]
public class EnemyText_Ha : EnemyText {

    public EllipsoidParticleEmitter _emmitter;
    Tweener _twer = null;
    SteeringDirLine _stDir = null;

    protected override void Awake()
    {
        base.Awake();
        _stDir = GetComponent<SteeringDirLine>();
        _emmitter.emit = false;
    }

    public void SetTargetPos(Vector3 pos, float fDur)
    {
        _emmitter.emit = true;
        Vector3 dir = ToolsUseful.LookDir(transform.position, pos);
        _twer = transform.DOMove(pos, fDur).OnComplete(() => 
        {
            _stDir._vec2DirOff = dir;
            _emmitter.emit = false;
        });
    }

    public override void OnThingDestroy()
    {
        base.OnThingDestroy();
        _twer.Kill();
        _stDir._vec2DirOff = Vector2.zero;
        _emmitter.emit = false;
    }
}

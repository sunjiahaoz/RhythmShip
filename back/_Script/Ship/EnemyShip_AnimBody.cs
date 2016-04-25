/*
EnemyShip_AnimBody
By: @sunjiahaoz, 2016-4-19

    使用2d animator作为body的飞船基类
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class EnemyShip_AnimBody : BaseEnemyShip
{
    public tk2dSpriteAnimator _animTor;
    public bool _bDestroySelfWhenAnimEnd;

    protected override void Awake()
    {
        if (_animTor == null)
        {
            _animTor = GetComponentInChildren<tk2dSpriteAnimator>();
            if (_animTor == null)
            {
                TagLog.LogError(LogIndex.Ship, "EnemyShip_AnimBody 需要 tk2dSpriteAnimator");
                return;
            }
        }
        base.Awake();
        _animTor.AnimationCompleted += OnAnimComplete;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _animTor.AnimationCompleted -= OnAnimComplete;
    }

    public override void OnThingCreate(EnemyCreator creator)
    {
        base.OnThingCreate(creator);        
        _animTor.Play();
    }

    void OnAnimComplete(tk2dSpriteAnimator tor, tk2dSpriteAnimationClip clip)
    {
        if (_bDestroySelfWhenAnimEnd)
        {
            OnThingDestroy();
        }
    }

    public override void OnThingDestroy()
    {
        base.OnThingDestroy();
        _animTor.StopAndResetFrame();
    }
}

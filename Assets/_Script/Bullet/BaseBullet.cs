using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;
public class BaseBullet : BaseFireThing{

    public List<EventDelegate> _lstThingCreate;
    public List<EventDelegate> _lstThingDestroy;

    public ColliderTrigger _trigger;    
    public EffectParam _effectDestroy;
    public int _nHurtValue = 1;

    // 可以被碰撞的次数
    public int _nLifeCount = 1;
    int _nCurLifeCount = 1;

#region _Mono_
    void Awake()
    {
        _trigger._actionTriggerEnter = OnBulletTriggerEnter;
    }
#endregion
    
    

#region _BaseFireThing_
    public override void OnThingCreate(IFirePoint fp)
    {
        TagLog.Log(LogIndex.Bullet, "OnBulletCreate");
        base.OnThingCreate(fp);
        _nCurLifeCount = _nLifeCount;
        // 其他
        EventDelegate.Execute(_lstThingCreate);        
    }

    // Bullet需要销毁时的处理
    // 在Bullet需要销毁时进行调用    
    // 注意是需要销毁时的处理，不是销毁后的处理。一般是在Bullet碰撞了什么需要显示销毁特效时调用
    public override void OnThingDestroy()
    {
        base.OnThingDestroy();
        TagLog.Log(LogIndex.Bullet, "OnBulletDestoy");
        PlayBulletDestroyEffect();
        ObjectPoolController.Destroy(gameObject);
        // 其他
        EventDelegate.Execute(_lstThingDestroy);
    }
#endregion    

    protected virtual void PlayBulletDestroyEffect()
    {
        if (_effectDestroy._strName.Length == 0)
        {
            return;
        }
        _effectDestroy._pos = transform.position;
        ShotEffect.Instance.Shot(_effectDestroy);
    }

    void OnBulletTriggerEnter(GameObject go)
    {
        TagLog.Log(LogIndex.Bullet, "OntriggerEnter:" + go.name);
        BaseLifeCom LifeCom = go.GetComponent<BaseLifeCom>();
        if (LifeCom != null)
        {
            LifeCom.AddValue(-_nHurtValue);
            if (_nCurLifeCount > 0)
            {
                _nCurLifeCount--;
                if (_nCurLifeCount == 0)
                {
                    OnThingDestroy();
                }
            }            
        }
    }
}

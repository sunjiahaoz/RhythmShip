using UnityEngine;
using System.Collections;
using sunjiahaoz;
public class BaseBullet : BaseFireThing{    
    public ColliderTrigger _trigger;    
    public EffectParam _effectDestroy;
    public int _nHurtValue = 1;

#region _Mono_
    void Awake()
    {
        _trigger._actionTriggerEnter = OnBulletTriggerEnter;
    }
#endregion
    
    // 作为Bullet的初始化函数
    // 在创建Bullet的时候进行调用
    public virtual void OnBulletCreate(BaseFirePoint fp)
    {
        TagLog.Log(LogIndex.Bullet, "OnBulletCreate");
    }

    // Bullet需要销毁时的处理
    // 在Bullet需要销毁时进行调用    
    // 注意是需要销毁时的处理，不是销毁后的处理。一般是在Bullet碰撞了什么需要显示销毁特效时调用
    public virtual void OnBulletDestoy()
    {
        TagLog.Log(LogIndex.Bullet, "OnBulletDestoy");
        PlayBulletDestroyEffect();
        ObjectPoolController.Destroy(gameObject);
    }

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
            OnBulletDestoy();
        }
    }
}

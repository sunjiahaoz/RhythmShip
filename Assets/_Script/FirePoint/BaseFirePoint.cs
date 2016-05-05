using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum FireThingType
{
    Bullet,
    Ship,
    SpaceItem,
}

public class IFirePoint : MonoBehaviour
{
    public virtual void Fire() { }
    public virtual Vector3 GetDir() { return Vector3.zero; }
}

public class BaseFireThing : MonoBehaviour
{    
    //public virtual void OnThingCreate() { }
    public virtual void OnThingCreate(IFirePoint pt) { }
    public virtual void OnThingDestroy() { }
}

public class BaseFirePoint : IFirePoint
{
    public BaseFireThing _prefabFireThing;
    [Header("pos自动赋值")]    
    public EffectParam _shootEffect;                    // 创建特效
    public bool _bCreateBulletAfterAnim = false;   // 先播放动画，动画完成后再创建对象
    public Transform _trFirePointBodyPos;           // 身体位置，与trCreatePos可以得出朝向
    public Transform _trCreatePos;                      // 创建出的对象的初始位置

    #region _IFirePoint_
    public override void Fire()
    {
        if (_bCreateBulletAfterAnim)
        {
            FireEffect(GetCreatePos(), true);
        }
        else
        {
            CreateObject(GetCreatePos());
            FireEffect(GetCreatePos());
        }
    }

    // 获得炮筒朝向
    public override Vector3 GetDir()
    {
        return (GetCreatePos() - _trFirePointBodyPos.position).normalized;
    }
    #endregion

    protected virtual void Awake()
    {
        if (_trFirePointBodyPos == null)
        {
            _trFirePointBodyPos = transform;
        }
        if (_trCreatePos == null)
        {
            _trCreatePos = transform;
        }
    }

    // 创建
    protected virtual void CreateObject(Vector3 createPos, System.Action<BaseFireThing> afterCreate = null)
    {
        GameObject go = ObjectPoolController.Instantiate(_prefabFireThing.gameObject, createPos, Quaternion.identity);
        if (!go.activeInHierarchy)
        {
            go.SetActive(true);
        }
        BaseFireThing thing = go.GetComponent<BaseFireThing>();
        thing.OnThingCreate(this);

        if (afterCreate != null)
        {
            afterCreate(thing);
        }
    }

    // 特效
    protected virtual void FireEffect(Vector3 effectPos, bool bCreateBulletAfterEffect = false)
    {
        if (_shootEffect._strName.Length == 0)
        {
            if (bCreateBulletAfterEffect)
            {
                CreateObject(effectPos);
            }
            return;
        }
        System.Action actionComplete = null;
        if (bCreateBulletAfterEffect)
        {
            actionComplete = () =>
            {
                CreateObject(effectPos);
            };
        }

        _shootEffect._pos = effectPos;
        ShotEffect.Instance.Shot(_shootEffect, actionComplete);
    }

    protected virtual Vector3 GetCreatePos()
    {
        return _trCreatePos.position;
    }
}
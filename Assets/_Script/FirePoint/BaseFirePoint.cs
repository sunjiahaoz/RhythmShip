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
    public virtual Vector3 GetDir() 
    {
        sunjiahaoz.TagLog.LogWarning(LogIndex.FirePoint, "GetDir 获得的是默认的zero!!!", this);
        return Vector3.zero; 
    }
}

public class BaseFireThing : MonoBehaviour
{
    public bool _bLockZ = false;
    //public virtual void OnThingCreate() { }
    public virtual void OnThingCreate(IFirePoint pt) 
    { 
        if (_bLockZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
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
    public Transform _trDirPos; // bodyPos与dirPos决定方向，如果为null则使用createPos

    public ComGetCreatePos _comGetCreatePos;

    #region _IFirePoint_
    public override void Fire()
    {
        Vector3 pos = GetCreatePos();
        if (_bCreateBulletAfterAnim)
        {
            FireEffect(pos, true);
        }
        else
        {
            CreateObject(pos);
            FireEffect(pos);
        }
    }

    // 获得炮筒朝向
    public override Vector3 GetDir()
    {
        return (_trDirPos.position - _trFirePointBodyPos.position).normalized;
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
        if (_trDirPos == null)
        {
            _trDirPos = _trCreatePos;
        }
    }

    // 创建
    protected virtual void CreateObject(Vector3 createPos, System.Action<BaseFireThing> afterCreate = null)
    {
        if (_prefabFireThing == null)
        {
            return;
        }

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
        if (_comGetCreatePos != null)
        {
            return _comGetCreatePos.GetNextPos();
        }
        return _trCreatePos.position;
    }
}
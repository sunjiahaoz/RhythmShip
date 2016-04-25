/*
ShipEventCom_CreateShip
By: @sunjiahaoz, 2016-4-25

创建一个东西
 * ShipEventCom_ 是ship的事件com,一般是通过Ship的createEvent,destroyEvent进行触发。也可以进行自定义的事件监听进行处理
*/
using UnityEngine;
using System.Collections;

public class ShipEventCom_CreateShip : MonoBehaviour
{
    public BaseFireThing _prefabThing;
    //public bool _bCreateAfterEffect = false;
    public EffectParam _appearEffect;    

    // 实现此接口
    public virtual void CreateShip()
    {
        if (_appearEffect._strName.Length > 0)
        {
            _appearEffect._pos = transform.position;
            //if (_bCreateAfterEffect)
            //{
            //    ShotEffect.Instance.Shot(_appearEffect, () => { InstanceObj(); });
            //    return;
            //}
            //else
            {
                ShotEffect.Instance.Shot(_appearEffect);
            }
        }
        InstanceObj();
    }   
 
    void InstanceObj()
    {
        BaseFireThing thing = ObjectPoolController.Instantiate(_prefabThing.gameObject, transform.position, Quaternion.identity).GetComponent<BaseFireThing>();
        thing.OnThingCreate();        
    }
}

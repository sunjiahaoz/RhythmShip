using UnityEngine;
using System.Collections;

public class CursorBullet : MonoBehaviour {
    ColliderTrigger _colliderTrigger;

    void Awake()
    {
        _colliderTrigger = GetComponentInChildren<ColliderTrigger>();
        _colliderTrigger._actionTriggerEnter += OnThingEnter;        
    }

    void OnDestroy()
    {
        _colliderTrigger._actionTriggerEnter -= OnThingEnter;        
    }

    void OnThingEnter(GameObject go)
    {
        BaseLifeCom lifeCom = go.GetComponent<BaseLifeCom>();
        if (lifeCom != null)
        {
            lifeCom.AddValue(-1);
        }
    }
}

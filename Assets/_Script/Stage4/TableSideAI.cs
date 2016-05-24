using UnityEngine;
using System.Collections;

public class TableSideAI : MonoBehaviour {
    public EffectParam _hitEffect;    
    void OnCollisionEnter(Collision collision)
    {
        if (_hitEffect._strName.Length > 0)
        {
            _hitEffect._pos = collision.contacts[0].point;
            ShotEffect.Instance.Shot(_hitEffect);
        }            
        //if (collision.transform.gameObject.CompareTag(GloabalSet.Tag_Ball))
        //{
            
        //}
    }
}

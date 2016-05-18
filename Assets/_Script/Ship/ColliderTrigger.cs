using UnityEngine;
using System.Collections;

public class ColliderTrigger : MonoBehaviour {    
    public System.Action<GameObject> _actionTriggerEnter;
    public System.Action<GameObject> _actionTriggerStay;
    public System.Action<GameObject> _actionTriggerExit;

    bool _bEnable = true;
    public void SetEnable(bool bEnable)
    {
        _bEnable = bEnable;
    }


    void OnTriggerEnter(Collider other)
    {
        if (!_bEnable)
        {
            return;
        }

        if (_actionTriggerEnter != null)
        {
            _actionTriggerEnter(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!_bEnable)
        {
            return;
        }
        if (_actionTriggerExit != null)
        {
            _actionTriggerExit(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!_bEnable)
        {
            return;
        }

        if (_actionTriggerStay != null)
        {
            _actionTriggerStay(other.gameObject);
        }
    }
	
}

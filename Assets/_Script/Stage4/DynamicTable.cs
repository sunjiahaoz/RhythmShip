using UnityEngine;
using System.Collections;

public class DynamicTable : MonoBehaviour {
    public Transform _trBody;
    public Transform _trMaxPos;    
    public float _fExpandSmoothSpeed;    

    void Update()
    {
        Vector3 pos = _trBody.localPosition;        
        pos.y = Mathf.SmoothStep(pos.y, 0f, Time.deltaTime * _fExpandSmoothSpeed);
        _trBody.localPosition = pos;
    }

    public float _fAdd = -30;    
    public void AddForce()
    {
        float fAddDest = _fAdd;
        if (_fAdd < 0)
        {
            if (_trBody.localPosition.y < _trMaxPos.localPosition.y)
            {
                _trBody.localPosition = _trMaxPos.localPosition;
            }
        }
        else
        {
            if (_trBody.localPosition.y > _trMaxPos.localPosition.y)
            {
                _trBody.localPosition = _trMaxPos.localPosition;
            }
        }
        _trBody.Translate(0, fAddDest, 0);
    }
}

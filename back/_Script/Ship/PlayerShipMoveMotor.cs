using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class PlayerShipMoveMotor : MonoBehaviour 
{
    public Transform _root;
    public float _fLerpSpeed = 50f;
    void Awake()
    {
        if (_root == null)
        {
            _root = transform;
        }
    }

    Vector3 _posTarget = Vector3.zero;
    float _fProgress = 0;
    void FixedUpdate()
    {
        _posTarget = ToolsUseful.GetWorldPos(Input.mousePosition);
        _fProgress += Time.deltaTime / _fLerpSpeed;
        if(_fProgress > 1) 
            _fProgress = 1;
        _root.position = Vector3.Lerp(_root.position, _posTarget, _fProgress);
    }
}

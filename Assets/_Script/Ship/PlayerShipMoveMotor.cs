using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class PlayerShipMoveMotor : MonoBehaviour 
{
    public Transform _root;
    public Transform _body;
    //public float _fMoveRotateAngle = 15;
    public float _fLerpSpeed = 50f;

    //Vector3 _angleToLeft = Vector3.zero;
    //Vector3 _angleToRight = Vector3.zero;
    void Awake()
    {
        if (_root == null)
        {
            _root = transform;
        }
        //_posLastPos = _root.position;
        //_angleToLeft = new Vector3(0, -_fMoveRotateAngle, 0);
        //_angleToRight = new Vector3(0, _fMoveRotateAngle, 0);
    }

    Vector3 _posTarget = Vector3.zero;
    //Vector3 _posLastPos = Vector3.zero;
    Vector3 _vel = Vector3.one;
    void FixedUpdate()
    {
        _posTarget = ToolsUseful.GetWorldPos(Input.mousePosition);
        _root.position = Vector3.SmoothDamp(_root.position, _posTarget, ref _vel, _fLerpSpeed);
        //if (_posLastPos.x > _root.position.x)
        //{
        //    TagLog.Log(LogIndex.Ship, "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");            
        //}
        //else if (_posLastPos.x < _root.position.x)
        //{
        //    TagLog.Log(LogIndex.Ship, ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>");            
        //}
        //else
        //{
        //    TagLog.Log(LogIndex.Ship, "===========================");            
        //}
        //_posLastPos = _root.position;
    }
}

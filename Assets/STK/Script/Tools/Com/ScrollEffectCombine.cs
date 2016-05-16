/*
*ScrollEffectCombine
*by sunjiahaoz 2016-5-16
*
*卷轴效果，pos,Angle,Scale的组合版
*/
using UnityEngine;
using System.Collections;

namespace sunjiahaoz
{    
    public class ScrollEffectCombine : MonoBehaviour
    {
        public Transform _trTarget;
        public Vector3 _posRadio = Vector3.one * 0.8f;
        public Vector3 _angleRadio = Vector3.one * 0.8f;
        public Vector3 _scaleRadio = Vector3.one * 0.8f;

        // 目标的初始化
        Vector3 _v3TargetInitPos = Vector3.zero;
        Vector3 _v3TargetInitScale = Vector3.zero;
        Vector3 _v3TargetInitAngle = Vector3.zero;
        // 自己的初始化
        Vector3 _v3MineInitPos = Vector3.zero;
        Vector3 _v3MineInitScale = Vector3.zero;
        Vector3 _v3MineInitAngle = Vector3.zero;

        void Start()
        {
            _v3TargetInitPos = _trTarget.position;
            _v3MineInitPos = transform.position;
            _v3TargetInitScale = _trTarget.localScale;
            _v3MineInitScale = transform.localScale;
            _v3TargetInitAngle = _trTarget.localEulerAngles;
            _v3MineInitAngle = transform.localEulerAngles;
        }
        // 用于计算的临时变量
        Vector3 _v3Generate = Vector3.zero;
        Vector3 _v3New = Vector3.zero;
        void Update()
        {
            if (_trTarget == null)
            {
                return;
            }
            _v3Generate = _trTarget.position - _v3TargetInitPos;
            _v3New.x = _v3Generate.x * _posRadio.x;
            _v3New.y = _v3Generate.y * _posRadio.y;
            _v3New.z = _v3Generate.z * _posRadio.z;
            transform.position = _v3MineInitPos  + _v3New;

            _v3Generate = _trTarget.localScale - _v3TargetInitScale;
            _v3New.x = _v3Generate.x * _scaleRadio.x;
            _v3New.y = _v3Generate.y * _scaleRadio.y;
            _v3New.z = _v3Generate.z * _scaleRadio.z;
            transform.localScale = _v3MineInitScale + _v3New;

            _v3Generate = _trTarget.localEulerAngles - _v3TargetInitAngle;            
            _v3New.x = _v3Generate.x * _angleRadio.x;
            _v3New.y = _v3Generate.y * _angleRadio.y;
            _v3New.z = _v3Generate.z * _angleRadio.z;
            transform.localEulerAngles = _v3MineInitAngle + _v3New;
        }
    }
}

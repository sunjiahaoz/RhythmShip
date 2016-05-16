using UnityEngine;
using System.Collections;

namespace sunjiahaoz
{

    public class ScrollEffectPoint_Angle : MonoBehaviour
    {
        public Transform _trTarget;
        public float _fRadio = 0.8f;

        // 目标的初始化位置
        Vector3 _v3TargetInit = Vector3.zero;
        // 自己的初始化位置
        Vector3 _v3MineInit = Vector3.zero;
        void Start()
        {
            _v3TargetInit = _trTarget.localEulerAngles;
            _v3MineInit = transform.localEulerAngles;
        }

        // 用于计算的临时变量
        Vector3 _v3Generate = Vector3.zero;
        Vector3 _v3New = Vector3.zero;
        void Update()
        {
            _v3Generate = _trTarget.localEulerAngles - _v3TargetInit;
            _v3New = _v3Generate * _fRadio;            
            transform.localEulerAngles = _v3MineInit + _v3New;
        }

    }
}

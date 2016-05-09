using UnityEngine;
using System.Collections;

/// <summary>
/// 卷轴效果基本点
/// 此处卷轴效果是不断检测一个目标的位置，当目标移动了，则自己也按照一定的百分比移动
/// </summary>
namespace sunjiahaoz
{
    public class ScrollEffectPoint : MonoBehaviour
    {
        public Transform _trTarget;
        public float _perX = 0.8f;
        public float _perY = 0.8f;

        // 目标的初始化位置
        Vector3 _v3TargetInitPos = Vector3.zero;
        // 自己的初始化位置
        Vector3 _v3MineInitPos = Vector3.zero;
        void Start()
        {
            _v3TargetInitPos = _trTarget.position;
            _v3MineInitPos = transform.position;
        }

        // 用于计算的临时变量
        Vector3 _v3Generate = Vector3.zero;
        Vector3 _v3New = Vector3.zero;
        void Update()
        {
            _v3Generate = _trTarget.position - _v3TargetInitPos;
            _v3New.x = _v3Generate.x * _perX;
            _v3New.y = _v3Generate.y * _perY;
            _v3New.z = _v3MineInitPos.z;
            transform.position = _v3MineInitPos + _v3New;
        }
    }
}

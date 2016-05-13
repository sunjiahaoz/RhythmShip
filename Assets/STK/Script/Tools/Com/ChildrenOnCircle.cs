/*
DelayFollowRotate
By: @sunjiahaoz, 2016-5-10

让子对象 以本对象为中心生成的圆 遍布
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dest.Math;

namespace sunjiahaoz
{    
    public class ChildrenOnCircle : MonoBehaviour
    {
        List<Transform>_rotatePoint;

        public float _fRadius = 1;
        public int _nGenerateCirclePoints = 60;
        public int _nPointInterval = 4;

        public bool _bDebug = false;
        

        void Update()
        {
            if (_bDebug)
            {
                ReGeneratePos();
            }
        }

        Circle2 _cc;
        [ContextMenu("重新生成")]
        void ReGeneratePos()
        {            
            _rotatePoint = ToolsUseful.GetComponentsInChildren<Transform>(transform, false, true);

            Vector3[] poses = UtilityTool.PointsOnCircle(transform.position, _fRadius, _rotatePoint.Count, _nPointInterval, _nGenerateCirclePoints);
            if (poses != null)
            {
                for (int i = 0; i < _rotatePoint.Count; ++i)
                {
                    _rotatePoint[i].position = poses[i];
                }
            }
            
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _fRadius);
        }
    }
}

/*
IKAnchor
By: @sunjiahaoz, 2016-5-18

用此对象的SetPosition可以让_trParent进行相对移动
*/
using UnityEngine;
using System.Collections;

namespace sunjiahaoz
{
    public class IKAnchor : MonoBehaviour
    {
        public Transform _trParent;
        void Awake()
        {
            if (_trParent == null)
            {
                _trParent = transform.parent;
                if (_trParent == null)
                {
                    _trParent = transform;
                }
            }
        }

        public void SetPosition(Vector3 pos)
        {   
            _trParent.position -= transform.position - pos;
        }

        public void SetPosition(Transform targetPos)
        {
            SetPosition(targetPos.position);
        }
    }
}

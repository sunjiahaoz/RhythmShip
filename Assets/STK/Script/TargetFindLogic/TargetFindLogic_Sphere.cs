using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace sunjiahaoz
{
    public class TargetFindLogicPrams_Sphere : TargetFindLogicParams
    {
        public Vector3 _posCenter = Vector3.zero;    // 寻找位置中心
        public float _fRadius = 0;  // 半径

        public System.Func<GameObject, GameObject> _funcFilter = null;    // 过滤函数, 参数为找到的go,返回值为处理有的go,如果没有过滤成功可以返回null,过滤后可能会被处理为其他go
        public System.Comparison<GameObject> _sortFunc = null;      // 排序函数


        public void SetParam(Vector3 posCenter, float fRadius, 
            System.Func<GameObject, GameObject> funcFilter = null, 
            System.Comparison<GameObject> sortFunc = null, 
            int nCheckLayer = -1, int nLimitMaxCount = ToolsUseful.MaxIntCanUse)
        {
            _posCenter = posCenter;
            _fRadius = fRadius;
            _funcFilter = funcFilter;
            _sortFunc = sortFunc;
            _nLayerMask = nCheckLayer;
            _nLimitMaxCount = nLimitMaxCount;
        }

        public override void Clear()
        {
            base.Clear();
            _posCenter = Vector3.zero;
            _fRadius = 0;
            _funcFilter = null;
            _sortFunc = null;
        }
    }
    public class TargetFindLogic_Sphere : TargetFindLogic_None
    {
        List<GameObject> _collidFilted = new List<GameObject>();
        Collider[] _colliders = null;

        public override GameObject[] Find(TargetFindLogicParams data = null)
        {
            _collidFilted.Clear();
            _colliders = null;

            if (data == null)
            {
                return null;
            }

            TargetFindLogicPrams_Sphere param = data as TargetFindLogicPrams_Sphere;

            _colliders = Physics.OverlapSphere(param._posCenter, param._fRadius, param._nLayerMask);
            if (_colliders.Length == 0)
            {
                return null;
            }
            // 过滤
            for (int i = 0; i < _colliders.Length; ++i)
            {
                GameObject go = _colliders[i].gameObject;
                // 如果需要过滤
                if (param._funcFilter != null)
                {
                    go = param._funcFilter(_colliders[i].gameObject);                    
                }

                // 超出最大数量限制
                if (i >= param._nLimitMaxCount)
                {
                    break;
                }
                else
                {
                    if (go != null)
                    {
                        _collidFilted.Add(go);
                    }
                }
            }

            if (_collidFilted.Count == 0)
            {
                return null;
            }
            else
            {
                // 排序
                if (param._sortFunc != null)
                {
                    _collidFilted.Sort(param._sortFunc);
                }
                return _collidFilted.ToArray();
            }
        }
    }
}

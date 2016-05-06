using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
namespace sunjiahaoz
{
    public class ObjectAnim_Scroll : ObjectAnimBase
    {
        public Transform _trGoalPos;
        [Header("进入此范围表示到达目标点")]
        public float _fGoalRadius = 10f;
        public List<OAElem_ScrollItem> _lstItems;
        [Header("越大越快")]
        public float _fSpeed = 100;

        bool _bIsRunning = false;
        Vector3 _dir = Vector3.zero;        
        public override void Run()
        {
            if (_lstItems == null
                || _lstItems.Count == 0)
            {
                return;
            }

            _dir = (_trGoalPos.position - _lstItems[0].transform.position).normalized;            
            _bIsRunning = true;
        }

        void RemoveHeadItem()
        {
            OAElem_ScrollItem firstItem = _lstItems[0];
            firstItem.FollowScrollItem(_lstItems[_lstItems.Count - 1]);
            // 从头部移除
            _lstItems.Remove(firstItem);
            // 再添加到尾部
            _lstItems.Add(firstItem);
        }
        
        void Update()
        {
            if (_bIsRunning)
            {
                for (int i = 0; i < _lstItems.Count; ++i )
                {
                    _lstItems[i].transform.Translate(_dir * Time.deltaTime * _fSpeed);
                }

                if (_lstItems[0].CheckGoal(_trGoalPos.position))
                {
                    RemoveHeadItem();
                }
            }            
        }

        void OnDrawGizmos()
        {
            // 目标点范围
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_trGoalPos.position, _fGoalRadius);
        }

        #region _Menu_
        [ContextMenu("调整")]
        void ResetLstScrollItems()
        {
            if (_lstItems == null
                || _lstItems.Count == 0)
            {
                return;
            }
            for (int i = 1; i < _lstItems.Count; ++i )
            {
                _lstItems[i].FollowScrollItem(_lstItems[i - 1]);
            }
        }
        #endregion

    }
}

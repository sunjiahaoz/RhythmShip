using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if _USENGUI_
namespace sunjiahaoz.NGUIElem
{
    [RequireComponent(typeof(UIGrid))]
    public class UIControl_List : MonoBehaviour
    {
        protected UIGrid _comGrid;
        public UIGrid comGrid
        {
            get
            {
                if (_comGrid == null)
                {
                    _comGrid = GetComponent<UIGrid>();
                }
                return _comGrid;
            }
        }
        //public GameObject _prefabItem;

        protected List<UIControl_ListItemBase> _lstItems = new List<UIControl_ListItemBase>();
        public List<UIControl_ListItemBase> Items
        {
            get { return _lstItems; }
        }

        protected virtual void Awake()
        {
            _comGrid = GetComponent<UIGrid>();
        }

        /// <summary>
        /// 根据prefab创建一个新的item，并添加到列表中
        /// </summary>
        /// <param name="prefab">要实例化的预制体</param>
        /// <returns>实例化的item</returns>
        public virtual UIControl_ListItemBase CreateAndAddItem(UIControl_ListItemBase prefab, System.Action<UIControl_ListItemBase> dosth = null)
        {
            GameObject clone = NGUITools.AddChild(gameObject, prefab.gameObject);
            if (!clone.activeInHierarchy)
            {
                clone.gameObject.SetActive(true);
            }
            UIControl_ListItemBase item = clone.GetComponent<UIControl_ListItemBase>();
            AddItem(item);
            if (dosth != null)
            {
                dosth(item);
            }
            return item;
        }
        /// <summary>
        /// 添加一项
        /// </summary>
        /// <param name="item"></param>
        /// <returns>返回该项的索引</returns>
        public virtual int AddItem(UIControl_ListItemBase item)
        {
            //item.transform.parent = _comGrid.transform;
            comGrid.AddChild(item.transform);
            item.transform.localScale = Vector3.one;
            if (!item.gameObject.activeInHierarchy)
            {
                item.gameObject.SetActive(true);
            }
            _lstItems.Add(item);
            ReSort();
            return _lstItems.Count - 1;
        }

        /// <summary>
        /// 按对象移除
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(UIControl_ListItemBase item)
        {
            for (int i = 0; i < _lstItems.Count; ++i)
            {
                if (_lstItems[i] == item)
                {
                    RemoveItemAt(i);
                    break;
                }
            }
        }
        /// <summary>
        /// 按索引移除
        /// </summary>
        /// <param name="nIndex"></param>
        public virtual void RemoveItemAt(int nIndex)
        {
            if (nIndex < 0
                || nIndex >= _lstItems.Count)
            {
                return;
            }

            if (_lstItems[nIndex] == null)
            {
                Debug.LogWarning("RemoveItemAt:" + nIndex + " gameObject is null !");
                _lstItems.RemoveAt(nIndex);
                return;
            }

            try
            {
                _comGrid.RemoveChild(_lstItems[nIndex].transform);
                _lstItems[nIndex].transform.parent = null;
                Destroy(_lstItems[nIndex].gameObject);
                _lstItems.RemoveAt(nIndex);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("RemoveItemAt Exception: " + ex.Message);
            }


            ReSort();
        }

        /// <summary>
        /// 获得指定索引的道具
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        public virtual UIControl_ListItemBase GetItemAt(int nIndex)
        {
            if (nIndex < 0
                || nIndex >= _lstItems.Count)
            {
                return null;
            }
            return _lstItems[nIndex];
        }

        /// <summary>
        /// 清空
        /// </summary>
        public virtual void ResetContent()
        {
            for (int i = 0; i < _lstItems.Count; ++i)
            {
                if (_lstItems[i] != null)
                {
                    _lstItems[i].transform.parent = null;
                    Destroy(_lstItems[i].gameObject);
                }
            }
            _lstItems.Clear();
        }

        public virtual void ReSort()
        {
            //_comGrid.Reposition();
            comGrid.repositionNow = true;
        }

        /// <summary>
        /// 自定义排序
        /// </summary>
        /// <param name="customSortFunc"></param>
        public virtual void ReSortCustom(System.Comparison<Transform> customSortFunc)
        {
            comGrid.sorting = UIGrid.Sorting.Custom;
            comGrid.onCustomSort = customSortFunc;

            comGrid.repositionNow = true;
        }

        /// <summary>
        /// 元素数量
        /// </summary>
        /// <returns></returns>
        public virtual int ElemCount()
        {
            return _lstItems.Count;
        }

        public virtual float GetMaxWidth()
        {
            float fMax = 0;
            float fTotal = 0;

            for (int i = 0; i < _lstItems.Count; ++i)
            {
                if (fMax < _lstItems[i].GetWidth())
                {
                    fMax = _lstItems[i].GetWidth();
                }
                fTotal += _lstItems[i].GetWidth();
            }

            if (comGrid.arrangement == UIGrid.Arrangement.Horizontal)
            {
                fTotal = _lstItems.Count * comGrid.cellWidth;
            }
            else if (comGrid.arrangement == UIGrid.Arrangement.Vertical)
            {
                return fMax;
            }

            return fTotal;
        }

        public virtual float GetMaxHeight()
        {
            float fMax = 0;
            float fTotal = 0;

            for (int i = 0; i < _lstItems.Count; ++i)
            {
                if (fMax < _lstItems[i].GetHeight())
                {
                    fMax = _lstItems[i].GetHeight();
                }
                fTotal += _lstItems[i].GetHeight();
            }

            // 间隔
            fTotal += (_lstItems.Count - 1) * comGrid.cellHeight;

            //if (_comGrid.arrangement == UIGrid.Arrangement.Horizontal)
            //{
            //    return fMax;
            //}
            //else if (_comGrid.arrangement == UIGrid.Arrangement.Vertical)
            //{   
            //    return _lstItems.Count * _comGrid.cellHeight;
            //}

            return fTotal;
        }
    }
}
#endif

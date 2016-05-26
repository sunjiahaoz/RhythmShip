using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if _USENGUI_
namespace sunjiahaoz
{
    [RequireComponent(typeof(UITable))]
    public class UIControl_ListTable : MonoBehaviour
    {

        UITable _comTable;
        public UITable comTable
        {
            get { return _comTable; }
        }
        //public GameObject _prefabItem;

        List<UIControl_ListItemBase> _lstItems = new List<UIControl_ListItemBase>();
        public List<UIControl_ListItemBase> Items
        {
            get { return _lstItems; }
        }

        void Awake()
        {
            _comTable = GetComponent<UITable>();
        }

        /// <summary>
        /// 根据prefab创建一个新的item，并添加到列表中
        /// </summary>
        /// <param name="prefab">要实例化的预制体</param>
        /// <returns>实例化的item</returns>
        public UIControl_ListItemBase CreateAndAddItem(UIControl_ListItemBase prefab, System.Action<UIControl_ListItemBase> dosth = null)
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
        public int AddItem(UIControl_ListItemBase item)
        {
            //item.transform.parent = _comGrid.transform;
            //_comTable.AddChild(item.transform);

            item.transform.parent = _comTable.transform;
            item.transform.localScale = Vector3.one;
            _lstItems.Add(item);
            ReSort();
            return _lstItems.Count - 1;
        }

        /// <summary>
        /// 按对象移除
        /// </summary>
        /// <param name="item"></param>
        public void Remove(UIControl_ListItemBase item)
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
        public void RemoveItemAt(int nIndex)
        {
            if (nIndex < 0
                || nIndex >= _lstItems.Count)
            {
                return;
            }

            _comTable.GetChildList().Remove(_lstItems[nIndex].transform);
            //_comTable.RemoveChild(_lstItems[nIndex].transform);
            _lstItems[nIndex].transform.parent = null;
            Destroy(_lstItems[nIndex].gameObject);
            _lstItems.RemoveAt(nIndex);

            ReSort();
        }

        /// <summary>
        /// 获得指定索引的道具
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        public UIControl_ListItemBase GetItemAt(int nIndex)
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
        public void ResetContent()
        {
            for (int i = 0; i < _lstItems.Count; ++i)
            {
                _lstItems[i].transform.parent = null;
                Destroy(_lstItems[i].gameObject);
            }
            _lstItems.Clear();
        }

        public void ReSort()
        {
            //_comGrid.Reposition();
            //_comGrid.repositionNow = true;
            _comTable.repositionNow = true;
        }

        /// <summary>
        /// 自定义排序
        /// </summary>
        /// <param name="customSortFunc"></param>
        public void ReSortCustom(System.Comparison<Transform> customSortFunc)
        {
            _comTable.sorting = UITable.Sorting.Custom;
            _comTable.onCustomSort = customSortFunc;
            _comTable.repositionNow = true;
        }

        /// <summary>
        /// 元素数量
        /// </summary>
        /// <returns></returns>
        public int ElemCount()
        {
            return _lstItems.Count;
        }

        public float GetMaxWidth()
        {
            //float fMax = 0;
            //float fTotal = 0;

            //for (int i = 0; i < _lstItems.Count; ++i)
            //{
            //    if (fMax < _lstItems[i].GetWidth())
            //    {
            //        fMax = _lstItems[i].GetWidth();
            //    }
            //    fTotal += _lstItems[i].GetWidth();
            //}

            //if (_comGrid.arrangement == UIGrid.Arrangement.Horizontal)
            //{
            //    fTotal = _lstItems.Count * _comGrid.cellWidth;
            //}
            //else if (_comGrid.arrangement == UIGrid.Arrangement.Vertical)
            //{
            //    return fMax;
            //}

            //return fTotal;
            throw new System.NotImplementedException();
        }

        public float GetMaxHeight()
        {
            //float fMax = 0;
            //float fTotal = 0;

            //for (int i = 0; i < _lstItems.Count; ++i)
            //{
            //    if (fMax < _lstItems[i].GetWidth())
            //    {
            //        fMax = _lstItems[i].GetWidth();
            //    }
            //    fTotal += _lstItems[i].GetWidth();
            //}

            //if (_comGrid.arrangement == UIGrid.Arrangement.Horizontal)
            //{
            //    return fMax;
            //}
            //else if (_comGrid.arrangement == UIGrid.Arrangement.Vertical)
            //{
            //    return _lstItems.Count * _comGrid.cellHeight;
            //}

            //return fTotal;
            throw new System.NotImplementedException();
        }

    }
}
#endif
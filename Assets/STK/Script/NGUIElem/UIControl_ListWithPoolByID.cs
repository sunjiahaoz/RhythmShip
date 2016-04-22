using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if _USENGUI_
namespace sunjiahaoz.NGUIElem
{

    public class UIControl_ListWithPoolByID : UIControl_List
    {
        // 可以设定最多保存多少个对象，超过的就删掉
        // 如果为0表示没有限制
        public int _nPoolMax = 0;

        // 用来保存池子里的对象,会自动生成的
        GameObject _goPoolParent;
        // IDInPool -> PrefabStack
        Dictionary<int, Stack<UIControl_ListItemBase>> _dictPool = new Dictionary<int, Stack<UIControl_ListItemBase>>();

        protected override void Awake()
        {
            base.Awake();
            _goPoolParent = new GameObject();
            _goPoolParent.transform.parent = transform.parent;
            _goPoolParent.name = "_ListWithPoolParent_";
        }


        Stack<UIControl_ListItemBase> GetStackPool(int nIdInPool)
        {
            Stack<UIControl_ListItemBase> stackPool = null;
            if (!_dictPool.ContainsKey(nIdInPool))
            {
                _dictPool.Add(nIdInPool, new Stack<UIControl_ListItemBase>());
            }
            stackPool = _dictPool[nIdInPool];
            return stackPool;
        }
        /// <summary>
        /// 虽然有个prefab参数，貌似可以填充任意的ItemBase，
        /// 但实际每次只能传入同一个prefab,否则池子可不会区分你两个prefab的不同
        /// 如果传入的prefab不同，出现什么后果我可不管
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="dosth"></param>
        /// <returns></returns>
        public override UIControl_ListItemBase CreateAndAddItem(UIControl_ListItemBase prefab, System.Action<UIControl_ListItemBase> dosth = null)
        {
            Stack<UIControl_ListItemBase> stackPool = GetStackPool(prefab.IDInPool);

            // 如果池子里面有
            if (stackPool.Count > 0)
            {
                UIControl_ListItemBase item = stackPool.Pop();
                item.gameObject.SetActive(true);
                AddItem(item);
                if (dosth != null)
                {
                    dosth(item);
                }
                return item;
            }
            // 否则就创建个新的
            return base.CreateAndAddItem(prefab, dosth);
        }

        public override void ResetContent()
        {
            for (int i = 0; i < _lstItems.Count; ++i)
            {
                RemoveToPool(_lstItems[i]);
            }
            _lstItems.Clear();
        }

        public override void RemoveItemAt(int nIndex)
        {
            if (nIndex < 0
                || nIndex >= _lstItems.Count)
            {
                return;
            }
            RemoveToPool(_lstItems[nIndex]);
            _lstItems.RemoveAt(nIndex);

            ReSort();
        }

        void RemoveToPool(UIControl_ListItemBase item)
        {
            if (!_dictPool.ContainsKey(item.IDInPool))
            {
                Debug.LogWarning("<color=orange>[Warning]</color>---" + "will remove item is not contain idInpool!");
                return;
            }
            Stack<UIControl_ListItemBase> stackPool = GetStackPool(item.IDInPool);

            _comGrid.RemoveChild(item.transform);
            item.transform.parent = _goPoolParent.transform;
            item.gameObject.SetActive(false);
            // 如果超出了池子的限制，则删除
            if (_nPoolMax > 0
                && stackPool.Count == _nPoolMax)
            {
                Destroy(item.gameObject);
            }
            else
            {
                stackPool.Push(item);
            }
        }
    }
}
#endif
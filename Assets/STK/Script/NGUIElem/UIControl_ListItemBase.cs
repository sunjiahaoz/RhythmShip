using UnityEngine;
using System.Collections;

#if _USENGUI_
namespace sunjiahaoz.NGUIElem
{
    public class UIControl_ListItemBase : MonoBehaviour
    {
        protected int _nData = -1;

        // 使用withPool的list的时候可以用这个来区别相同的ItemBase但有个别差异的prefab池子
        [Tooltip("使用withPool的list的时候可以用这个来区别相同的ItemBase但有个别差异的prefab池子")]
        public int _nIDInPool = 0;
        public int IDInPool
        {
            get { return _nIDInPool; }
            set { _nIDInPool = value; }
        }

        public virtual void SetItemData(int nData)
        {
            _nData = nData;
        }

        public virtual int GetItemData()
        {
            return _nData;
        }

        public virtual float GetHeight()
        { return 0; }
        public virtual float GetWidth()
        { return 0; }
    }
}
#endif
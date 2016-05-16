/*
*DepthSortTool
*by sunjiahaoz 2016-5-16
*
*用来对子对象的depth进行排序
*/
using UnityEngine;
using System.Collections;
namespace sunjiahaoz
{
    public class DepthSortTool : MonoBehaviour
    {
        public int _nBaseDepth = 0;
        public bool _bDirIn = true; // 往屏幕里排序，还是往屏幕外方向排序

        void Awake()
        {
            Generate();
        }

        [ContextMenu("执行")]
        void Generate()
        {
            tk2dBaseSprite[] sprites = GetComponentsInChildren<tk2dBaseSprite>();
            for (int i = 0; i < sprites.Length; ++i )
            {
                sprites[i].SortingOrder = _nBaseDepth + (_bDirIn ? -i : i);
            }
        }
    }
}

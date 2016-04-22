using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace sunjiahaoz.UI
{
    // T 为窗口ID
    public interface IUIManager<T>
    {
        /// <summary>
        /// 加载某个UI资源
        /// </summary>
        /// <param name="frameId">资源对应的ID</param>
        /// <param name="bLoadAndShow">加载完是否立即进行显示</param>
        IFrame<T> LoadUI(T frameId, bool bLoadAndShow = false);
        // 显示一个窗口
        IFrame<T> ShowUI(T frameId);
        // 隐藏一个窗口
        void HideUI(T frameId);
        // 销毁一个窗口（注意隐藏并不是销毁，销毁包括了隐藏）
        void DestroyUI(T frameId);

        // 某窗口是否正在显示中
        //bool IsShowing(T frameId);
        // 某窗口是否已经加载了
        bool IsLoaded(T frameId);
    }
}

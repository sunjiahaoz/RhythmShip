using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if _USENGUI_
namespace sunjiahaoz
{
    /// <summary>
    /// 加点按钮的简单实现。就是按住按钮，每隔一段时间就触发一次事件。
    /// 此类下需要配合UIEventTrigger使用。
    /// </summary>
    [RequireComponent(typeof(UIEventTrigger))]
    public class UIControl_PressBtn : MonoBehaviour
    {

        UIEventTrigger _comEventTrigger;

        public List<EventDelegate> onEvent = new List<EventDelegate>();

        // 如果在执行的时候拖动了，就停止
        public bool _bStopWhenDragMove = false;
        // 两次执行的间隔时间（秒）
        public float _fInternalTime = 0.5f;
        // 最后两次执行的最短间隔时间（秒）
        public float _fMinInternalTime = 0.1f;
        // 如果一直按着，每次间隔时间缩短的时间（秒）
        public float _fTransformCutInternalOff = 0.1f;

        // 执行下一次剩余的时间
        float _fCurNextLeftTime = 0;
        // 当前间隔时间
        float _fCurInternalTime = 0;
        // 是否按下了
        bool _bIsPress = false;

        void Start()
        {
            _comEventTrigger = GetComponent<UIEventTrigger>();

            CheckAndAddTrigger(ref _comEventTrigger.onPress, "OnTouchPress");
            CheckAndAddTrigger(ref _comEventTrigger.onRelease, "OnTouchRelease");
            CheckAndAddTrigger(ref _comEventTrigger.onDragOut, "OnDragOverOut");
        }

        // 检测，只有没有的时候才添加，防止重复添加
        void CheckAndAddTrigger(ref List<EventDelegate> lstEvent, string strFuncName)
        {
            bool bHasFunc = false;
            for (int i = 0; i < lstEvent.Count; ++i)
            {
                if (lstEvent[i].methodName == strFuncName)
                {
                    bHasFunc = true;
                    break;
                }
            }
            if (!bHasFunc)
            {
                lstEvent.Add(new EventDelegate(this, strFuncName));
            }
        }

        // 按下
        public void OnTouchPress()
        {
            // 先执行一次
            ExcuteEvent();

            // 时间初始化
            _fCurNextLeftTime = _fInternalTime;
            _fCurInternalTime = _fInternalTime;

            _bIsPress = true;
        }

        // 移出
        public void OnDragOverOut()
        {
            if (_bStopWhenDragMove)
            {
                _bIsPress = false;
            }
        }

        // 释放
        public void OnTouchRelease()
        {
            _bIsPress = false;
        }


        void Update()
        {
            if (!_bIsPress)
            {
                return;
            }

            _fCurNextLeftTime -= Time.deltaTime;
            if (_fCurNextLeftTime <= 0)
            {
                ExcuteEvent();
                _fCurInternalTime = Mathf.Clamp(_fCurInternalTime - _fTransformCutInternalOff, _fMinInternalTime, _fCurInternalTime);
                _fCurNextLeftTime = _fCurInternalTime;
            }
        }

        void ExcuteEvent()
        {
            EventDelegate.Execute(onEvent);
        }
    }
}
#endif
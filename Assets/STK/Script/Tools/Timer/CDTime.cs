using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace sunjiahaoz
{
    public class ActionCD
    {        
        // CD 开始时间（时间点）
        float _fStartTime = 0;
        // CD时间（时间段）
        int _nCDTime = 0;  // ms 
        // CD 是否完成
        bool _bIsFinished = true;
        public bool IsFinished
        {
            get { return _bIsFinished; }
        }
        
        /// <summary>
        /// 开始CD
        /// </summary>
        /// <param name="nCDTime">CD时间,毫秒</param>
        public void StartCooldown(int nCDTime)
        {
            if (nCDTime == 0)
            {
                _bIsFinished = true;
                _nCDTime = 0;
                _fStartTime = 0.0f;
            }
            else
            {
                _fStartTime = Time.time;
                _nCDTime = nCDTime;
                _bIsFinished = false;
            }
        }
        /// <summary>
        /// 剩余多少毫秒
        /// </summary>
        /// <returns></returns>
        public int GetLeftMS()
        {
            int passedTime = (int)((Time.time - _fStartTime) * 1000);
            int leftTime = Mathf.Max(_nCDTime - passedTime, 0);
            return leftTime;
        }
        /// <summary>
        /// 剩余多少百分比
        /// </summary>
        /// <returns></returns>
        public float GetLeftPercent()
        {
            if (_nCDTime == 0)
            {
                return 0;
            }
            float fCurLeft = GetLeftMS();
            float fpercent = fCurLeft / _nCDTime;
            return fpercent;
        }

        /// <summary>
        /// CD更新，可以在每帧时更新
        /// </summary>
        /// <param name="time"></param>
        public void Update(float time)
        {
            if (_fStartTime + ((float)_nCDTime / 1000) < time)
            {
                _bIsFinished = true;
            }
        }
    }

    public class CDTimeManager
    {
        //  todo
    }
}

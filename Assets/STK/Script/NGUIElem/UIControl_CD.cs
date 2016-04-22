using UnityEngine;
using System.Collections;

#if _USENGUI_
namespace sunjiahaoz.NGUIElem
{

    public enum CdFillType
    {
        _0To1,
        _1To0,
    }
    [RequireComponent(typeof(UISprite))]
    public class UIControl_CD : MonoBehaviour
    {

        #region _Event_
        public class UIControlCD_Event
        {
            public delegate void OnReset();
            public event OnReset _eventReset;
            public void OnResetAct()
            {
                if (_eventReset != null)
                {
                    _eventReset();
                }
            }

            public delegate void OnFillUpdate(float fFillAmount);
            public event OnFillUpdate _eventFillUpdate;
            public void OnFillUpdateAct(float fFillAmount)
            {
                if (_eventFillUpdate != null)
                {
                    _eventFillUpdate(fFillAmount);
                }
            }

            public delegate void OnPaused(bool bPaused);
            public event OnPaused _eventPaused;
            public void OnPausedAct(bool bPaused)
            {
                if (_eventPaused != null)
                {
                    _eventPaused(bPaused);
                }
            }

            public delegate void OnStop();
            public event OnStop _eventStop;
            public void OnStopAct()
            {
                if (_eventStop != null)
                {
                    _eventStop();
                }
            }
        }
        public UIControlCD_Event _event = new UIControlCD_Event();
        #endregion
        public CdFillType _cdFillType = CdFillType._0To1;
        UISprite _comCd;
        void Awake()
        {
            _comCd = GetComponent<UISprite>();
            _comCd.type = UIBasicSprite.Type.Filled;
        }

        float _fStartDur = 0;
        float _fDuration = 0;
        bool _bIsPaused = false;
        public void Reset(float fDuration)
        {
            if (_cdFillType == CdFillType._0To1)
            {
                _comCd.fillAmount = 0;
            }
            else
            {
                _comCd.fillAmount = 1;
            }

            _fDuration = fDuration;
            _fStartDur = _fDuration;
            Paused(false);

            _event.OnResetAct();
        }

        public void Paused(bool bPaused)
        {
            _bIsPaused = bPaused;
            _event.OnPausedAct(_bIsPaused);
        }

        public void Stop(bool bFillFull)
        {
            _fDuration = 0;
            _fStartDur = 0;
            _bIsPaused = true;
            if (bFillFull)
            {
                if (_cdFillType == CdFillType._0To1)
                {
                    _comCd.fillAmount = 1;
                }
                else
                {
                    _comCd.fillAmount = 0;
                }
            }
            else
            {
                if (_cdFillType == CdFillType._0To1)
                {
                    _comCd.fillAmount = 0;
                }
                else
                {
                    _comCd.fillAmount = 1;
                }
            }

            _event.OnStopAct();
        }

        public void SetFillAmountManual(float fFillAmount)
        {
            _comCd.fillAmount = fFillAmount;
            _fDuration = _fStartDur * fFillAmount;
        }

        void Update()
        {
            if (_bIsPaused)
            {
                return;
            }
            if (_fStartDur <= 0
                || _fDuration <= 0)
            {
                if (_cdFillType == CdFillType._0To1)
                {
                    _comCd.fillAmount = 1;
                }
                else
                {
                    _comCd.fillAmount = 0;
                }
                Stop(true);
            }
            else
            {
                if (_cdFillType == CdFillType._0To1)
                {
                    _comCd.fillAmount = 1f - (_fDuration / _fStartDur);
                }
                else
                {
                    _comCd.fillAmount = (_fDuration / _fStartDur);
                }

                _fDuration -= Time.deltaTime;

                _event.OnFillUpdateAct(_comCd.fillAmount);
            }
        }
    }
}
#endif
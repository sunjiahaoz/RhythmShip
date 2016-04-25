using UnityEngine;
using System.Collections;
using sunjiahaoz.gamebase;


public class BaseLifeCom : MonoBehaviour, ILife
{
    #region _Event_
    public class BaseLifeComEvent
    {
        // 生命值改变
        public delegate void OnAddValue(int nOffValue);
        public event OnAddValue _eventOnAddValue;
        public void OnAddValueEvent(int nOffValue)
        {
            if (_eventOnAddValue != null)
            {
                _eventOnAddValue(nOffValue);
            }
        }
        // 死亡
        public delegate void OnDead();
        public event OnDead _eventOnDead;
        public void OnDeadEvent()
        {
            if (_eventOnDead != null)
            {
                _eventOnDead();
            }
        }
    }
    public BaseLifeComEvent _event = new BaseLifeComEvent();
    #endregion

    #region _Params_
    public int _nCurLife = 0;
    public int _nMaxLife = 1;
    #endregion

    #region _ILife_
    public virtual bool IsDead()
    {
        if (_nCurLife <= 0)
        {
            return true;
        }
        return false;
    }

    public virtual float CurPercent()
    {
        if (_nMaxLife == 0)
        {
            return 0;
        }
        return (float)_nCurLife / (float)_nMaxLife;
    }

    public virtual int CurValue
    {
        get { return _nCurLife; }
    }

    public virtual int MaxValue
    {
        get { return _nMaxLife; }
    }

    public virtual void ReSet(int curValue, int MaxValue)
    {
        _nCurLife = curValue;
        _nMaxLife = MaxValue;
    }

    public virtual void AddValue(int offValue)
    {
        _nCurLife += offValue;
        _event.OnAddValueEvent(offValue);

        if (_nCurLife <= 0)
        {
            _nCurLife = 0;
            _event.OnDeadEvent();
        }
        else if (_nCurLife > _nMaxLife)
        {
            _nCurLife = _nMaxLife;
        }
    }
    #endregion
}


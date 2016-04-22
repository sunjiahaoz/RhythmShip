/*
IProperty
By: @sunjiahaoz, 2016-4-7

游戏中通用的 属性 接口。
 * 包括当前值，最大值
*/
using UnityEngine;
using System.Collections;

namespace sunjiahaoz.gamebase
{
    public interface IEntityProperty<T>
    {
        T CurValue { get; }
        T MaxValue { get; }

        void ReSet(T curValue, T MaxValue);

        void AddValue(T offValue);
    }

#region _基本类型的简单实现_
    public class IntProperty : IEntityProperty<int>
    {
        int _nCurValue = 0;
        int _nMaxValue = 0;
        public virtual int CurValue
        {
            get { return _nCurValue; }
        }

        public virtual int MaxValue
        {
            get { return _nMaxValue; }
        }

        public virtual void ReSet(int curValue, int MaxValue)
        {
            _nCurValue = curValue;
            _nMaxValue = MaxValue;
        }

        public virtual void AddValue(int offValue)
        {
            _nCurValue += offValue;
            _nCurValue = Mathf.Min(_nCurValue, _nMaxValue);
        }
    }
    public class FloatProperty : IEntityProperty<float>
    {
        float _fCurValue = 0;
        float _fMaxValue = 0;
        public virtual float CurValue
        {
            get { return _fCurValue; }
        }

        public virtual float MaxValue
        {
            get { return _fMaxValue; }
        }

        public virtual void ReSet(float curValue, float MaxValue)
        {
            _fCurValue = curValue;
            _fMaxValue = MaxValue;
        }

        public virtual void AddValue(float offValue)
        {
            _fCurValue += offValue;
            _fCurValue = Mathf.Min(_fCurValue, _fMaxValue);
        }
    }
#endregion
}

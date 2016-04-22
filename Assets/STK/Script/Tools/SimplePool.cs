/*
SimplePool
By: @sunjiahaoz, 2016-4-15

一个超简单的内存池
 * 要求对象可以new出来
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace sunjiahaoz
{
    public class SimplePool<T> where T : new()
    {
        protected Queue<T> _queCD = new Queue<T>();

        // 获取一个实例
        public virtual T Instantiate()
        {
            if (_queCD.Count > 0)
            {
                return _queCD.Dequeue();
            }
            return new T();
        }

        // 销毁一个实例
        public virtual void Destroy(T cd)
        {
            if (!_queCD.Contains(cd))
            {
                _queCD.Enqueue(cd);
            }
        }
    }
}

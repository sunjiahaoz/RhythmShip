using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace sunjiahaoz
{
    public interface IHistoryStackItem
    {
        void Do();
        void UnDo();
    }

    public class HistoryStack
    {
        private Stack<IHistoryStackItem> _stack = new Stack<IHistoryStackItem>();

        public void Push(IHistoryStackItem item)
        {
            item.Do();
            _stack.Push(item);
        }

        public IHistoryStackItem Pop()
        {
            if (_stack.Count == 0)
            {
                return null;
            }

            IHistoryStackItem item = _stack.Pop();        
            item.UnDo();
            return item;
        }
    }
}

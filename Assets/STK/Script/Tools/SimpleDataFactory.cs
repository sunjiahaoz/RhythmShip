using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sunjiahaoz
{
    public class SimpleDataFactory<T, U>
    {
        protected Dictionary<T, U> _dictRegister = new Dictionary<T, U>();
        public virtual void RegisterData(T eType, U data)
        {
            _dictRegister[eType] = data;
        }

        public virtual U GetData(T eType)
        {
            if (_dictRegister.ContainsKey(eType))
            {
                return _dictRegister[eType];
            }
            return default(U);
        }
    }
}

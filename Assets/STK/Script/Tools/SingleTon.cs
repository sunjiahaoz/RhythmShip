using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sunjiahaoz
{
    public abstract class Singleton<T> where T : new()
    {
        public static T Instance
        {
            get { return SingletonCreator.instance; }
        }
        class SingletonCreator
        {
            internal static readonly T instance = new T();
        }
    }  
}

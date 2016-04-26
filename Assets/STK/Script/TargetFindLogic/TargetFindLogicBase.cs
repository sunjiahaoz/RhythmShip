using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace sunjiahaoz
{
    public interface ITargetFindLogic<T>
    {
        T[] Find();
    }

    public class TargetFindLogicBase<GameObject> : ITargetFindLogic<GameObject>
    {        
        public virtual void SetLimit(Hashtable tableParam)
        { 

        }

        public virtual GameObject[] Find()
        {
            return null;
        }
    }

    public class TargetFindLogic_None : TargetFindLogicBase<GameObject>
    {
        public override GameObject[] Find()
        {
            return null;
        }
    }
}

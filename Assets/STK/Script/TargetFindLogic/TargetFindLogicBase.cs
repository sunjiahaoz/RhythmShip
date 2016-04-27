using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace sunjiahaoz
{
    // 具体查找逻辑需要的数据
    public class TargetFindLogicParams
    {
        public int _nLayerMask = Physics.AllLayers; // 查找Layer
        public int _nLimitMaxCount = ToolsUseful.MaxIntCanUse; // 限制最大数量

        public virtual void Clear()
        {
            _nLayerMask = Physics.AllLayers;
            _nLimitMaxCount = ToolsUseful.MaxIntCanUse;
        }
    }

    public interface ITargetFindLogic<T>
    {
        T[] Find(TargetFindLogicParams data = null);
    }

    public class TargetFindLogicBase : ITargetFindLogic<GameObject>
    {        
        public virtual void SetLimit(Hashtable tableParam)
        { 

        }

        public virtual GameObject[] Find(TargetFindLogicParams data = null)
        {
            return null;
        }
    }

    public class TargetFindLogic_None : TargetFindLogicBase
    {
        public override GameObject[] Find(TargetFindLogicParams data = null)
        {
            return null;
        }
    }
}

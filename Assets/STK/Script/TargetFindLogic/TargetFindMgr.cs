using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace sunjiahaoz
{
    public enum FindLogicType
    {
        // 通用
        None,
        // todo

        // 游戏本身相关
        PlayerShip,
    }
    public class TargetFindMgr
    {
        #region _Instance_
        public static TargetFindMgr Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TargetFindMgr();
                }
                return _Instance;
            }
        }
        private static TargetFindMgr _Instance;

        private TargetFindMgr()
        {
            Init();
        }
        #endregion
        Dictionary<FindLogicType, TargetFindLogicBase<GameObject>> _dictLogic = new Dictionary<FindLogicType, TargetFindLogicBase<GameObject>>();

        void Init()
        {
            _dictLogic.Clear();
            _dictLogic.Add(FindLogicType.None, new TargetFindLogic_None());
            _dictLogic.Add(FindLogicType.PlayerShip, new TargetFind_PlayerShip());
        }

        public TargetFindLogicBase<GameObject> GetFindLogic(FindLogicType eType)
        {
            if (_dictLogic.ContainsKey(eType))
            {
                return _dictLogic[eType];
            }
            return null;
        }
    }
}

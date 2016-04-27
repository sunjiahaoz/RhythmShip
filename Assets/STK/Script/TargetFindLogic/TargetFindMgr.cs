using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace sunjiahaoz
{
    public enum FindLogicType
    {
        // 通用
        None,
        Sphere,
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
        Dictionary<FindLogicType, TargetFindLogicBase> _dictLogic = new Dictionary<FindLogicType, TargetFindLogicBase>();
        Dictionary<FindLogicType, TargetFindLogicParams> _dictParam = new Dictionary<FindLogicType, TargetFindLogicParams>();

        void Init()
        {
            _dictLogic.Clear();
            //_dictParam.Clear();

            _dictLogic.Add(FindLogicType.None, new TargetFindLogic_None());
            _dictParam.Add(FindLogicType.None, null);

            _dictLogic.Add(FindLogicType.PlayerShip, new TargetFind_PlayerShip());
            _dictParam.Add(FindLogicType.PlayerShip, null);

            _dictLogic.Add(FindLogicType.Sphere, new TargetFindLogic_Sphere());
            _dictParam.Add(FindLogicType.Sphere, new TargetFindLogicPrams_Sphere());
        }

        public TargetFindLogicBase GetFindLogic(FindLogicType eType)
        {
            if (_dictLogic.ContainsKey(eType))
            {
                return _dictLogic[eType];
            }
            return null;
        }

        public TargetFindLogicParams GetParamInstance(FindLogicType eType)
        {
            if (_dictParam.ContainsKey(eType))
            {
                return _dictParam[eType];
            }
            return null;
        }
    }
}

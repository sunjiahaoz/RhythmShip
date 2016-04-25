using UnityEngine;
using System.Collections;

public class GamingData
{
    #region _Instance_
    public static GamingData Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GamingData();
            }
            return _Instance;
        }
    }
    private static GamingData _Instance;
    #endregion

    // 摄像头
    CameraManager _camManager = null;
    public CameraManager CamMgr
    {
        get { return _camManager; }
        set { _camManager = value; }
    }

    // 玩家飞船数据
    PlayerShipData _shipData = new PlayerShipData();
    public PlayerShipData shipData
    {
        get { return _shipData; }
    }
    // 普通射击
    PlayerEquipedFirePointMgr _firePointMgr = new PlayerEquipedFirePointMgr();
    public PlayerEquipedFirePointMgr playerEquipedFirePointMgr
    {
        get { return _firePointMgr; }
    }

    // 技能
    PlayerEquipedSkillMgr _equipedSkillMgr = new PlayerEquipedSkillMgr();
    public PlayerEquipedSkillMgr playerEquipedSkillMgr
    {
        get { return _equipedSkillMgr; }
    }
        
    public GameBattleManager gameBattleManager = null;
    // 每个场景根据配置什么的不同
    SceneConfig _sceneConfig = null;
    public SceneConfig sceneConfig
    {        
        set { _sceneConfig = value; }
        get 
        { 
            if (_sceneConfig == null)
            {
                sunjiahaoz.TagLog.LogError(LogIndex.Data, "没有配置SceneConfig！！！！");
            }
            return _sceneConfig;
        }
    }



    public void Init()
    {
        _shipData.Load();
        _firePointMgr.Load();
        _equipedSkillMgr.Load();
    }
}

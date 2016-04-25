using UnityEngine;
using System.Collections;
using sunjiahaoz;
using System;
public static class LogIndex
{
    public static int Ship = 0;
    public static int Bullet = 1;
    public static int FirePoint = 2;
    public static int Effect = 3;
    public static int Data = 4;
    public static int DeathArea = 5;
    public static int Enemy = 6;
    public static int RecordRhythm = 7;
    public static int Normal = 8;
    public static int GameManager = 9;
    public static int Skill = 10;
    public static int SpaceItem = 11;
}

public class GloabalSet : MonoBehaviour {
    void Awake()
    {
        InitSet();
        DontDestroyOnLoad(gameObject);
    }
    
    void InitSet()
    {
        TagLog.SetLogTag(LogIndex.Ship, "Ship", "cyan");
        TagLog.SetLogTag(LogIndex.Bullet, "Bullet", "orange", TagLog.LogLevel.Warning);
        TagLog.SetLogTag(LogIndex.FirePoint, "FirePoint", "green");
        TagLog.SetLogTag(LogIndex.Effect, "Effect", "#808000ff");
        TagLog.SetLogTag(LogIndex.Data, "Data", "yellow");
        TagLog.SetLogTag(LogIndex.DeathArea, "DeathArea", "gray", TagLog.LogLevel.None);
        TagLog.SetLogTag(LogIndex.Enemy, "Enemy", "#008080ff");
        TagLog.SetLogTag(LogIndex.RecordRhythm, "Rhythm", "#ff9911ff");
        TagLog.SetLogTag(LogIndex.Normal, "Normal", "white");
        TagLog.SetLogTag(LogIndex.GameManager, "GM", "magenta");
        TagLog.SetLogTag(LogIndex.Skill, "Skill", "#aa00ffff");
        TagLog.SetLogTag(LogIndex.SpaceItem, "SpaceItem", "#00aaffff");

        
        // 自身
        Physics.IgnoreLayerCollision(ToolsUseful.GetLayerIndex(LayerName_EnemyBullet), ToolsUseful.GetLayerIndex(LayerName_EnemyBullet));
        Physics.IgnoreLayerCollision(ToolsUseful.GetLayerIndex(LayerName_Player), ToolsUseful.GetLayerIndex(LayerName_Player));
        Physics.IgnoreLayerCollision(ToolsUseful.GetLayerIndex(LayerName_PlayerBullet), ToolsUseful.GetLayerIndex(LayerName_PlayerBullet));
        Physics.IgnoreLayerCollision(ToolsUseful.GetLayerIndex(LayerName_Enemy), ToolsUseful.GetLayerIndex(LayerName_Enemy));
        // 敌人子弹与玩家子弹
        Physics.IgnoreLayerCollision(ToolsUseful.GetLayerIndex(LayerName_PlayerBullet), ToolsUseful.GetLayerIndex(LayerName_EnemyBullet));

        // 玩家子弹与玩家战机
        Physics.IgnoreLayerCollision(ToolsUseful.GetLayerIndex(LayerName_PlayerBullet), ToolsUseful.GetLayerIndex(LayerName_Player));
        // 敌人子弹与敌人战机
        Physics.IgnoreLayerCollision(ToolsUseful.GetLayerIndex(LayerName_EnemyBullet), ToolsUseful.GetLayerIndex(LayerName_Enemy));

        // 配置文件
        Equip_FirePointConfig.load();
        Equip_SkillConfig.load();

        // 特效初始化
        ShotEffect.Instance.Init();
        // 游戏数据初始化
        GamingData.Instance.Init();
        // 随机初始化
        RUL.Rul.Initialize();
    }

    
    public static string LayerName_PlayerBullet = "PlayerBullet";
    public static string LayerName_Player = "Player";
    public static string LayerName_EnemyBullet = "EnemyBullet";
    public static string LayerName_Enemy = "Enemy";
    public static string LayerName_Effect = "Effect";
    public static string LayerName_Item = "Item";

    public static string SceneName_Battle = "first";
}


/*
CommonUIRegisterInfo
By: @sunjiahaoz, 2016-3-24

统一管理所有界面的注册信息，
 * 注册信息主要是FrameID与对应的Prefab名称 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz.UI;


// 每个界面都需要一个ID
public enum ID_FRAME
{
    ID_None,
    ID_FrameSwitchScene,
    ID_FrameStart,
}

// 注册信息
public class FrameRegInfo
{
    public ID_FRAME nFrameID = ID_FRAME.ID_None;
    public string strPrefabName = string.Empty;
}

public class CommonUIRegisterInfo : BaseUIRegisterInfo<ID_FRAME, FrameRegInfo>
{
    #region _Instance_
    public static CommonUIRegisterInfo Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new CommonUIRegisterInfo();
            }
            return _Instance;
        }
    }
    private static CommonUIRegisterInfo _Instance;

    public CommonUIRegisterInfo()
    {
        InitRegisterAll();
    }
    #endregion

    public override void InitRegisterAll()
    {
        base.InitRegisterAll();

        RegisterUI(ID_FRAME.ID_FrameSwitchScene, "FrameSwitchScene");
        RegisterUI(ID_FRAME.ID_FrameStart, "FrameStart");
    }

    void RegisterUI(ID_FRAME idFrame, string strPrefabName)
    {
        FrameRegInfo regInfo = new FrameRegInfo();
        regInfo.nFrameID = idFrame;
        regInfo.strPrefabName = strPrefabName;
        RegisterUI(idFrame, regInfo);
    }

}


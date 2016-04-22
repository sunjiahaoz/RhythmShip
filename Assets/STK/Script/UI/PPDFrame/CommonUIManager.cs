using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;
using sunjiahaoz.UI;

public class CommonUIManager : SingletonMonoBehaviour<CommonUIManager>, IUIManager<ID_FRAME>
{
    // ui预制体所在的路径，相对于Resources文件夹
    public static string FramePrefabPath = "UI";
    // 已经load过的窗口
    Dictionary<ID_FRAME, CommonBaseFrame> _dictLoadedFrame = new Dictionary<ID_FRAME, CommonBaseFrame>();

    #region _IUIManager_
    /// <summary>
    /// 加载一个界面
    /// 如果该界面已经加载过了则不会再次加载
    /// 可以指定是否加载完成之后直接进行显示UI
    /// </summary>
    /// <param name="frameId">要加载显示的UI</param>
    /// <param name="onAfterLoadUI">加载之后的额外操作</param>
    /// <param name="bLoadAndShow">是否在加载之后进行显示</param>        
    /// <returns></returns>
    public IFrame<ID_FRAME> LoadUI(ID_FRAME frameId, bool bLoadAndShow = false)
    {
        CommonBaseFrame frameReturn = null;
        // 是否已经加载过了
        if (IsLoaded(frameId))
        {
            frameReturn = _dictLoadedFrame[frameId];
        }
        else
        {
            frameReturn = LoadFrameRes(frameId);
            if (frameReturn != null)
            {
                // 默认隐藏界面实例
                frameReturn.gameObject.SetActive(false);
            }
        }

        // 如果要在加载完显示
        if (frameReturn != null
            && bLoadAndShow)
        {
            return ShowUI(frameId);
        }

        return frameReturn;
    }

    /// <summary>
    /// 显示某个UI
    /// 如果没有加载该UI会先进行加载8
    /// </summary>
    /// <param name="frameId"></param>
    /// <returns></returns>
    public IFrame<ID_FRAME> ShowUI(ID_FRAME frameId)
    {
        IFrame<ID_FRAME> frame = null;
        if (_dictLoadedFrame.ContainsKey(frameId))
        {
            frame = _dictLoadedFrame[frameId];
        }
        else
        {
            frame = LoadUI(frameId, false);
        }

        if (frame != null)
        {
            frame.OnFrameShow();
        }
        return frame;
    }

    public void HideUI(ID_FRAME frameId)
    {
        if (_dictLoadedFrame.ContainsKey(frameId))
        {
            _dictLoadedFrame[frameId].OnFrameHide();
        }
    }

    public void DestroyUI(ID_FRAME frameId)
    {
        if (_dictLoadedFrame.ContainsKey(frameId))
        {
            _dictLoadedFrame[frameId].OnFrameDestroy();
            Destroy(_dictLoadedFrame[frameId].gameObject);
            _dictLoadedFrame.Remove(frameId);
        }
    }

    public bool IsLoaded(ID_FRAME frameId)
    {
        if (_dictLoadedFrame.ContainsKey(frameId))
        {
            return true;
        }
        return false;
    }
    #endregion

    #region _Inner_
    CommonBaseFrame LoadFrameRes(ID_FRAME nFrameID)
    {
        FrameRegInfo regInfo = CommonUIRegisterInfo.Instance.GetReginfo(nFrameID);
        if (regInfo == null)
        {
            Debug.LogError("<color=red>[Error]</color>---" + "找不到Frame" + nFrameID + "的注册配置");
            return null;
        }

        // 通过注册信息获取prefab
        ResourceManager.instance.AddRequest(regInfo.strPrefabName, FramePrefabPath, OnLoadFrameRes, regInfo);
        if (_dictLoadedFrame.ContainsKey(nFrameID))
        {
            return _dictLoadedFrame[nFrameID];
        }
        return null;
    }

    void OnLoadFrameRes(UnityEngine.Object asset, System.Object arg)
    {
        FrameRegInfo regInfo = (FrameRegInfo)arg;
        GameObject obj = Instantiate(asset, Vector3.zero, Quaternion.identity) as GameObject;
        if (obj == null)
        {
            Debug.LogWarning("Load Frame Error !!!!!!!!!");
            return;
        }
        // 设置父子关系
        obj.transform.parent = transform;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        // 获取IBaseFrame
        CommonBaseFrame frame = obj.GetComponent<CommonBaseFrame>();
        frame.frameId = regInfo.nFrameID;
        // 加载
        frame.OnFrameLoad();
        // 保存到已加载列表        
        _dictLoadedFrame[regInfo.nFrameID] = frame;
    }
    #endregion
}    


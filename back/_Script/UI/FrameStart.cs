using UnityEngine;
using System.Collections;
public class FrameStart : CommonBaseFrame {
    public void OnBnStart()
    {
        FrameSceneSwitch fss = CommonUIManager.Instance.ShowUI(ID_FRAME.ID_FrameSwitchScene) as FrameSceneSwitch;
        fss.SwitchScene(GloabalSet.SceneName_Battle);

        CommonUIManager.Instance.DestroyUI(frameId);
    }
}

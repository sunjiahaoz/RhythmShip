using UnityEngine;
using System.Collections;
using sunjiahaoz.UI;
public class CommonBaseFrame : MonoBehaviour, IFrame<ID_FRAME>
{
    // 当前窗口对应的FrameID，在实例化时进行设置
    ID_FRAME _frameId = ID_FRAME.ID_None;
    public ID_FRAME frameId
    {
        get { return _frameId; }
        set { _frameId = value; }
    }

    public virtual void OnFrameLoad()
    {

    }

    public virtual void OnFrameShow()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnFrameHide()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnFrameDestroy()
    {
        OnFrameHide();
    }
}


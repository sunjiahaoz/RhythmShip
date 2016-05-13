using UnityEngine;
using System.Collections;
using sunjiahaoz;

public enum CameraAnchorPos
{
    LeftTop,
    RightTop,
    RightBottom,
    LeftBottom,
}
public class CameraManager : MonoBehaviour {
    public CameraShake _cShake;
    public Transform[] _anchors;    // 需要4个，按照CameraAnchorPos赋值
    void Awake()
    {
        if (_anchors.Length != 4)
        {
            TagLog.LogError(LogIndex.Normal, "CameraManager需要4个Anchor!!!");
        }
        GamingData.Instance.CamMgr = this;        
    }

    // 获取锚点
    public Transform GetAnchorPos(CameraAnchorPos pos)
    {
        return _anchors[(int)pos];
    }
    // 获取水平方向两个锚点的距离
    public float GetWidthDistance()
    {
        return GetAnchorPos(CameraAnchorPos.RightTop).position.x - GetAnchorPos(CameraAnchorPos.LeftTop).position.x;
    }
    // 获取垂直方向两个锚点的距离
    public float GetHeightDistance()
    {
        return GetAnchorPos(CameraAnchorPos.LeftBottom).position.y - GetAnchorPos(CameraAnchorPos.LeftTop).position.y;
    }
}

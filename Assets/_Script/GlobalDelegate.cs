using UnityEngine;
using System.Collections;

public class GlobalDelegate
{
    #region _Instance_
    public static GlobalDelegate Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GlobalDelegate();
            }
            return _Instance;
        }
    }
    private static GlobalDelegate _Instance;
    #endregion

    // 场景切换结束
    public delegate void OnSceneSwitchEnd(string strSceneName);
    public event OnSceneSwitchEnd _eventOnSceneSwitchEnd;
    public void OnSceneSwitchEndRun(string strSceneName)
    {
        if (_eventOnSceneSwitchEnd != null)
        {
            _eventOnSceneSwitchEnd(strSceneName);
        }
    }
}

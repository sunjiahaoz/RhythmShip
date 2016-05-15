using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class WaitToScene : MonoBehaviour {
    public KeyCode _triggerKey = KeyCode.Space;
    public float _fDelay = 2f;
    public string _strSceneName = "";

    bool _bIsTriggered = false;
    void Update()
    {
        if (_bIsTriggered)
        {
            return;
        }
        
        if (Input.GetKeyUp(_triggerKey))
        {
            _bIsTriggered = true;
            vp_Timer.In(_fDelay, () => 
            {
                Application.LoadLevel(_strSceneName);
            });
        }
    }
}

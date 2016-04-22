using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class FrameSceneSwitch : CommonBaseFrame {
    public ObjeMoveAnim _trLeftDoor;
    public ObjeMoveAnim _trRightDoor;

    string _strToSceneName = string.Empty;
    AsyncOperation _ao = null;    

    public void SwitchScene(string strSceneName)
    {
        _strToSceneName = strSceneName;
        StartCoroutine(OnSwitchScene());
    }

    IEnumerator OnSwitchScene()
    {
        _trLeftDoor.Run();
        _trRightDoor.Run();
        yield return new WaitForSeconds(_trLeftDoor._fDuration + _trLeftDoor._fDelay);
        _ao = Application.LoadLevelAsync(_strToSceneName);
        while (!_ao.isDone)
        {
            yield return 0;
        }
        //iTween.Stop(_trLeftDoor.gameObject);
        //iTween.Stop(_trRightDoor.gameObject);
        _trLeftDoor.RunBack();
        _trRightDoor.RunBack();
        yield return new WaitForSeconds(_trLeftDoor._fDuration + _trLeftDoor._fDelay);
        // 加载完成
        GlobalDelegate.Instance.OnSceneSwitchEndRun(_strToSceneName);
        // 隐藏自己
        CommonUIManager.Instance.HideUI(frameId);
    }

    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Home))
        //{
        //    _trLeftDoor.Run();
        //    _trRightDoor.Run();
        //}
        //if (Input.GetKeyUp(KeyCode.End))
        //{
        //    _trLeftDoor.RunBack();
        //    _trRightDoor.RunBack();
        //}
    }
}

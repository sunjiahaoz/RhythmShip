using UnityEngine;
using System.Collections;

public class SceneConfig : FirePointRhythm
{   
    public class SceneConfigEvent
    {
        public delegate void OnRhythmNormal();
        public event OnRhythmNormal _eventOnRhythmNormal;
        public void OnRhythmNormalEvent()
        {
            if (_eventOnRhythmNormal != null)
            {
                _eventOnRhythmNormal();
            }
        }
    }
    public SceneConfigEvent _event = new SceneConfigEvent();

    public string _strMusicName = "";
    public bool _bShowMusicSlider = false;

    protected override void Awake()
    {
        base.Awake();
        GamingData.Instance.sceneConfig = this;
    }

    protected override void Start()
    {
        base.Start();
        if (_bShowMusicSlider)
        {
            CommonUIManager.Instance.ShowUI(ID_FRAME.ID_FrameMusic);
        }
    }

    protected override void CreateObject(Vector3 createPos, System.Action<BaseFireThing> afterCreate = null)
    {   
        _event.OnRhythmNormalEvent();
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        GamingData.Instance.sceneConfig = null;        
    }
}

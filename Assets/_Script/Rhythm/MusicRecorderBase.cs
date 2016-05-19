using UnityEngine;
using System.Collections;
using sunjiahaoz;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

public class MusicRecorderBase : FSMMono<MusicRecorderBase, MusicRecorderBase.State> {
#region _Event_
    public class MusicRecorderEvent
    {
        // 每播放一个点的处理
        // 在录制时就是点击录制键的时候执行以下
        public delegate void OnPlayOneShot(int nPointIndex);
        public event OnPlayOneShot _eventOnPlayOneShot;
        public void OnPlayOneShotEvent(int nPointIndex)
        {
            if (_eventOnPlayOneShot != null)
            {
                _eventOnPlayOneShot(nPointIndex);
            }
        }

        // 音乐播放完成的时候
        // 包括录制与播放两种状态
        public delegate void OnPlayEnd();
        public event OnPlayEnd _eventOnPlayEnd;
        public void OnPlayEndEvent()
        {
            if (_eventOnPlayEnd != null)
            {
                _eventOnPlayEnd();
            }
        }
    }
    public MusicRecorderEvent _event = new MusicRecorderEvent();
#endregion

#region _State_
    public enum State
    {        
        Record,        
        Play,
        Stop,   // 什么也不做
    }
    public MusicRecorderBaseState_Record _stateRecord = new MusicRecorderBaseState_Record();    
    public MusicRecorderBaseState_Play _statePlay = new MusicRecorderBaseState_Play();
    public MusicRecorderBaseState_Stop _stateStop = new MusicRecorderBaseState_Stop();
    public override void FSMInit()
    {
        base.FSMInit();
        FSM = new FiniteStateMachine<MusicRecorderBase, State>(this);
        FSM.RegisterState(_stateRecord);        
        FSM.RegisterState(_statePlay);
        FSM.RegisterState(_stateStop);
    }
    
#endregion
    // 当前播放的音乐    
    AudioObject _CurAudio;
    public AudioObject CurAudio
    {
        get { return _CurAudio; }
        set { _CurAudio = value; }
    }

    RhythmDataMgrBase _rhythmMgr;
    public RhythmDataMgrBase RhythmMgr
    {
        get { return _rhythmMgr; }
    }

    public string _strSavePath = "RecordDefault";    
    public string _strRecordConfigName = "Test";
    
    [Header("如果为true则表示录制，否则表示播放")]
    public bool _bRecord = false;
#region _Mono_
    protected virtual void Awake()
    {   
        FSMInit();
        _rhythmMgr = new RhythmDataMgr_Normal();
    }
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        FSMUpdate();
    }
#endregion    
    public virtual void PlayOrRecord(AudioObject ao, string strFolder, string strSaveToConfig)
    {
        if (_bRecord)
        {
            StartRecord(ao, strFolder, strSaveToConfig);
        }
        else
        {
            StarPlay(ao, strFolder, strSaveToConfig);
        }
    }
    public virtual void PlayOrRecord(AudioObject ao)
    {
        if (_bRecord)
        {
            StartRecord(ao);
        }
        else
        {
            StarPlay(ao);
        }
    }    

    /// <summary>
    /// 开始录制，使用指定的配置文件名称进行保存
    /// </summary>
    /// <param name="audioObj">音乐对象</param>
    /// <param name="strFolder">保存到的文件夹路径，相对于Resources</param>
    /// <param name="strSaveToConfig">保存到的配置文件名称</param>
    public virtual void StartRecord(AudioObject audioObj, string strFolder, string strSaveToConfig)
    {
        _strSavePath = strFolder;
        _strRecordConfigName = strSaveToConfig;
        StartRecord(audioObj);
    }
    /// <summary>
    /// 开始录制。使用对象属性中填写的路径及名称进行保存
    /// </summary>
    /// <param name="audioObj"></param>
    public virtual void StartRecord(AudioObject audioObj)
    {
        CurAudio = audioObj;        
        ChangeState(State.Record);
    }

    /// <summary>
    /// 开始播放。使用指定的路径及名称进行播放
    /// </summary>
    /// <param name="audioObj"></param>
    /// <param name="strConfig"></param>
    public virtual void StarPlay(AudioObject audioObj, string strFolder, string strConfig)
    {
        _strSavePath = strFolder;
        _strRecordConfigName = strConfig;
        StarPlay(audioObj);
    }
    /// <summary>
    /// 开始播放，使用属性中填写的路径及名称进行播放
    /// </summary>
    /// <param name="audioObj"></param>
    public virtual void StarPlay(AudioObject audioObj)
    {
        CurAudio = audioObj;        
        ChangeState(State.Play);
    }

    // 清空节奏点
    public virtual void ResetList()
    {
        _rhythmMgr.Clear();
    }

    public virtual void Stop()
    {
        ChangeState(State.Stop);
    }

#region _ContextMenu_

#if UNITY_EDITOR

    [ContextMenu("删除配置")]
    void DeleteConfig()
    {
        string strFilePath = Application.dataPath + "/Resources/" + _strSavePath + "/" + _strRecordConfigName + ".txt";
        UnityEditor.FileUtil.DeleteFileOrDirectory(strFilePath);
        //SimpleFileProcess.DeleteFile(strFilePath);
    }

    [ContextMenu("配置名称为对象名")]
    void SetConfigName()
    {
        _strSavePath = SceneManager.GetActiveScene().name + "/Record";
        _strRecordConfigName = gameObject.name;
    }
    [ContextMenu("先删除再设置名称")]
    void SetConfigNameAfterDelete()
    {
        DeleteConfig();
        SetConfigName();
    }

    [ContextMenu("配置是否存在")]
    void CheckConfigHas()
    {
        string strFilePath = Application.dataPath + "/Resources/" + _strSavePath + "/" + _strRecordConfigName + ".txt";
        if (SimpleFileProcess.FileExists(strFilePath))
        {
            TagLog.Log(LogIndex.RecordRhythm, "文件存在！！");                
        }
        else
        {
            TagLog.Log(LogIndex.RecordRhythm, "文件不不不不不不存在！！");
        }
    }
#endif
#endregion

}

[System.Serializable]
public class MusicRecorderBaseState_Record : FSMState<MusicRecorderBase, MusicRecorderBase.State>
{
    public override MusicRecorderBase.State StateID
    {
        get
        {
            return MusicRecorderBase.State.Record;
        }
    }

    // 当这个键按下的时候就是记录一个点
    public KeyCode _codeListen = KeyCode.F;    
    // 增加一个时间点的时候可以delay，向前为-值，向后为+值
    public float _fDelayPer = 0f;

    AudioObject _curAo;
    public override void Enter()
    {
        if (entity._strRecordConfigName.Length == 0)
        {
            TagLog.LogError(LogIndex.RecordRhythm, "录制需要文件名！！");            
            entity.ChangeState(MusicRecorderBase.State.Stop);
            return;
        }
        if (entity.CurAudio == null)
        {
            TagLog.LogError(LogIndex.RecordRhythm, "当前CurAudio为Null！！！");
            entity.ChangeState(MusicRecorderBase.State.Stop);
            return;
        }        

        base.Enter();        
        StartEditRecord();
    }

    public override void Execute()
    {
        base.Execute();
        UpdateRecord();
    }

    public override void Exit()
    {
        base.Exit();
        entity.CurAudio.completelyPlayedDelegate -= OnAudioComplete;
    }

    protected virtual void StartEditRecord()
    {
        TagLog.Log(LogIndex.RecordRhythm, "开始录制：", entity.gameObject);        
        entity.ResetList();
        entity.CurAudio.completelyPlayedDelegate += OnAudioComplete;
    }

    void OnAudioComplete(AudioObject ao)
    {
        Save();
        entity._event.OnPlayEndEvent();
    }

    protected virtual void UpdateRecord()
    {
        if (Input.GetKeyDown(_codeListen))
        {
            AddTimePoint(entity.CurAudio.audioTime + _fDelayPer);            
            entity._event.OnPlayOneShotEvent(entity.RhythmMgr.Count() - 1);
            TagLog.Log(LogIndex.RecordRhythm, "AddRhytm:" + entity.gameObject.name);
        }
    }
    protected virtual void Save()
    {
        TagLog.Log(LogIndex.RecordRhythm, "开始Save:" + entity.gameObject.name);
        //string strInfo = GenerateHeadLine() + "\n";
        string strInfo = entity.RhythmMgr.GenerateConfig();
        SimpleFileProcess.CreateDir(GetRecordSaveConfigFolder(true));
        SimpleFileProcess.CreateFile(GetRecordSaveConfigFolder(true), entity._strRecordConfigName + ".txt", strInfo, true);
        TagLog.Log(LogIndex.RecordRhythm, "文件" + GetRecordSaveConfigFolder(true) + "/" + entity._strRecordConfigName + "创建完成:" + entity.gameObject.name);        
    }

    protected virtual void AddTimePoint(float fTime)
    {
        entity.RhythmMgr.Add(fTime);
    }

    // 返回配置文件的保存文件夹路径
    // 如果bFullPath则返回绝对路径，否则是基于Resources的路基
    string GetRecordSaveConfigFolder(bool bFullPath)
    {
        if (bFullPath)
        {
            return Application.dataPath + "/Resources/" + entity._strSavePath;// GlobalConst.Path_RecordSavePath + "/" + GameGlobal.Instance.curScene._strSceneName;
        }
        else
        {
            return entity._strSavePath;//.Path_RecordSavePath + "/" + GameGlobal.Instance.curScene._strSceneName;
        }
    }
}

//[System.Serializable]
public class MusicRecorderBaseState_Play : FSMState<MusicRecorderBase, MusicRecorderBase.State>
{
    public override MusicRecorderBase.State StateID
    {
        get
        {
            return MusicRecorderBase.State.Play;
        }
    }        
    protected bool _bIsPlaying = false;
    protected int _nCurCheckIndex = 0;

    public override void Enter()
    {
        base.Enter();
        PlayRecordCreate();
    }

    public override void Execute()
    {
        base.Execute();
        UpdatePlayRecord();
    }

    public virtual void EndRecordCreate()
    {
        _bIsPlaying = false;
        _nCurCheckIndex = 0;
        entity._event.OnPlayEndEvent();
    }
    public virtual void PlayRecordCreate()
    {
        if (entity._strRecordConfigName.Length == 0)
        {
            TagLog.LogError(LogIndex.RecordRhythm, "没有文件可以播放！！");            
            entity.ChangeState(MusicRecorderBase.State.Stop);
            return;
        }
        if (LoadRecord(entity._strRecordConfigName))
        {
            _bIsPlaying = true;
            _nCurCheckIndex = 0;

            // 调整下一个节奏点时间
            if (entity.CurAudio == null
                || !entity.CurAudio.IsPlaying())
            {
                _nCurCheckIndex = 0;
            }
            else
            {
                JumpRhythmIndexToNow();
            }            
        }
    }
    
    void JumpRhythmIndexToNow()
    {
        for (int i = 0; i < entity.RhythmMgr.Count(); i++)
        {
            if (entity.CurAudio.audioTime < entity.RhythmMgr.GetPointByIndex(i))
            {
                _nCurCheckIndex = i;
                break;
            }
        }
    }

    protected virtual bool LoadRecord(string strRecordConfig)
    {  
        entity.ResetList();

        string strFileName = strRecordConfig;
        UnityEngine.Object obj = ResourceManager.instance.RequestImediate(strFileName, GetRecordLoadConfigFolder(false));
        if (obj == null)
        {
            TagLog.LogWarning(LogIndex.RecordRhythm, GetRecordLoadConfigFolder(false) + "中找不到" + strRecordConfig, obj);            
            entity.ChangeState(MusicRecorderBase.State.Stop);
            return false;
        }
        TextAsset ta = obj as TextAsset;
        entity.RhythmMgr.LoadByConfigData(ta.text);

        return true;
    }
    // 播放完成之后调用
    public System.Action _endRecord;
    public virtual void UpdatePlayRecord()
    {
        if (!_bIsPlaying)
        {
            return;
        }
        if (_nCurCheckIndex >= entity.RhythmMgr.Count())
        {
            EndRecordCreate();
            if (_endRecord != null)
            {
                _endRecord();
            }
            return;
        }

        if (entity.CurAudio.audioTime >= entity.RhythmMgr.GetPointByIndex(_nCurCheckIndex))
        {
            _nCurCheckIndex++;
            entity._event.OnPlayOneShotEvent(_nCurCheckIndex - 1);            
        }
    }

    /// <summary>
    /// 返回配置文件的读取路径
    /// </summary>
    /// <param name="bFullPath"></param>
    /// <returns></returns>
    string GetRecordLoadConfigFolder(bool bFullPath)
    {
        if (bFullPath)
        {
            return Application.dataPath + "/Resources/" + entity._strSavePath;// GlobalConst.Path_RecordSavePath + "/" + GameGlobal.Instance.curScene._strSceneName;
        }
        else
        {
            return entity._strSavePath;// GlobalConst.Path_RecordLoadPath + "/" + GameGlobal.Instance.curScene._strSceneName;
        }
    }
}

public class MusicRecorderBaseState_Stop : FSMState<MusicRecorderBase, MusicRecorderBase.State>
{
    public override MusicRecorderBase.State StateID
    {
        get
        {
            return MusicRecorderBase.State.Stop;
        }
    }
}
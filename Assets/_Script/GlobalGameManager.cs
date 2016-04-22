using UnityEngine;
using System.Collections;
using sunjiahaoz;
public class GlobalGameManager : FSMMono<GlobalGameManager, GlobalGameManager.State> {
#region _State_
    public enum State
    {
        Logo,
        StartScene,
        BattleScene,
    }

    GlobalGameMgr_Logo _stateLogo = new GlobalGameMgr_Logo();
    GlobalGameMgr_StartScene _stateStartScene = new GlobalGameMgr_StartScene();
    GlobalGameMgr_BattleScene _stateBattleScene = new GlobalGameMgr_BattleScene();
    public override void FSMInit()
    {
        base.FSMInit();
        FSM = new FiniteStateMachine<GlobalGameManager, State>(this);
        FSM.RegisterState(_stateLogo);
        FSM.RegisterState(_stateStartScene);
        FSM.RegisterState(_stateBattleScene);
    }
#endregion

    public bool bTest = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        FSMInit();
        if (bTest)
        {
            ChangeState(State.BattleScene);
        }
        else
        {
            ChangeState(State.Logo);
        }        
    }

    void Update()
    {
        FSMUpdate();
    }
}

public class GlobalGameMgr_Logo : FSMState<GlobalGameManager, GlobalGameManager.State>
{
    public override GlobalGameManager.State StateID
    {
        get
        {
            return GlobalGameManager.State.Logo;
        }
    }

    public override void Enter()
    {
        base.Enter();
        TagLog.LogWarning(LogIndex.GameManager, "进入Logo状态！！！没有实现");
        entity.ChangeState(GlobalGameManager.State.StartScene);
    }
}

public class GlobalGameMgr_StartScene : FSMState<GlobalGameManager, GlobalGameManager.State>
{
    public override GlobalGameManager.State StateID
    {
        get
        {
            return GlobalGameManager.State.StartScene;
        }
    }

    public override void Enter()
    {
        base.Enter();
        TagLog.Log(LogIndex.GameManager, "进入StartScene状态");
        CommonUIManager.Instance.ShowUI(ID_FRAME.ID_FrameStart);
        GlobalDelegate.Instance._eventOnSceneSwitchEnd += Instance__eventOnSceneSwitchEnd;
    }
    public override void Exit()
    {
        base.Exit();
        GlobalDelegate.Instance._eventOnSceneSwitchEnd -= Instance__eventOnSceneSwitchEnd;
    }

    void Instance__eventOnSceneSwitchEnd(string strSceneName)
    {
        TagLog.Log(LogIndex.GameManager, "SwitchScene:" + strSceneName);
        if (string.Compare(strSceneName, GloabalSet.SceneName_Battle) == 0)
        {
            entity.ChangeState(GlobalGameManager.State.BattleScene);
        }
    }
}

public class GlobalGameMgr_BattleScene : FSMState<GlobalGameManager, GlobalGameManager.State>
{
    public override GlobalGameManager.State StateID
    {
        get
        {
            return GlobalGameManager.State.BattleScene;
        }
    }

    public override void Enter()
    {
        base.Enter();
        TagLog.Log(LogIndex.GameManager, "进入BattleScene");
        GamingData.Instance.gameBattleManager.BattleInitAndStart();
        GamingData.Instance.gameBattleManager._event._eventOnState_End += _event__eventOnState_End;
    }

    public override void Exit()
    {
        base.Exit();
        GamingData.Instance.gameBattleManager._event._eventOnState_End -= _event__eventOnState_End;
    }

    void _event__eventOnState_End()
    {
        TagLog.Log(LogIndex.GameManager, "是的，GlobalManager也检测到结束了！！！！");
    }
}

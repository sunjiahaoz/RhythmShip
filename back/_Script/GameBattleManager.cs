using UnityEngine;
using System.Collections;
using sunjiahaoz;
public class GameBattleManager : FSMMono<GameBattleManager, GameBattleManager.State> {
#region _State_
    public enum State
    {
        LoadInit,
        Start,
        End,
    }

    GameManager_LoadInit _stateLoadInit = new GameManager_LoadInit();
    GameManager_Start _stateStart = new GameManager_Start();
    GameManager_End _stateEnd = new GameManager_End();
    public override void FSMInit()
    {
        base.FSMInit();
        FSM = new FiniteStateMachine<GameBattleManager, State>(this);
        FSM.RegisterState(_stateLoadInit);
        FSM.RegisterState(_stateStart);
        FSM.RegisterState(_stateEnd);
    }
#endregion

#region _event_
    public class GameMangerEvent
    {
        public delegate void OnState_LoadInit();
        public event OnState_LoadInit _eventOnStateLoadInit;
        public void OnState_LoadInitEvent()
        {
            if (_eventOnStateLoadInit != null)
            {
                _eventOnStateLoadInit();
            }
        }

        public delegate void OnState_Start();
        public event OnState_Start _eventOnState_Start;
        public void OnState_StartEvent()
        {
        	if (_eventOnState_Start != null)
        	{
        		_eventOnState_Start();
        	}
        }

        public delegate void OnState_End();
        public event OnState_End _eventOnState_End;
        public void OnState_EndEvent()
        {
        	if (_eventOnState_End != null)
        	{
        		_eventOnState_End();
        	}
        }
    }
    public GameMangerEvent _event = new GameMangerEvent();
#endregion

#region _Params_
    // 当前正在播放的
    AudioObject _ao;
    public AudioObject CurAO
    {
        set { _ao = value; }
        get { return _ao; }
    }

    // 游戏过程中的玩家飞机
    PlayerShip _playerShip = null;
    public PlayerShip playerShip
    {
        get { return _playerShip; }
        set { _playerShip = value; }
    }
#endregion

#region _Mono_
    void Awake()
    {
        GamingData.Instance.gameBattleManager = this;
        FSMInit();
    }
    
    public void BattleInitAndStart()
    {
        ChangeState(State.LoadInit);
    }

    void Update()
    {
        FSMUpdate();
    }
#endregion
}

public class GameManager_LoadInit : FSMState<GameBattleManager, GameBattleManager.State>
{
    public override GameBattleManager.State StateID
    {
        get
        {
            return GameBattleManager.State.LoadInit;
        }
    }
    public override void Enter()
    {
        base.Enter();
        TagLog.Log(LogIndex.GameManager, "LoadInit Enter 初始化一些东西");
        // 初始化玩家飞机 Todo!!!!!!
        entity.playerShip = GameObject.Find("PlayerShip").GetComponent<PlayerShip>();

        entity._event.OnState_LoadInitEvent();

        entity.StartCoroutine(DelayToInvoke.DelayToInvokeDo(() => 
        {
            entity.ChangeState(GameBattleManager.State.Start);
        }, 2f));
    }
}

public class GameManager_Start : FSMState<GameBattleManager, GameBattleManager.State>
{
    public override GameBattleManager.State StateID
    {
        get
        {
            return GameBattleManager.State.Start;
        }
    }
    public override void Enter()
    {
        base.Enter();
        TagLog.Log(LogIndex.GameManager, "Start Enter 开始游戏！！！");        
        entity.CurAO = AudioController.PlayMusic(GamingData.Instance.sceneConfig._strMusicName);
        entity.CurAO.completelyPlayedDelegate += OnCurAOComplete;
        entity._event.OnState_StartEvent();
    }

    public override void Exit()
    {
        base.Exit();
        entity.CurAO.completelyPlayedDelegate -= OnCurAOComplete;
    }

    void OnCurAOComplete(AudioObject audioObject)
    {
        TagLog.Log(LogIndex.GameManager, "播放完了！");
        entity.ChangeState(GameBattleManager.State.End);
    }
}

public class GameManager_End : FSMState<GameBattleManager, GameBattleManager.State>
{
    public override GameBattleManager.State StateID
    {
        get
        {
            return GameBattleManager.State.End;
        }
    }
    public override void Enter()
    {
        base.Enter();
        TagLog.Log(LogIndex.GameManager, "End Enter 播放完了，游戏结束");
        entity._event.OnState_EndEvent();
    }
}


/*
SkillElem_Defender
By: @sunjiahaoz, 2016-4-26

防护罩技能的防护罩实现
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz;
using DG.Tweening;

public class SkillElem_Defender : FSMMono<SkillElem_Defender, SkillElem_Defender.State>
{

    #region _State_
    public enum State
    {
        Disable,            // 未启用
        DefenderStart,  // 防御开始
        Defendering,     // 正在防御中
        DefendEnd,      // 防御效果完成，过渡到未启用的一个过渡状态
    }
    
    DefenderState_DefendDisable _stateDisable = new DefenderState_DefendDisable();
    DefenderState_Start _stateStart = new DefenderState_Start();
    DefenderState_DefendEnd _stateEnd = new DefenderState_DefendEnd();
    DefenderState_Defendering _stateDefendering = new DefenderState_Defendering();
    public override void FSMInit()
    {
        base.FSMInit();
        FSM = new FiniteStateMachine<SkillElem_Defender, State>(this);
        FSM.RegisterState(_stateDisable);
        FSM.RegisterState(_stateStart);
        FSM.RegisterState(_stateEnd);
        FSM.RegisterState(_stateDefendering);
    }
    #endregion

#region _Mono_
    void Awake()
    {
        FSMInit();
        _trigger = GetComponentInChildren<ColliderTrigger>();
        if (_trigger == null)
        {
            TagLog.LogError(LogIndex.Skill, "Defender 需要 ColliderTrigger");
        }
    }

    void Update()
    {
        FSMUpdate();
    }
#endregion

#region _Params_
    public Transform _trDefenderBody;
    public float _fRadius;  // 半径
    public float _fMinRadius;   // 最小半径，到达这个半径就不能再小了
    public float _fDur;     // 持续时间
    public LayerMask _defendLayer;  // 防御的类型
    public int _nHurtPerTrigger = 1; // 对敌人造成的伤害

    ColliderTrigger _trigger;
    public ColliderTrigger trigger
    {
        get { return _trigger; }
    }
#endregion

#region _Method_
    // 初始化
    public void InitDefend()
    {
        ChangeState(State.DefenderStart);
    }
#endregion
}

public class DefenderState_Start : FSMState<SkillElem_Defender, SkillElem_Defender.State>
{
    public override SkillElem_Defender.State StateID
    {
        get
        {
            return SkillElem_Defender.State.DefenderStart;
        }
    }
    public override void Enter()
    {
        base.Enter();
        entity._trDefenderBody.localScale = Vector3.zero;
        entity._trDefenderBody.transform.DOScale(entity._fRadius * Vector3.one, 0.2f).OnComplete(() => { entity.ChangeState(SkillElem_Defender.State.Defendering); });
    }

}

public class DefenderState_Defendering : FSMState<SkillElem_Defender, SkillElem_Defender.State>
{
    public override SkillElem_Defender.State StateID
    {
        get
        {
            return SkillElem_Defender.State.Defendering;
        }
    }

    float _fCurDur = 0;
    float _fCurR = 0;    
    public override void Enter()
    {
        base.Enter();
        entity.trigger._actionTriggerEnter += OnSthEnter;
        entity.trigger._actionTriggerStay += OnSthStay;
        _fCurDur = entity._fDur;
        _fCurR = entity._fRadius;
        entity._trDefenderBody.localScale = Vector3.one * _fCurR;
    }

    public override void Execute()
    {
        base.Execute();
        _fCurDur -= Time.deltaTime;
        if (_fCurDur <= 0)
        {
            entity.ChangeState(SkillElem_Defender.State.DefendEnd);
        }
        else
        {
            _fCurR = Mathf.Lerp(entity._fMinRadius, entity._fRadius, _fCurDur / entity._fDur);
            entity._trDefenderBody.localScale = Vector3.one * _fCurR;
        }
    }

    public override void Exit()
    {
        base.Exit();
        entity.trigger._actionTriggerEnter -= OnSthEnter;
        entity.trigger._actionTriggerStay -= OnSthStay;
    }

    void OnSthEnter(GameObject go)
    {
        if (ToolsUseful.CheckLayerContainedGo(entity._defendLayer, go))
        {
            BaseLifeCom lifeCom = go.GetComponent<BaseLifeCom>();
            if (lifeCom != null)
            {
                lifeCom.AddValue(-entity._nHurtPerTrigger);
            }
        }
    }

    void OnSthStay(GameObject go)
    {
        if (ToolsUseful.CheckLayerContainedGo(entity._defendLayer, go))
        {
            BaseLifeCom lifeCom = go.GetComponent<BaseLifeCom>();
            if (lifeCom != null)
            {
                lifeCom.AddValue(-entity._nHurtPerTrigger);
            }
        }
    }
}

public class DefenderState_DefendEnd : FSMState<SkillElem_Defender, SkillElem_Defender.State>
{
    public override SkillElem_Defender.State StateID
    {
        get
        {
            return SkillElem_Defender.State.DefendEnd;
        }
    }

    public override void Enter()
    {
        base.Enter();
        // 结束动画 todo

        entity.ChangeState(SkillElem_Defender.State.Disable);
    }
}

public class DefenderState_DefendDisable : FSMState<SkillElem_Defender, SkillElem_Defender.State>
{
    public override SkillElem_Defender.State StateID
    {
        get
        {
            return SkillElem_Defender.State.Disable;
        }
    }

    public override void Enter()
    {
        base.Enter();
        // 删除吧？ todo
        ObjectPoolController.Destroy(entity.gameObject);
    }
}


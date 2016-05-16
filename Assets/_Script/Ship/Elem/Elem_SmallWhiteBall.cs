using UnityEngine;
using System.Collections;
using sunjiahaoz;
using DG.Tweening;

public class Elem_SmallWhiteBall : BaseEnemyShip
{
    #region _State_
    public enum State
    {
        None,
        ToScatterPos,
        ToParent,
    }

    SmallWhitBallState_None _stateNone = new SmallWhitBallState_None();
    SmallWhitBallState_ToParent _stateToParent = new SmallWhitBallState_ToParent();
    SmallWhitBallState_ToScatterPos _stateToScatterPos = new SmallWhitBallState_ToScatterPos();

    FiniteStateMachine<Elem_SmallWhiteBall, Elem_SmallWhiteBall.State> _FSM = null;
    public FiniteStateMachine<Elem_SmallWhiteBall, Elem_SmallWhiteBall.State> FSM
    {
        get { return _FSM; }
    }
    void FSMInit()
    {
        _FSM = new FiniteStateMachine<Elem_SmallWhiteBall, State>(this);
        FSM.RegisterState(_stateNone);
        FSM.RegisterState(_stateToParent);
        FSM.RegisterState(_stateToScatterPos);
    }

    protected override void Awake()
    {
        base.Awake();
        FSMInit();
    }
    #endregion
    public float _fToParentSpeed = 10f;
    float _fInhaleDest = 100;
    public float InhaleDest
    {
        get { return _fInhaleDest; }
    }
    float _fCombineDest = 30;
    public float CombineDest
    {
        get { return _fCombineDest; }
    }
    Vector3 _posToScatterPos = Vector3.zero;
    public Vector3 PotToScatter
    {
        get { return _posToScatterPos; }
    }

    float _posToScatterDur = 0;
    public float DurToScatterPos
    {
        get { return _posToScatterDur; }
    }

    Enemy_WhiteBall _parent;
    public Enemy_WhiteBall parentBall
    {
        get { return _parent; }
    }

    public void Init(Enemy_WhiteBall whiteBall,
        Vector3 toPos, float fToPosDur,
        float fInhaleDest,
        float fCombineDest)
    {
        _parent = whiteBall;
        _posToScatterPos = toPos;
        _posToScatterDur = fToPosDur;
        _fInhaleDest = fInhaleDest;
        _fCombineDest = fCombineDest;
        FSM.ChangeState(State.ToScatterPos);
    }

    void Update()
    {
        FSM.Update();
    }

    public override void OnThingDestroy()
    {
        base.OnThingDestroy();
        Debug.LogWarning("<color=orange>[Warning]</color>---" + "woca !!!!!!!!!!!!!!!!");
    }
}

public class SmallWhitBallState_None : FSMState<Elem_SmallWhiteBall, Elem_SmallWhiteBall.State>
{
    public override Elem_SmallWhiteBall.State StateID
    {
        get
        {
            return Elem_SmallWhiteBall.State.None;
        }
    }
}

public class SmallWhitBallState_ToScatterPos : FSMState<Elem_SmallWhiteBall, Elem_SmallWhiteBall.State>
{
    public override Elem_SmallWhiteBall.State StateID
    {
        get
        {
            return Elem_SmallWhiteBall.State.ToScatterPos;
        }
    }

    public override void Enter()
    {
        base.Enter();
        entity.transform.DOMove(entity.PotToScatter, entity.DurToScatterPos)
            .OnComplete(() =>
            {
                entity.FSM.ChangeState(Elem_SmallWhiteBall.State.ToParent);
            });
    }
}
public class SmallWhitBallState_ToParent : FSMState<Elem_SmallWhiteBall, Elem_SmallWhiteBall.State>
{
    public float _fInhaleSpeedRadio = 10;

    public override Elem_SmallWhiteBall.State StateID
    {
        get
        {
            return Elem_SmallWhiteBall.State.ToParent;
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
        if (entity.parentBall != null)
        {
            float fDest = Vector3.Distance(entity.transform.position, entity.parentBall.transform.position);
            float fMoveSpeed = entity._fToParentSpeed;
            if (fDest >= entity.InhaleDest)
            {
                float fRadios = Mathf.Lerp(0.5f, 1f, fDest / entity.parentBall._fScatterAreaRadius);
                fMoveSpeed *= fRadios;
            }
            else
            {
                fMoveSpeed *= _fInhaleSpeedRadio;
            }
            Vector3 dir = ToolsUseful.LookDir(entity.transform.position, entity.parentBall.transform.position);
            entity.transform.Translate(dir * (Time.deltaTime * fMoveSpeed));

            if (fDest <= entity.CombineDest)
            {
                entity.parentBall.OnInHale(entity);
            }
        }
    }
}

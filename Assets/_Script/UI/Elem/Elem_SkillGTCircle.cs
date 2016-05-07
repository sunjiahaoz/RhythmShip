using UnityEngine;
using System.Collections;
using DG.Tweening;
using sunjiahaoz;

public class Elem_SkillGTCircle : MonoBehaviour //FSMMono<Elem_SkillGTCircle, Elem_SkillGTCircle.State> {    
{
    public float _fRotateConsumeSpeed = 100;
    public float _fRotateRecoverSpeed = -100;

    PlayerShipSkillGuideTypeBase _guideSkill = null;
    float _fLastValue = 0;
    public void Init(PlayerShipSkillGuideTypeBase guideSkill)
    {
        _guideSkill = guideSkill;
        _fLastValue = guideSkill.CurFill;
    }

    Vector3 _vecToHide = Vector3.zero;
    Vector3 _vecToShow = Vector3.zero;


    float _fScaleProgress = 1;
    float _fScaleSpeed = 100;
    void Update()
    {
        if (_guideSkill == null)
        {
            return;
        }

        float fRotateSpeed = 0;
        if (_guideSkill.CurFill > _fLastValue)
        {
            fRotateSpeed = _fRotateRecoverSpeed;
            _fLastValue = _guideSkill.CurFill;            
        }
        else if (_guideSkill.CurFill <_fLastValue)
        {
            fRotateSpeed = _fRotateConsumeSpeed;
            _fLastValue = _guideSkill.CurFill;            
        }
        else
        {
            
        }
        transform.Rotate(0, 0, Time.deltaTime * fRotateSpeed);
    }
//#region _State_
//    public enum State
//    {
//        ToShow,     
//        Showing,
//        ToHide,
//        Hiding,
//    }

//    GTCircleState_Hiding _stateHiding = new GTCircleState_Hiding();
//    GTCircleState_Showing _stateShowing = new GTCircleState_Showing();
//    GTCircleState_ToHide _stateToHide = new GTCircleState_ToHide();
//    GTCircleState_ToShow _stateToShow = new GTCircleState_ToShow();
//    public override void FSMInit()
//    {
//        base.FSMInit();
//        FSM = new FiniteStateMachine<Elem_SkillGTCircle, State>(this);
//        FSM.RegisterState(_stateHiding);
//        FSM.RegisterState(_stateShowing);
//        FSM.RegisterState(_stateToHide);
//        FSM.RegisterState(_stateToShow);
//    }

//    void Awake()
//    {
//        FSMInit();
//    }
//#endregion

//    public UISprite _sp;
//    public ObjectAnim_Rotate _comRotate;
//    public ObjectAnim_Scale _comScale;

//    PlayerShipSkillGuideTypeBase _guideSkill = null;
//    public PlayerShipSkillGuideTypeBase guideSkill
//    {
//        get { return _guideSkill; }
//    }
//   public void Init(PlayerShipSkillGuideTypeBase guideSkill)
//    {
//        _guideSkill = guideSkill;
//        ChangeState(State.Hiding);
//    }

//    void Update()
//    {
//        FSM.Update();
//    }
}

//public class GTCircleState_ToShow : FSMState<Elem_SkillGTCircle, Elem_SkillGTCircle.State>
//{
//    public override Elem_SkillGTCircle.State StateID
//    {
//        get
//        {
//            return Elem_SkillGTCircle.State.ToShow;
//        }
//    }

//    public override void Enter()
//    {
//        base.Enter();
//        // 显示出来
//        entity._sp.enabled = true;
//        // 执行动画, 放大
//        entity._comScale._startValue = Vector3.zero;
//        entity._comScale._endValue = Vector3.one;
//        entity._comScale.Run();
//        entity._comScale._actionComplete = () => 
//        {
//            entity.ChangeState(Elem_SkillGTCircle.State.Showing);
//        };        
//    }
//}
//public class GTCircleState_ToHide : FSMState<Elem_SkillGTCircle, Elem_SkillGTCircle.State>
//{
//    public override Elem_SkillGTCircle.State StateID
//    {
//        get
//        {
//            return Elem_SkillGTCircle.State.ToHide;
//        }
//    }

//    public override void Enter()
//    {
//        base.Enter();        
//        // 执行动画, 缩小
//        entity._comScale._startValue = Vector3.one;
//        entity._comScale._endValue = Vector3.zero;
//        entity._comScale.Run();
//        entity._comScale._actionComplete = () =>
//        {
//            entity.ChangeState(Elem_SkillGTCircle.State.Hiding);
//        };
//        // 旋转
//        entity._comRotate._endValue = new Vector3(0, 0, -360);
//        entity._comRotate.Run();
//    }
//}
//public class GTCircleState_Showing : FSMState<Elem_SkillGTCircle, Elem_SkillGTCircle.State>
//{
//    public override Elem_SkillGTCircle.State StateID
//    {
//        get
//        {
//            return Elem_SkillGTCircle.State.Showing;
//        }
//    }

//    Vector3 _endValueRecover = new Vector3(0, 0, 360);
//    Vector3 _endValueConsume = new Vector3(0, 0, -360);
//    bool _bCurIsConsume = true;
//    public override void Enter()
//    {
//        base.Enter();
//        entity.guideSkill._event._eventOnCurEnergyValueChange += _event__eventOnCurEnergyValueChange;

//        entity._comRotate._endValue = _endValueConsume;
//        _bCurIsConsume = true;
//        entity._comRotate.Run();
//    }

//    public override void Execute()
//    {
//        base.Execute();
//        if (Mathf.Approximately(1f, entity.guideSkill.GetCurEnergyPercent()))
//        {
//            entity.ChangeState(Elem_SkillGTCircle.State.ToHide);
//        }
//    }

//    void _event__eventOnCurEnergyValueChange(float fOff)
//    {
//        if (fOff < 0)
//        {
//            if (!_bCurIsConsume)
//            {
//               entity._comRotate.curTw.ChangeEndValue(_endValueConsume, false);   
//            }            
//        }
//        else
//        {
//            if (_bCurIsConsume)
//            {
//                entity._comRotate.curTw.ChangeEndValue(_endValueRecover, false);   
//            }
//        }
//    }

//    public override void Exit()
//    {
//        base.Exit();
//        entity.guideSkill._event._eventOnCurEnergyValueChange -= _event__eventOnCurEnergyValueChange;
//    }
//}
//public class GTCircleState_Hiding : FSMState<Elem_SkillGTCircle, Elem_SkillGTCircle.State>
//{
//    public override Elem_SkillGTCircle.State StateID
//    {
//        get
//        {
//            return Elem_SkillGTCircle.State.Hiding;
//        }
//    }

//    public override void Enter()
//    {
//        base.Enter();
//        entity._comRotate.Stop();
//        entity._sp.enabled = false;
//        entity.guideSkill._event._eventOnCurEnergyValueChange += _event__eventOnCurEnergyValueChange;
//    }

//    public override void Exit()
//    {
//        base.Exit();
//        entity.guideSkill._event._eventOnCurEnergyValueChange += _event__eventOnCurEnergyValueChange;
//    }

//    void _event__eventOnCurEnergyValueChange(float fOff)
//    {
//        entity.ChangeState(Elem_SkillGTCircle.State.ToShow);
//    }
//}

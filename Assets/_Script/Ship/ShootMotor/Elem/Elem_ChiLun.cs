using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz.SteerTrack;
using sunjiahaoz;

public class Elem_ChiLun : MonoBehaviour {
    public float _fRotateSpeed = 100;
    public float _fMaxSpeed = 100;

    public float _fEnergyAddSpeed = 100;
    public float _fEnergySubSpeed = 200;
    public LayerMask _checkLayer;
    public int _nHurtValuePerFrame = 1;
    public Transform _body;
    public Transform _trPosRoot;

    SteeringFollowTrPtPath _followPath;
    SteeringAgent _sAgent;
    List<Transform> _lstTradePos = new List<Transform>();

    [SerializeField]
    float _fCurEnergy = 0;    
    float _fEergyMax = 100;

    ColliderTrigger _bodyTrigger = null;
    void Awake()
    {
        _followPath = GetComponent<SteeringFollowTrPtPath>();
        _sAgent = _followPath.Agent;
        _sAgent.MaxVelocity = 0;

        _bodyTrigger = _body.GetComponent<ColliderTrigger>();
        _bodyTrigger._actionTriggerStay += OnEnemyEnter;
    }    

    void OnDestroy()
    {
        _bodyTrigger._actionTriggerStay -= OnEnemyEnter;
    }
    public void GenerateTradePos()
    {
        _lstTradePos.Clear();
        for (int i = 0; i < _trPosRoot.childCount; ++i)
        {
            _lstTradePos.Add(_trPosRoot.GetChild(i));
        }
        _followPath.Path = _lstTradePos.ToArray();
        transform.position = _lstTradePos[0].position;
    }

    void Update()
    {
        float fProgress = _fCurEnergy / _fEergyMax;
        Update_Rotate(fProgress);
        Update_Move(fProgress);
        if (!Input.GetMouseButton(0))
        {
            Update_SubEnergy();
        }        
    }
    
    void Update_Rotate(float fProgress)
    {
        float fRotateSpeed = Mathf.Lerp(0, _fRotateSpeed, fProgress);
        _body.transform.Rotate(0, 0, fRotateSpeed * Time.deltaTime);
    }

    void Update_Move(float fProgress)
    {       
        float fMoveSpeed = Mathf.Lerp(0, _fMaxSpeed, fProgress);
        _sAgent.MaxVelocity = fMoveSpeed;
    }

    void Update_SubEnergy()
    {
        if (_fCurEnergy == 0)
        {
            return;
        }
        _fCurEnergy -= (_fEnergySubSpeed * Time.deltaTime);
        _fCurEnergy = Mathf.Max(0, _fCurEnergy);
    }
    
    public void PushEnergy()
    {
        _fCurEnergy += (_fEnergyAddSpeed * Time.deltaTime);
        _fCurEnergy = Mathf.Min(_fCurEnergy, _fEergyMax);
        TagLog.Log(LogIndex.FirePoint, "PushEnergy:" + _fCurEnergy);
    }

    void OnEnemyEnter(GameObject go)
    {
        if (ToolsUseful.CheckLayerContainedGo(_checkLayer, go))
        {
            BaseLifeCom lifeCom = go.GetComponentInChildren<BaseLifeCom>();
            if (lifeCom != null)
            {
                lifeCom.AddValue(-_nHurtValuePerFrame);
            }
        }
    }

}

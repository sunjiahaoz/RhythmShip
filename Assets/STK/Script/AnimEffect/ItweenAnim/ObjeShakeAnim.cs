using UnityEngine;
using System.Collections;
namespace sunjiahaoz
{
    public enum RunOpportunity
    {
        Awake,
        Start,
        Enable,
        Manual,
    }
    public class ObjeShakeAnim : MonoBehaviour
    {
        public RunOpportunity _runOpp;
        public Vector3 _shakePosition = Vector3.zero;
        public Vector3 _shakeRotate = Vector3.zero;
        public Vector3 _shakeScale = Vector3.zero;
        public float _fDelay = 0;
        public float _fDur = 0.2f;
        public iTween.LoopType _loopType = iTween.LoopType.loop;
        public iTween.EaseType _type = iTween.EaseType.spring;
        public bool _bIgnoreTimeScale = false;


        private Vector3 _posSrc = Vector3.zero;
        private Quaternion _rotateSrc = Quaternion.identity;
        private Vector3 _scaleSrc = Vector3.one;
        void Awake()
        {
            _posSrc = transform.position;
            _scaleSrc = transform.localScale;
            _rotateSrc = transform.localRotation;

            if (_runOpp == RunOpportunity.Awake)
            {
                Run();
            }
        }

        void OnEnable()
        {
            if (_runOpp == RunOpportunity.Enable)
            {
                Run();
            }
        }

        void Start()
        {
            if (_runOpp == RunOpportunity.Start)
            {
                Run();
            }
        }

        public void Run()
        {
            if (_shakePosition != Vector3.zero)
            {
                iTween.ShakePosition(gameObject, iTween.Hash(
                "ignoretimescale", _bIgnoreTimeScale,
                "amount", _shakePosition,
                "delay", _fDelay,
                "time", _fDur,
                "easetype", _type,
                "looptype", _loopType,
                "oncomplete", "OnCompletePosition",
                "oncompletetarget", gameObject));
            }

            if (_shakeRotate != Vector3.zero)
            {
                iTween.ShakeRotation(gameObject, iTween.Hash(
                "ignoretimescale", _bIgnoreTimeScale,
                "amount", _shakeRotate,
                "delay", _fDelay,
                "time", _fDur,
                "easetype", _type,
                "looptype", _loopType,
                "oncomplete", "OnCompleteRotation",
                "oncompletetarget", gameObject));
            }

            if (_shakeScale != Vector3.zero)
            {
                iTween.ShakeScale(gameObject, iTween.Hash(
                "ignoretimescale", _bIgnoreTimeScale,
                "amount", _shakeScale,
                "delay", _fDelay,
                "time", _fDur,
                "easetype", _type,
                "looptype", _loopType,
                "oncomplete", "OnCompleteScale",
                "oncompletetarget", gameObject));
            }
        }

        void OnCompletePosition()
        {
            transform.position = _posSrc;            
        }

        void OnCompleteRotation()
        {
            transform.localRotation = _rotateSrc;            
        }

        void OnCompleteScale()
        {
            transform.localScale = _scaleSrc;            
        }
    }
}

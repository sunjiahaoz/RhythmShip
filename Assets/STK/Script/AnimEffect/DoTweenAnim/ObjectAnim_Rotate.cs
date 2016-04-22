using UnityEngine;
using System.Collections;
using DG.Tweening;
using DG;

namespace sunjiahaoz
{
    public class ObjectAnimBase : MonoBehaviour
    {
        public RunOpportunity _opt = RunOpportunity.Start;

        void Awake()
        {
            if (_opt == RunOpportunity.Awake)
            {
                Run();
            }
        }

        void Start()
        {
            if (_opt == RunOpportunity.Start)
            {
                Run();
            }
        }

        void OnEnable()
        {
            if (_opt == RunOpportunity.Enable)
            {
                Run();
            }
        }

        public virtual void Run()
        {

        }
    }
    public class ObjectAnim_Rotate : ObjectAnimBase
    {        
        public Vector3 _endValue= Vector3.zero;
        public float _fDelay = 0;
        public float _fDur = 1f;
        public RotateMode _mode = RotateMode.Fast;
        public int _nLoopCount = 1;
        public LoopType _loopType = LoopType.Restart;
        public Ease _ease = Ease.Linear;
        public TweenCallback _actionComplete = null;

        public override void Run()
        {
            Tweener tween = transform.DORotate(_endValue, _fDur, _mode)
                .SetLoops(_nLoopCount, _loopType)
                .SetEase(_ease);

            if (_fDelay > 0)
            {
                tween.SetDelay(_fDelay);
            }
            if (_actionComplete != null)
            {
                tween.OnComplete(_actionComplete);
            }
        }
    }
}

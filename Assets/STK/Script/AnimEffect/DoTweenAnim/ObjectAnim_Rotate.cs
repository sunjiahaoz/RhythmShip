using UnityEngine;
using System.Collections;
using DG.Tweening;
using DG;

namespace sunjiahaoz
{   
    public class ObjectAnim_Rotate : ObjectAnimBase
    {
        public Transform _trTarget;
        public Vector3 _endValue= Vector3.zero;
        public float _fDelay = 0;
        public float _fDur = 1f;
        public RotateMode _mode = RotateMode.Fast;
        public int _nLoopCount = 1;
        public LoopType _loopType = LoopType.Restart;
        public Ease _ease = Ease.Linear;
        public bool _bNeedRunBack = false;
        public TweenCallback _actionComplete = null;


        Tweener _tw = null;
        public Tweener curTw
        {
            get { return _tw; }
        }

        protected override void Awake()
        {
            if (_trTarget == null)
            {
                _trTarget = transform;
            }
            base.Awake();
        }
        public override void Run()
        {
            _tw = _trTarget.DORotate(_endValue, _fDur, _mode)
                .SetLoops(_nLoopCount, _loopType)
                .SetEase(_ease);

            if (_fDelay > 0)
            {
                _tw.SetDelay(_fDelay);
            }
            if (_actionComplete != null)
            {
                _tw.OnComplete(_actionComplete);
            }
            if (_bNeedRunBack)
            {
                _tw.SetAutoKill(false);
            }
        }

        public override void Stop(bool bComplete = false)
        {
            base.Stop(bComplete);
            _tw.Kill(bComplete);
        }

        public override void RunBack()
        {
            base.RunBack();
            if (_tw != null)
            {
                _tw.PlayBackwards();
            }
        }
        
    }
}

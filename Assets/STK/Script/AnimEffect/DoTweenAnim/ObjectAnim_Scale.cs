using UnityEngine;
using System.Collections;
using DG.Tweening;
namespace sunjiahaoz
{
    public class ObjectAnim_Scale : ObjectAnimBase
    {
        public Transform _trTarget;
        public Vector3 _startValue = Vector3.one;
        public Vector3 _endValue = Vector3.one;
        public float _fDelay = 0;
        public float _fDur = 1f;
        public int _nLoopCount = 1;
        public LoopType _loopType = LoopType.Restart;
        public Ease _ease = Ease.Linear;
        public bool _bNeedRunBack = false;
        public TweenCallback _actionComplete = null;

        protected override void Awake()
        {
            if (_trTarget == null)
            {
                _trTarget = transform;
            }
            base.Awake();            
        }

        Tweener _tw = null;
        public override void Run()
        {
            _trTarget.localScale = _startValue;

            _tw = _trTarget.DOScale(_endValue, _fDur)
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

        public override void RunBack()
        {
            base.RunBack();
            if (_tw != null)
            {
                _tw.PlayBackwards();
            }
        }

        public override void Stop(bool bComplete = false)
        {
            base.Stop();
            _tw.Kill(bComplete);
        }
    }
}

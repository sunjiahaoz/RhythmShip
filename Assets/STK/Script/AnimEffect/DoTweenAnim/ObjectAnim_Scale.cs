using UnityEngine;
using System.Collections;
using DG.Tweening;
namespace sunjiahaoz
{
    public class ObjectAnim_Scale : ObjectAnimBase
    {
        public Vector3 _startValue = Vector3.one;
        public Vector3 _endValue = Vector3.one;
        public float _fDelay = 0;
        public float _fDur = 1f;
        public int _nLoopCount = 1;
        public LoopType _loopType = LoopType.Restart;
        public Ease _ease = Ease.Linear;
        public TweenCallback _actionComplete = null;

        Tweener _tw = null;
        public override void Run()
        {
            transform.localScale = _startValue;

            _tw = transform.DOScale(_endValue, _fDur)
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
        }

        public override void Stop(bool bComplete = false)
        {
            base.Stop();
            _tw.Kill(bComplete);
        }
    }
}

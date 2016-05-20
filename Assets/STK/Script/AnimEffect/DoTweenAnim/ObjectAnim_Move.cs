using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace sunjiahaoz
{
    public class ObjectAnim_Move : ObjectAnimBase
    {
        public Vector3 _endValue = Vector3.one;
        public float _fDelay = 0;
        public float _fDur = 1f;
        public int _nLoopCount = 1;
        public LoopType _loopType = LoopType.Restart;
        public Ease _ease = Ease.Linear;
        public TweenCallback _actionComplete = null;

        Tweener _tw = null;
        public Tweener twner
        {
            get { return _tw; }
        }
        public override void Run()
        {
            base.Run();
            _tw = transform.DOLocalMove(_endValue, _fDur)
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
            if (_tw != null)
            {
                _tw.Kill(bComplete);
            }            
        }

#region _Menu_
        [ContextMenu("Goal为当前位置")]
        void SetGoalPos()
        {
            _endValue = transform.localPosition;
        }
#endregion
    }
}

using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace sunjiahaoz
{
    public class ObjectAnim_Move : ObjectAnimBase
    {
        public Transform _trTarget;
        public Vector3 _endValue = Vector3.one;
        public float _fDelay = 0;
        public float _fDur = 1f;
        public int _nLoopCount = 1;
        public LoopType _loopType = LoopType.Restart;
        public Ease _ease = Ease.Linear;
        public TweenCallback _actionComplete = null;

        [Header("是否会用到Runback,不会用到就设为false")]
        public bool _bUseRunBack = false;

        Tweener _tw = null;
        public Tweener twner
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
            base.Run();
            _tw = _trTarget.DOLocalMove(_endValue, _fDur)
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
            _tw.SetAutoKill(!_bUseRunBack);
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
            if (_tw != null)
            {
                _tw.Kill(bComplete);
            }            
        }

#region _Menu_
        [ContextMenu("Goal为当前位置")]
        void SetGoalPos()
        {
            if (_trTarget == null)
            {
                _endValue = transform.localPosition;
            }
            else
            {
                _endValue = _trTarget.localPosition;
            }            
        }
#endregion
    }
}

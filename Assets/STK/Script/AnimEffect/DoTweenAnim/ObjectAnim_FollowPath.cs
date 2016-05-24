using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace sunjiahaoz
{
    public class ObjectAnim_FollowPath : ObjectAnimBase
    {
        public Transform _trTarget;
        public Vector3[] _path;
        public float _fDelay = 0;
        public float _fDur = 1f;
        public PathType _pathType = PathType.Linear;
        public PathMode _pathMode = PathMode.Ignore;
        public int _resolution = 10;    // pathType为非Linera时有效，表示曲线的平滑(?)程度
        public int _nLoopCount = 1;
        public LoopType _loopType = LoopType.Restart;
        public Ease _ease = Ease.Linear;
        public bool _bNeedRunBack = false;
        public TweenCallback _actionComplete = null;
        public TweenCallback<int> _actionWayPointChange = null;


        Tweener _tw = null;

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
            _tw = _trTarget.DOPath(_path, _fDur, _pathType, _pathMode, _resolution, Color.blue).
                SetLoops(_nLoopCount, _loopType)
                .SetEase(_ease);
            if (_actionWayPointChange != null)
            {
                _tw.OnWaypointChange(_actionWayPointChange);
            }
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
    }
}

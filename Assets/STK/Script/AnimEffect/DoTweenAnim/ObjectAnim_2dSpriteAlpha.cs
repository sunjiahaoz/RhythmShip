using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace sunjiahaoz
{
    public class ObjectAnim_2dSpriteAlpha : ObjectAnimBase
    {
        public tk2dBaseSprite _sprite;
        public float _fAlphaStart = 1;
        public float _fAlphaTarget = 0;
        public float _fDelay = 0;
        public float _fDur = 1f;
        public int _nLoopCount = 1;
        public LoopType _loopType = LoopType.Restart;
        public Ease _ease = Ease.Linear;
        public TweenCallback _actionComplete = null;

        Tweener _twner = null;
        public Tweener curTw
        {
            get { return _twner; }
        }
        
        public override void Run()
        {
            if (_sprite == null)
            {
                TagLog.LogError(-1, "ObjectAnim_2dSpriteAlpha 需要 tk2dBaseSprite", this);
                return;
            }
            base.Run();
            _twner = DOTween.To((value)=>
            {
                Color color = _sprite.color;
                color.a = value;
                _sprite.color = color;
            }, _fAlphaStart, _fAlphaTarget, _fDur)
                 .SetLoops(_nLoopCount, _loopType)
                .SetEase(_ease);

            if (_fDelay > 0)
            {
                _twner.SetDelay(_fDelay);
            }
            if (_actionComplete != null)
            {
                _twner.OnComplete(_actionComplete);
            }
        }

        public override void Stop(bool bComplete = false)
        {
            base.Stop(bComplete);
            if (_twner != null)
            {
                _twner.Kill(bComplete);
            }
        }

        [ContextMenu("当前Alpha为Start")]
        void SetCurIsStart()
        {
            if (_sprite != null)
            {
                _fAlphaStart = _sprite.color.a;
            }
        }
        [ContextMenu("当前Alpha为Target")]
        void SetCurIsTarget()
        {
            if (_sprite != null)
            {
                _fAlphaTarget = _sprite.color.a;
            }
        }
    }
}

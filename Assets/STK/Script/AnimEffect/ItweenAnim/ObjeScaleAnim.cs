using UnityEngine;
using System.Collections;

namespace sunjiahaoz
{

    public class ObjeScaleAnim : ObjectAnimBase
    {
        public Vector3 _vecStartScale = Vector3.zero;
        public Vector3 _vecGoalScale = Vector3.one;
        public float _fDelay = 0;
        public float _fDuration = 0.5f;
        public iTween.LoopType _loopType = iTween.LoopType.loop;
        public iTween.EaseType _type = iTween.EaseType.spring;
        public bool _bIgnoreTimeScale = false;

        public override void Run()
        {
            transform.localScale = _vecStartScale;
            iTween.ScaleTo(gameObject, iTween.Hash(
                "ignoretimescale", _bIgnoreTimeScale, 
                "scale", _vecGoalScale, 
                "delay", _fDelay, 
                "time", _fDuration, 
                "easetype", _type, 
                "looptype", _loopType));
        }
    }
}

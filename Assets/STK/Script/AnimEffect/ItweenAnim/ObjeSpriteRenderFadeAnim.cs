using UnityEngine;
using System.Collections;

namespace sunjiahaoz
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class ObjeSpriteRenderFadeAnim : MonoBehaviour
    {
        public Color _startColor = Color.white;
        public Color _endColor = Color.white;
        public float _fDelay = 0f;
        public float _fDuration = 0.5f;
        public iTween.LoopType _loopType = iTween.LoopType.pingPong;
        public iTween.EaseType _type = iTween.EaseType.linear;
        public bool _bRunStart = true;
        public bool _bIgnoreTimeScale = false;

        SpriteRenderer _comSp;
        void Awake()
        {
            _comSp = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            if (_bRunStart)
            {
                Run();
            }
        }

        void Run()
        {
            _comSp.color = _startColor;
            iTween.ColorTo(_comSp.gameObject, iTween.Hash("ignoretimescale", _bIgnoreTimeScale, "color", _endColor, "time", _fDuration, "delay", _fDelay, "easetype", _type, "looptype", _loopType));
        }
    }
}

/*
*DepthPyramidEffect
*by sunjiahaoz 2016-5-16
*
*深度叠罗汉效果。。这个名字 *
*/
using UnityEngine;
using System.Collections;

namespace sunjiahaoz
{
    public class DepthPyramidEffect : MonoBehaviour
    {
        public Transform _firstChild;
        public int _nCount;
        public float _fOffAngle = 5;
        public float _fOffScale = 0.2f;
        public bool _bAlpha = false;

        public Vector3 _radioPos = Vector3.one * 0.8f;
        public Vector3 _radioScale = Vector3.one;
        public Vector3 _radioAngle = Vector3.one * 0.8f;

        ScrollEffectCombine[] _sdChildren = null;

        void Awake()
        {            
            //GenerateAngle();
        }

        [ContextMenu("生成Prefab")]
        void GenerateSE()
        {
            float fAlpha = 1f / (float)_nCount;
            Transform trLast = _firstChild;
            for (int i = 0; i < _nCount; ++i )
            {
                GameObject go = ObjectPoolController.Instantiate(_firstChild.gameObject, transform.position, Quaternion.identity);
                go.transform.parent = transform;              
                ScrollEffectCombine se = ToolsUseful.DefaultGetComponent<ScrollEffectCombine>(go);
                se._trTarget = trLast;
                se._angleRadio = _radioAngle;
                se._posRadio = _radioPos;
                se._scaleRadio = _radioScale;
                trLast = se.transform;

                if (_bAlpha)
                {
                    tk2dBaseSprite sp = go.GetComponent<tk2dBaseSprite>();
                    Color color = sp.color;
                    color.a = Mathf.Lerp(1f, 0f, (float)i / (float)_nCount);
                    sp.color = color;
                }
            }
        }

        [ContextMenu("重新计算Alpha")]
        void GenerateAlpha()
        {
            _sdChildren = GetComponentsInChildren<ScrollEffectCombine>();
            for (int i = 0; i < _sdChildren.Length; ++i)
            {
                tk2dBaseSprite sp = _sdChildren[i].GetComponent<tk2dBaseSprite>();
                Color color = sp.color;
                color.a = Mathf.Lerp(1f, 0f, (float)i / (float)_nCount);
                sp.color = color;
            }
        }

        [ContextMenu("旋转角度")]
        void GenerateAngle()
        {            
            _sdChildren = GetComponentsInChildren<ScrollEffectCombine>();
            for (int i = 0; i < _sdChildren.Length; ++i )
            {
                _sdChildren[i].transform.Rotate(0, 0, _fOffAngle * i);
            }
        }

        [ContextMenu("缩放设置")]
        void GenerageScale()
        {
            Vector3 scale = Vector3.zero;
            _sdChildren = GetComponentsInChildren<ScrollEffectCombine>();
            for (int i = 0; i < _sdChildren.Length; ++i)
            {
                scale = _sdChildren[i].transform.localScale;
                scale.x += _fOffScale * i;
                scale.y += _fOffScale * i;
                _sdChildren[i].transform.localScale = scale;
            }
        }

        [ContextMenu("重置角度为0")]
        void ResetToAngle0()
        {
            _sdChildren = GetComponentsInChildren<ScrollEffectCombine>();            
            for (int i = 0; i < _sdChildren.Length; i++)
            {
                _sdChildren[i].transform.localEulerAngles = Vector3.zero;
            }
        }
        [ContextMenu("重置缩放为1")]
        void ResetToScale1()
        {
            _sdChildren = GetComponentsInChildren<ScrollEffectCombine>();
            for (int i = 0; i < _sdChildren.Length; i++)
            {
                _sdChildren[i].transform.localScale = Vector3.one;
            }
        }
    }
}

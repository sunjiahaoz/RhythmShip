/*
*DepthPyramidEffect
*by sunjiahaoz 2016-5-16
*
*深度叠罗汉效果。。这个名字 *
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace sunjiahaoz
{
    public class DepthPyramidEffect : MonoBehaviour
    {
        public bool _bDynamic = false;
        public Transform _firstChild;
        public Transform _prefabEntity;
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
            if (_prefabEntity == null)
            {
                _prefabEntity = _firstChild;
            }

            _sdChildren = GetComponentsInChildren<ScrollEffectCombine>();
            if (_bDynamic)
            {
                for (int i = 0; i < _sdChildren.Length; ++i )
                {
                    OpenEntity(_sdChildren[i], false);                    
                }
            }
        }

        int _nOpenedIndex = 0;
        public void Add()
        {
            if (_nOpenedIndex >= _sdChildren.Length)
            {
                return;
            }
            OpenEntity(_sdChildren[_nOpenedIndex], true);            
            _nOpenedIndex++;
        }

        void OpenEntity(ScrollEffectCombine entity, bool bOpen)
        {
            entity.GetComponent<Renderer>().enabled = bOpen;            
        }

        // TEST CODE
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Home))
            {
                Add();
            }
        }

#region _动态_虽然支持动态添加什么，但会因为first处在动态中，所以动态添加会有误差存在
        //public int _nOrderOff = 1;
        //List<Transform> _lstOverLap = new List<Transform>();
        //public void Add()
        //{   
        //    Transform trTarget = null;
        //    if (_lstOverLap.Count == 0)
        //    {
        //        trTarget = _firstChild;
        //    }
        //    else
        //    {
        //        trTarget = _lstOverLap[_lstOverLap.Count - 1];
        //    }

        //    GameObject go = ObjectPoolController.Instantiate(_prefabEntity.gameObject, transform.position, Quaternion.identity);
        //    go.transform.parent = transform;
        //    go.SetActive(true);
        //    ReUpdateAngleAndScale(go.transform, _lstOverLap.Count);
        //    ScrollEffectCombine se = ToolsUseful.DefaultGetComponent<ScrollEffectCombine>(go);
        //    se._trTarget = trTarget;
        //    se._angleRadio = _radioAngle;
        //    se._posRadio = _radioPos;
        //    se._scaleRadio = _radioScale;
        //    se.enabled = false;
        //    _lstOverLap.Add(go.transform);
        //    _nCount = _lstOverLap.Count;
        //    UpdateAlpha();            
        //}
        
        //public void RemoveLast()
        //{
        //    if (_lstOverLap.Count > 0)
        //    {
        //        RemoveEntity(_lstOverLap.Count - 1);
        //    }
        //}

        //public void RemoveEntity(int nIndex)
        //{
        //    if (nIndex >= _lstOverLap.Count 
        //        || nIndex < 0)
        //    {
        //        return;
        //    }
        //    ObjectPoolController.Destroy(_lstOverLap[nIndex].gameObject);
        //    _nCount = _lstOverLap.Count;
        //    UpdateAlpha();
        //}

        //public void Clear()
        //{
        //    for (int i = 0; i < _lstOverLap.Count; ++i )
        //    {
        //        ObjectPoolController.Destroy(_lstOverLap[i].gameObject);
        //    }
        //    _nCount = 0;
        //    _lstOverLap.Clear();
        //}

        //void ReUpdateAngleAndScale(Transform trLast, int nIndex)
        //{
        //    trLast.eulerAngles = Vector3.zero;
        //    trLast.Rotate(0, 0, _fOffAngle * nIndex);

        //    // scale
        //    Vector3 scale = trLast.localScale;
        //    scale.x += _fOffScale * nIndex;
        //    scale.y += _fOffScale * nIndex;
        //    trLast.localScale = scale;
        //}
        //void UpdateAlpha()
        //{
        //    _sdChildren = GetComponentsInChildren<ScrollEffectCombine>(false);
        //    int nLastDepth = 0;
        //    Vector3 scale = Vector3.zero;
        //    for (int i = 0; i < _sdChildren.Length; ++i)
        //    {
        //        tk2dBaseSprite sp = _sdChildren[i].GetComponent<tk2dBaseSprite>();
        //        // alpha
        //        if (_bAlpha)
        //        {
        //            Color color = sp.color;
        //            color.a = Mathf.Lerp(1f, 0f, (float)i / (float)_nCount);
        //            sp.color = color;
        //        }
        //        // orderDepth
        //        sp.SortingOrder = (nLastDepth += _nOrderOff);
        //    }
        //}
#endregion

#region _Menu_

        [ContextMenu("生成Prefab")]
        void GenerateSE()
        {
            if (_prefabEntity == null)
            {
                _prefabEntity = _firstChild;
            }
            float fAlpha = 1f / (float)_nCount;
            Transform trLast = _firstChild;
            for (int i = 0; i < _nCount; ++i )
            {
                GameObject go = ObjectPoolController.Instantiate(_prefabEntity.gameObject, transform.position, Quaternion.identity);
                go.SetActive(true);
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
#endregion
    }
}

/*
PetalCom
By: @sunjiahaoz, 2016-5-20

 * 空中漂浮的花瓣模拟
 * 需要配合向下移动的组件可以实现一边模拟被风吹水平移动以及旋转，一边向下坠落
*/
using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace sunjiahaoz
{
    public class PetalCom : MonoBehaviour
    {
        [Header("在start时就执行")]
        public bool _bStart = false;
        public Transform _cdBody;
        [Header("body的Scale随机范围")]
        public FloatRange _bodyScale;        

        void Start()
        {
            float fScale = _bodyScale.RandomValue;
            _cdBody.localScale = new Vector3(fScale, fScale, 1);
            if (_bStart)
            {
                StartMove();
            }
        }
        [Header("是否启用水平移动")]
        public bool _bEnableHMove = true;
        [Header("水平移动时间")]
        public float _fMoveToHDurMin = 0.5f;
        public float _fMoveToHDurMax = 1.5f;
        [Header("水平移动距离")]
        public float _fMoveToHDistMin = 20;
        public float _fMoveToHDistMax = 40;
        public void SetMoveToHData(float fDurMin, float fDurMax, float fDistMin, float fDistMax)
        {
            _fMoveToHDurMin = fDurMin;
            _fMoveToHDurMax = fDurMax;
            _fMoveToHDistMin = fDistMin;
            _fMoveToHDistMax = fDistMax;
        }
        [Header("是否启用旋转")]
        public bool _bEnableRotate = true;
        [Header("旋转随机时间范围")]
        public float _fRotateDurMin = 3f;
        public float _fRotateDurMax = 5f;
        public void SetRotateData(float fDurMin, float fDurMax)
        {
            _fRotateDurMin = fDurMin;
            _fRotateDurMax = fDurMax;
        }

        public void StartMove()
        {
            if (_bEnableHMove)
            {
                FloatType_MoveToH(Random.Range(_fMoveToHDurMin, _fMoveToHDurMax), Random.Range(_fMoveToHDistMin, _fMoveToHDistMax));
            }
            if (_bEnableRotate)
            {
                FloatType_Rotate(Random.Range(_fRotateDurMin, _fRotateDurMax));
            }            
        }

        public void StopMove()
        {
            transform.DOKill();
            _cdBody.DOKill();
        }

        [HideInInspector]
        public float _fTmpValue = 0;
        void FloatType_MoveToH(float fDur, float fDist)
        {
            float fPer = fDist / fDur;
            Vector3 vecTmp = Vector3.zero;

            TweenParams param = new TweenParams();
            float fAngle = Random.Range(0, 100) > 50 ? 360 : -360;
            _fTmpValue = transform.position.x;
                param.SetEase(Ease.Linear)              
                .OnComplete(() =>
                {
                    FloatType_MoveToH(Random.Range(_fMoveToHDurMin, _fMoveToHDurMax), Random.Range(_fMoveToHDistMin, _fMoveToHDistMax));
                });
                DOTween.To(value => 
                {
                    _fTmpValue = value;
                    vecTmp = transform.position;
                    vecTmp.x = _fTmpValue;
                    transform.position = vecTmp;
                }, _fTmpValue, _fTmpValue + fDist, fDur).SetAs(param);
        }

        void FloatType_Rotate(float fDur)
        {
            TweenParams param = new TweenParams();
            float fAngle = Random.Range(-360, 360);
            _cdBody.DORotate(new Vector3(0, 0, fAngle), fDur).SetEase(Ease.Linear).OnComplete(() => 
            {
                FloatType_Rotate(Random.Range(_fRotateDurMin, _fRotateDurMax));
            });
        }
    }
}

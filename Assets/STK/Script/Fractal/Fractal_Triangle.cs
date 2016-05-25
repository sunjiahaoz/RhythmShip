/*
Fractal_Triangle
By: @sunjiahaoz, 2016-5-25

三角形分形
 * 取三条边的中点，这样三个中点可以组成一个新的三角形，同时还多分出了3个三角形
 * 用来演示的，要用其中的算法的话可以直接拷出去
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dest.Math;

namespace sunjiahaoz
{
    public class Fractal_Triangle : MonoBehaviour
    {
        public float _fSplitRadio = 0.5f;
        // 原始的三角形的三个点
        public Transform _trPt1;
        public Transform _trPt2;
        public Transform _trPt3;

        Triangle2 _triangle;

        void Start()
        {
            _triangle = new Triangle2(_trPt1.position, _trPt2.position, _trPt3.position);
            _lstTrig.Add(_triangle);
        }

        List<Triangle2> _lstTrig = new List<Triangle2>();

        // 每个三角形可以分裂成4个三角形
        Triangle2[] GenerateSplit(Triangle2 srcT)
        {
            Triangle2[] newT = new Triangle2[4];
            Vector3 pos1 = Vector3.Lerp(srcT.V0, srcT.V1, _fSplitRadio);
            Vector3 pos2 = Vector3.Lerp(srcT.V0, srcT.V2, _fSplitRadio);
            Vector3 pos3 = Vector3.Lerp(srcT.V1, srcT.V2, _fSplitRadio);
            newT[0] = new Triangle2(srcT.V0, pos1, pos2);
            newT[1] = new Triangle2(pos1, srcT.V1, pos3);
            newT[2] = new Triangle2(pos2, srcT.V2, pos3);
            newT[3] = new Triangle2(pos1, pos2, pos3);
            return newT;
        }

        // 测试用
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Home))
            {
                List<Triangle2> newList = new List<Triangle2>();
                for (int i = 0; i < _lstTrig.Count; ++i)
                {
                    newList.AddRange(GenerateSplit(_lstTrig[i]));
                }
                _lstTrig.AddRange(newList);
            }
        }

        void OnDrawGizmos()
        {
            for (int i = 0; i < _lstTrig.Count; ++i)
            {
                Triangle2 reft = _lstTrig[i];
                Gizmos.DrawLine(reft.V0, reft.V1);
                Gizmos.DrawLine(reft.V1, reft.V2);
                Gizmos.DrawLine(reft.V2, reft.V0);
            }
        }
    
    }
}

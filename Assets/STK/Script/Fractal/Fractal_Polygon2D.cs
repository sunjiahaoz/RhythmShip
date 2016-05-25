/*
Fractal_Polygon2D
By: @sunjiahaoz, 2016-5-25

多边形分形
 * 取一个多边形的各边的黄金分割点，组成一个新的多边形
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dest.Math;

namespace sunjiahaoz
{
    public class Fractal_Polygon2D : MonoBehaviour
    {
        public float _fRadio = 0.618f;
        public Transform[] _trPt;

        List<Polygon2> _lstPolygon = new List<Polygon2>();
        void Start()
        {
            if (_trPt.Length < 3)
            {
                TagLog.LogError(-1, "Fractal_Rectangle需要至少3个顶点");
                return;
            }
            Vector2[] v2Poses = new Vector2[_trPt.Length];
            for (int i = 0; i < v2Poses.Length; ++i)
            {
                v2Poses[i] = _trPt[i].position;
            }
            Polygon2 p = new Polygon2(v2Poses);
            _lstPolygon.Add(p);
        }

        List<Vector2> _lstTmp = new List<Vector2>();
        Polygon2 Generate(Polygon2 poly)
        {
            _lstTmp.Clear();
            for (int i = 0; i < poly.VertexCount-1; ++i )
            {
                _lstTmp.Add(Vector2.Lerp(poly[i], poly[i + 1], _fRadio));
            }
            _lstTmp.Add(Vector2.Lerp(poly[poly.VertexCount - 1], poly[0], _fRadio));
            return new Polygon2(_lstTmp.ToArray());
        }

        // 测试用

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Home))
            {
                _lstPolygon.Add(Generate(_lstPolygon[_lstPolygon.Count - 1]));
            }
        }

        void OnDrawGizmos()
        {
            for (int i = 0; i < _lstPolygon.Count; ++i )
            {
                for (int i0 = 0, i1 = _lstPolygon[i].VertexCount - 1; i0 < _lstPolygon[i].VertexCount; i1 = i0, ++i0)
                {
                    Gizmos.DrawLine(_lstPolygon[i][i0], _lstPolygon[i][i1]);
                }
            }
        }
    }
}

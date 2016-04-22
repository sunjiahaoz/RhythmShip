using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RUL;

public class UtilityTool
{
    /// <summary>
    /// 轨道计算
    /// 计算屏幕上四个点，组成两个折点的路径
    /// 第一个点与最后一个点在屏幕边缘
    /// </summary>
    /// <param name="ptScreenLT">屏幕左上角坐标（非屏幕坐标）</param>
    /// <param name="ptScrrenRB">屏幕右下角坐标（非屏幕坐标）</param>
    /// <param name="fMinWidthInterval">防止点生成在边缘</param>
    /// <param name="fMinHeightInterval">防止点生成在边缘</param>
    /// <param name="bH">是水平的还是垂直的（水平指的是中间线段是水平的还是垂直的），true为水平的</param>
    /// <returns>随机生成的四个点</returns>
    public static Vector3[] GenerateRail(Vector3 ptScreenLT, Vector3 ptScrrenRB, 
        float fMinWidthInterval, float fMinHeightInterval, 
        bool bH, bool bLeftTopIsStartSide)
    {
        Vector3 _leftTop = ptScreenLT;
        Vector3 _rightBottom = ptScrrenRB;
        float _fMinIntervalDistWidth = fMinWidthInterval;
        float _fMinIntervalDistHeight = fMinHeightInterval;

        // 点A
        Vector2 leftTop = _leftTop;
        // 取个比例，防止随机在边上
        leftTop.x += _fMinIntervalDistWidth;
        leftTop.y += _fMinIntervalDistHeight;
        Vector2 rightBottom = _rightBottom;
        rightBottom.x -= _fMinIntervalDistWidth;
        rightBottom.y -= _fMinIntervalDistHeight;
        Vector3 pointA = (RulVec.RandVector2(rightBottom.x, rightBottom.y, leftTop.x, leftTop.y));

        // 点B
        Vector3 PointB = Vector3.zero;
        PointB.z = pointA.z;
        if (bH)
        {
            PointB.y = pointA.y;

            float fX = Random.Range(_leftTop.x, _rightBottom.x);
            PointB.x = fX;
        }
        else
        {
            PointB.x = pointA.x;

            float fY = Random.Range(_rightBottom.y, _leftTop.y);
            PointB.y = fY;
        }

        // 开始 与 结束
        Vector3 pointStart = pointA;
        if (bH)
        {
            pointStart.y = bLeftTopIsStartSide ? _leftTop.y : _rightBottom.y;
        }
        else
        {
            pointStart.x = bLeftTopIsStartSide ? _leftTop.x : _rightBottom.x;
        }
        Vector3 pointEnd = PointB;
        if (bH)
        {
            pointEnd.y = bLeftTopIsStartSide ? _rightBottom.y : _leftTop.y;
        }
        else
        {
            pointEnd.x = bLeftTopIsStartSide ? _rightBottom.x : _leftTop.x;
        }

        Vector3[] lstPos = new Vector3[4];
        lstPos[0] = (pointStart);
        lstPos[1] = (pointA);
        lstPos[2] = (PointB);
        lstPos[3] = (pointEnd);
        return lstPos;
    }
}

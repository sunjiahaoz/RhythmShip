using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dest.Math;
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

    /// <summary>
    /// 生成模拟的漂浮路径
    /// </summary>
    /// <param name="posStart">开始位置</param>
    /// <param name="posGoal">结束位置，漂浮的最终目的地</param>
    /// <param name="nNodeCount">路径点数量</param>
    /// <param name="fOffRangeMin">偏移值，每个路径点在水平方线的偏移值最小值</param>
    /// <param name="fOffRangeMax">偏移值， 每个路径点在水平方线的偏移值最大值</param>
    /// <returns></returns>
    public static Vector3[] GenerateSimulateFloatPath(Vector3 posStart, Vector3 posGoal, int nNodeCount, float fOffRangeMin, float fOffRangeMax)
    {
        Vector3[] pathPos = new Vector3[nNodeCount];
        float fPerProgress = 1f / nNodeCount;

        bool bDirFirst = Random.Range(0, 100) > 50 ? true : false;
        for (int i = 0; i < nNodeCount; ++i)
        {
            if (i == 0)
            {
                pathPos[i] = posStart;
                continue;
            }
            else if (i == nNodeCount - 1)
            {
                pathPos[i] = posGoal;
                continue;
            }

            Vector3 pos = Vector3.Lerp(posStart, posGoal, fPerProgress * i);
            float fRange = Random.Range(fOffRangeMin, fOffRangeMax);

            if (bDirFirst)
            {
                if (i % 2 == 0)
                {
                    pos.x += fRange;
                }
                else
                {
                    pos.x -= fRange;
                }
            }
            else
            {
                if (i % 2 == 0)
                {
                    pos.x -= fRange;
                }
                else
                {
                    pos.x += fRange;
                }
            }
            
            pathPos[i] = pos;
        }
        return pathPos;
    }

    /// <summary>
    /// 阿基米德螺旋线
    /// </summary>
    /// <param name="posStart">圆心位置</param>
    /// <param name="fScale">值越小，点位置越集中，越大越扩散</param>
    /// <param name="fCircleCount">转几个圈</param>
    /// <param name="nPosCount">获得几个点</param>
    /// <returns></returns>
    public static Vector3[] AJiMiDeCiclePath(Vector3 posStart, float fScale, float fCircleCount, int nPosCount)
    {
        Vector3[] pos = new Vector3[nPosCount];
        float pi = Mathf.PI;
        float a = fScale;
        float fzta = 0;
        float fStep = (fCircleCount * pi) / nPosCount;
        int nIndex = 0;
        for (nIndex = 0, fzta = 0; fzta <= fCircleCount * pi; fzta += fStep, nIndex++)
        {
            float r = a * fzta;
            float x = posStart.x + r * Mathf.Cos(fzta);
            float y = posStart.y - r * Mathf.Sin(fzta);
            pos[nIndex] = new Vector3(x, y, posStart.z);
        }
        return pos;
    }

    /// <summary>
    /// 正多边形的点,至少为3个点
    /// </summary>
    /// <param name="posCenter">中心位置</param>
    /// <param name="fRadios">半径</param>
    /// <param name="nPolygonPointCount">正几多边形，至少为3</param>
    /// <returns></returns>
    public static Vector3[] RegularPolygonPosPoint(Vector3 posCenter, float fRadios, int nPolygonPointCount)
    {
        if (nPolygonPointCount < 3)
        {
            return null;
        }
        Vector3[] poses = new Vector3[nPolygonPointCount];

        Vector2 pos = posCenter;
        Circle2 _c2 = new Circle2(ref pos, fRadios);
        
        int count = nPolygonPointCount;
        float delta = 2f * Mathf.PI / count;
        Vector3 prev = _c2.Eval(0);
        for (int i = 1; i <= count; ++i)
        {
            Vector3 curr = _c2.Eval(i * delta);
            poses[i - 1] = curr;
            prev = curr;
        }
        return poses;
    }

    /// <summary>
    /// 在获得圆周上几个点
    /// 需要注意要nGenerateCount * nIntervalCount需要小于nGenerateCirclePoints
    /// </summary>
    /// <param name="posCenter">圆心</param>
    /// <param name="fRadius">半径</param>
    /// <param name="nGenerateCirclePoints">生成圆用多少个点</param>
    /// <param name="nIntervalCount">每个几个点取一次位置</param>
    /// <param name="nGenerateCount">要生成多少个点</param>
    /// <returns></returns>
    public static Vector3[] PointsOnCircle(Vector3 posCenter, float fRadius, int nGenerateCount, int nIntervalCount = 4, int nGenerateCirclePoints = 50)
    {
        if (nIntervalCount <= 0
            || nGenerateCount <= 0
            || nGenerateCirclePoints <= 0)
        {
            return null;
        }
        Circle2 cc = new Circle2(posCenter, fRadius);
        int count = nGenerateCirclePoints;
        float delta = 2f * Mathf.PI / count;
        int nTrIndex = 0;
        // 数量是否有问题
        if (nGenerateCount * nIntervalCount >= nGenerateCirclePoints)
        {            
            return null;
        }
        Vector3[] res = new Vector3[nGenerateCount];
        for (int i = 0; i <= count; ++i)
        {
            if (i % nIntervalCount == 0)
            {
                Vector3 curr = cc.Eval(i * delta);
                res[nTrIndex] = curr;
                nTrIndex++;
            }
            if (nTrIndex >= nGenerateCount)
            {
                break;
            }
        }
        return res;
    }
}

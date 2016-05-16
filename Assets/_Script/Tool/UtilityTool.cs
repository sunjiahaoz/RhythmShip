using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;
using Dest.Math;
using RUL;

public class UtilityTool
{
    #region _Pos_
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
            if (nIndex >= pos.Length)
            {
                break;
            }
            float r = a * fzta;
            float x = posStart.x + r * Mathf.Cos(fzta);
            float y = posStart.y - r * Mathf.Sin(fzta);
            pos[nIndex] = new Vector3(x, y, posStart.z);
        }
        return pos;
    }

    private static Fibonacci _fibnacci = null;
    /// <summary>
    /// 斐波那契螺旋线的路径点生成
    /// </summary>
    /// <param name="nCount">点的数量</param>
    /// <param name="nScale">缩放等级</param>
    /// <returns></returns>
    public static Vector3[] FibbinacciSpiralPath(int nCount, int nScale = 1)
    {
        if (_fibnacci == null)
        {
            _fibnacci = new Fibonacci();
        }
        _fibnacci.Generate(nCount - 1, nScale); // -1因为这里要获得point数量，而不是cell数量
        return _fibnacci.GetFibnacciPointPos().ToArray();
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

    /// <summary>
    /// 生成爆炸分散点
    /// 以某点为中心，以某随机距离为半径生成点
    /// </summary>
    /// <param name="posCenter">爆炸点</param>
    /// <param name="fScatterRadiusMin">最短距离</param>
    /// <param name="fScatterRadiusMax">最大距离</param>
    /// <param name="nCount">生成点数量</param>
    /// <returns></returns>
    public static Vector3[] GenerateScatterPoint(Vector3 posCenter, float fScatterRadiusMin, float fScatterRadiusMax, int nCount)
    {
        Vector3[] resPos = new Vector3[nCount];
        Segment2 sg2 = new Segment2();
        Vector2 vecDir = Vector2.zero;
        for (int i = 0; i < nCount; ++i)
        {
            vecDir = RUL.RulVec.RandUnitVector2();            
            sg2.SetCenterDirectionExtent(posCenter, vecDir, RUL.Rul.RandFloat(fScatterRadiusMin, fScatterRadiusMax));
            resPos[i] = sg2.P1;
        }
        return resPos;
    }
    #endregion

    #region _color_
    /// <summary>
    /// 三色混合
    /// </summary>
    /// <param name="color1"></param>
    /// <param name="color2"></param>
    /// <param name="color3"></param>
    /// <param name="greyControl"></param>
    /// <returns></returns>
    public static Color RandomMixTriad(Color color1, Color color2, Color color3, float greyControl, float fAlpha = 1f)
    {        
        int randomIndex = Random.Range(0, 256) % 3;
        float mixRatio1 = (randomIndex == 0) ? Random.Range(0f, 1f) * greyControl : Random.Range(0f, 1f);
        float mixRatio2 = (randomIndex == 1) ? Random.Range(0f, 1f) * greyControl : Random.Range(0f, 1f);
        float mixRatio3 = (randomIndex == 2) ? Random.Range(0f, 1f) * greyControl : Random.Range(0f, 1f);
        float sum = mixRatio1 + mixRatio2 + mixRatio3;
        mixRatio1 /= sum;
        mixRatio2 /= sum;
        mixRatio3 /= sum;

        Color newColor = new Color();
        newColor.a = fAlpha;
        newColor.r = (mixRatio1 * color1.r + mixRatio2 * color2.r + mixRatio3 * color3.r);// / 255f;
        newColor.g = (mixRatio1 * color1.g + mixRatio2 * color2.g + mixRatio3 * color3.g);// / 255f;
        newColor.b = (mixRatio1 * color1.b + mixRatio2 * color2.b + mixRatio3 * color3.b);// / 255f;
        return newColor;
    }

    /// <summary>
    /// 通过HSL方法获得一组颜色
    /// 自己去找HSL的色相盘
    /// </summary>
    /// <param name="colorCount"></param>
    /// <param name="offsetAngle1">[0,360]</param>
    /// <param name="offsetAngle2">[0,360]</param>
    /// <param name="rangeAngle0">[0,360]</param>
    /// <param name="rangeAngle1">[0,360]</param>
    /// <param name="rangeAngle2">[0,360]</param>
    /// <param name="saturation">[0,100]</param>
    /// <param name="luminance">[0,100]</param>
    /// <returns></returns>
    public static List<Color> GenerateColors_Harmony(
   int colorCount,
   float offsetAngle1,
   float offsetAngle2,
   float rangeAngle0,
   float rangeAngle1,
   float rangeAngle2,
   float saturation, float luminance)
    {
        List<Color> colors = new List<Color>();

        float referenceAngle = Random.Range(0f, 1f) * 360;

        for (int i = 0; i < colorCount; i++)
        {
            float randomAngle =
                Random.Range(0f, 1f) * (rangeAngle0 + rangeAngle1 + rangeAngle2);

            if (randomAngle > rangeAngle0)
            {
                if (randomAngle < rangeAngle0 + rangeAngle1)
                {
                    randomAngle += offsetAngle1;
                }
                else
                {
                    randomAngle += offsetAngle2;
                }
            }

            ColorHSL hslColor = new ColorHSL(
               ((referenceAngle + randomAngle)) % 360f,
               saturation,
               luminance);

            colors.Add(hslColor.Color());
        }

        return colors;
    }
    #endregion
}

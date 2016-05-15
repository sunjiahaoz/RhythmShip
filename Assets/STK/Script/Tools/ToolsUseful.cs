﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace sunjiahaoz
{
    class ToolsUseful
    {
        #region _转换_
        /// <summary>
        /// 将颜色转成16进制的编码，比如红色(255,0,0)转成字符串：ff0000
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string TranslateColorToCode(Color color)
        {
            string strCode = "";
            strCode += System.Convert.ToString((int)(color.r * 255), 16).PadLeft(2, '0');
            strCode += System.Convert.ToString((int)(color.g * 255), 16).PadLeft(2, '0');
            strCode += System.Convert.ToString((int)(color.b * 255), 16).PadLeft(2, '0');
            return strCode;
        }

        /// <summary>
        /// 将一个int值转换成颜色
        /// 这个int值需要符合以下规则：最多有9为数，每隔3为表示一个颜色分量，值为000~255，从前到后为RGB的分量
        /// 比如 int值为255255255，则该颜色就表示Color(1,1,1)了。
        /// 比如255000,该颜色表示没有R为0，颜色为Color(0,1,0)
        /// 再比如0，就是(0,0,0)了
        /// </summary>
        /// <param name="nColorInt"></param>
        /// <returns></returns>
        public static Color TranslateIntToColor(int nColorInt)
        {
            string str = ToolsUseful.TranslateIntToString(nColorInt);
            string[] strC = str.Split(',');
            if (strC.Length > 3)
            {
                Debug.LogWarning("<color=orange>[Warning]</color>---" + nColorInt + "不合法！");
                return Color.white;
            }
            int[] colorV = new int[3] { 0, 0, 0 };
            int nCIndex = 0;
            for (int i = strC.Length - 1; i >= 0; --i)
            {
                colorV[nCIndex++] = System.Convert.ToInt32(strC[i]);
            }
            Color color = Color.white;
            color.r = (float)colorV[2] / 255f;
            color.g = (float)colorV[1] / 255f;
            color.b = (float)colorV[0] / 255f;
            return color;
        }
        /// <summary>
        /// 将颜色的16进制编码转换成颜色，
        /// 比如ff0000,转换成Color(1, 0, 0)
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public static Color TranslateCodeToColor(string strCode)
        {
            Color color = Color.white;
            if (strCode.Length != 6)
            {
                return Color.white;
            }

            string strSplitCode = "";
            float[] colorValue = new float[3];
            for (int i = 0; i < strCode.Length; ++i)
            {
                strSplitCode += strCode[i];
                if ((i + 1) % 2 == 0)
                {
                    colorValue[i / 2] = System.Convert.ToInt32(strSplitCode, 16);
                    strSplitCode = "";
                }
            }
            color.r = colorValue[0] / 255f;
            color.g = colorValue[1] / 255f;
            color.b = colorValue[2] / 255f;

            return color;
        }

        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        public static System.DateTime ConvertIntDateTime(double d)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time;
        }

        /// <summary>
        /// 转换秒为天，向上取整
        /// </summary>
        /// <param name="nSec"></param>
        /// <returns></returns>
        public static int ConvertSecToDay(int nSec)
        {
            float x = (float)nSec / 86400f;// (60 * 60 * 24);        
            int day = (int)Mathf.CeilToInt(x);
            return day;
        }
        /// <summary>
        /// 转换秒为天，向下取整
        /// </summary>
        /// <param name="nSec"></param>
        /// <returns></returns>
        public static int RoundDownToDay(int nSec)
        {
            float x = (float)nSec / 86400f;
            int day = (int)Mathf.Floor(x);
            return day;
        }
        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static double ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return intResult;
        }

        /// <summary>
        /// 保留两位小数
        /// </summary>
        /// <param name="fValue"></param>
        /// <returns></returns>
        public static string Retain2Decimals(float fValue)
        {
            return fValue.ToString("f2");
        }
        // 把秒转换成时间字符串
        // 比如128秒换成：00:02:08
        public static string GenerateCoolDownTime_hms(int nTotalSec)
        {
            if (nTotalSec <= 0)
            {
                return "00:00:00";
            }
            int nMin = nTotalSec / 60;
            int nHour = nMin / 60;
            nMin = (nTotalSec - nHour * 3600) / 60;
            int nSec = nTotalSec % 60;

            return nHour.ToString().PadLeft(2, '0') + ":" + nMin.ToString().PadLeft(2, '0') + ":" + nSec.ToString().PadLeft(2, '0');
        }
        // 转换成 00:00
        public static string GenerateCoolDownTime_ms(int nTotalSec)
        {
            if (nTotalSec <= 0)
            {
                return "00:00";
            }
            int nMin = nTotalSec / 60;
            int nSec = nTotalSec % 60;

            return nMin.ToString().PadLeft(2, '0') + ":" + nSec.ToString().PadLeft(2, '0');
        }
        /// <summary>
        /// 转换一个整数位字符串，每隔三位会有一个逗号，比如
        /// 12345678转换为“12,345,678”
        /// </summary>
        /// <param name="nInt"></param>
        /// <returns></returns>
        public static string TranslateIntToString(int nInt)
        {
            string strSrc = ("" + nInt);
            if (strSrc.Length <= 3)
            {
                return strSrc;
            }

            // 执行到以下说明数值至少是4位数 //
            char[] chars = strSrc.ToCharArray();

            string strRes = "";
            // 排在最前面的数字有几个
            int nForwardLength = chars.Length % 3;
            // 先把排在前面的加到字符串中
            for (int i = 0; i < nForwardLength; ++i)
            {
                strRes += chars[i];
            }
            // 来个逗号
            if (nForwardLength > 0)
            {
                strRes += ",";
            }
            // 之后的，每隔3个数就加个逗号，当然最后一个数是不加的
            int nCommaIndex = 0;
            for (int i = nForwardLength; i < chars.Length; ++i)
            {
                strRes += chars[i];
                nCommaIndex++;
                if (nCommaIndex == 3
                    && i != chars.Length - 1)
                {
                    strRes += ",";
                    nCommaIndex = 0;
                }
            }
            return strRes;
        }

        /// <summary>
        /// 检测并转换米为千米
        /// 当数值大于1000的时候转换为千米的单位
        /// 比如如果参数为56则返回值为字符串"56m"
        /// 如果参数为1233467,则返回值为"1233.46km"
        /// </summary>
        /// <param name="fMeter">要转换的米</param>
        /// <param name="strOut">输出转换后的字符串</param>
        /// <returns>如果需要转换则返回true，不需要转换返回false</returns>
        public static bool TranslateMeterToKilometer(float fMeter, out string strOut)
        {
            if (fMeter <= 1000)
            {
                // 这里转成int，不要小数
                strOut = ((int)fMeter).ToString();
                return false;
            }
            else
            {
                strOut = (fMeter / 1000f).ToString("f2");
                return true;
            }
        }
        #endregion

        #region _层Layer相关_
        /// <summary>
        /// 检测指定层索引与指定层是否相同
        /// </summary>
        /// <param name="nCheckLayer">[1~31]</param>
        /// <param name="strLayerName"></param>
        /// <returns></returns>
        public static bool CheckLayerIndexEqual(int nCheckLayerIndex, string strLayerName)
        {
            if (nCheckLayerIndex == LayerMask.NameToLayer(strLayerName))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据层的名称获取该层的索引[1～31]
        /// </summary>
        /// <param name="strLayerName"></param>
        /// <returns></returns>
        public static int GetLayerIndex(string strLayerName)
        {
            return LayerMask.NameToLayer(strLayerName);
        }
        /// <summary>
        /// 根据层的名称获取该层的值
        /// </summary>
        /// <param name="strLayerName"></param>
        /// <returns></returns>
        public static int GetLayerValue(string strLayerName)
        {
            return 1 << LayerMask.NameToLayer(strLayerName);
        }

        /// <summary>
        /// 根据层索引获取该层的值
        /// </summary>
        /// <param name="nLayerIndex"></param>
        /// <returns></returns>
        public static int GetLayerValue(int nLayerIndex)
        {
            return 1 << nLayerIndex;
        }

        /// <summary>
        /// 获取多个层的混合之后的值
        /// </summary>
        /// <param name="strLayerNames"></param>
        /// <returns></returns>
        public static int GetLayersCombineValue(params string[] strLayerNames)
        {
            int nValue = 0;
            for (int i = 0; i < strLayerNames.Length; ++i)
            {
                nValue |= GetLayerValue(strLayerNames[i]);
            }
            return nValue;
        }

        /// <summary>
        /// 检测goCheck的layer是否在LayerMask中
        /// </summary>
        /// <param name="lmContainer"></param>
        /// <param name="goCheck"></param>
        /// <returns></returns>
        public static bool CheckLayerContainedGo(LayerMask lmContainer, GameObject goCheck)
        {   
            return BitValue(lmContainer, goCheck.layer);
        }

        /// <summary>
        /// 设置Layer，包括对象的所有子对象
        /// </summary>
        /// <param name="go"></param>
        /// <param name="strLayerName"></param>
        public static void ChangeGoLayer(GameObject go, string strLayerName, bool bIncludeChildren = true)
        {
            int nIndexLayer = GetLayerIndex(strLayerName);
            go.layer = nIndexLayer;
            if (bIncludeChildren)
            {
                ProcessChildren(go.transform, (tr) =>
                {
                    tr.gameObject.layer = nIndexLayer;
                }, true);
            }            
        }
        #endregion

        #region _坐标or位置_
        /// <summary>
        /// 返回朝向的单位向量
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static Vector3 LookDir(Vector3 from, Vector3 to)
        {
            return (to - from).normalized;
        }
        /// <summary>
        /// 详见一个它的重载函数。
        /// 只不过Rect的描述变成了一个左上角坐标以及宽高。
        /// </summary>
        /// <param name="posLeftTop"></param>
        /// <param name="fRectWidth"></param>
        /// <param name="fRectHeigh"></param>
        /// <param name="screenCam"></param>
        /// <returns></returns>
        public static Vector3 ClampRectInScreen(Vector3 posLeftTop, float fRectWidth, float fRectHeigh, Camera screenCam)
        {
            Vector3 posRightBottom = new Vector3(posLeftTop.x + fRectWidth, posLeftTop.y + fRectHeigh, posLeftTop.z);
            return ClampRectInScreen(posLeftTop, posRightBottom, screenCam);
        }
        /// <summary>
        /// 将一个Rect的区域限定在一个屏幕中。这个Rect是由左上角坐标与右下角坐标来描述的。
        /// 主要用于比如Tips的窗口显示，有时tips会超出屏幕，所以使用这个函数可以返回一个让Tips完全显示在屏幕上的位置（如果该rect尺寸大于屏幕，那就不管了）
        /// 返回的坐标就是一个新的左上角坐标
        /// </summary>
        /// <param name="posLeftTop">左上角坐标</param>
        /// <param name="posRightBottom">右下角坐标</param>
        /// <param name="screenCam">屏幕摄像机</param>
        /// <returns>如果该rect完全在屏幕中了，则返回原来的左上角坐标。否则会返回一个经过计算的新的左上角坐标</returns>
        public static Vector3 ClampRectInScreen(Vector3 posLeftTop, Vector3 posRightBottom, Camera screenCam)
        {
            float fWidthOff = 0;
            float fHeightOff = 0;
            // 左上角转成屏幕坐标
            Vector2 posLTScreenPos = screenCam.WorldToViewportPoint(posLeftTop);
            // 右下角转成屏幕坐标
            Vector2 posRBScreenPos = screenCam.WorldToViewportPoint(posRightBottom);
            // 屏幕尺寸上的宽高
            float fRectWidth = posRBScreenPos.x - posLTScreenPos.x;
            float fRectHeight = posLTScreenPos.y - posRBScreenPos.y;

            // 判断下左上角的点是否超出屏幕了
            if (posLTScreenPos.x < 0)
            {
                fWidthOff = 0 - posLTScreenPos.x;
            }
            else if (posLTScreenPos.x > 1)
            {
                fWidthOff = 1f - posLTScreenPos.x - fRectWidth;
            }
            if (posLTScreenPos.y > 1f)
            {
                fHeightOff = 1f - posLTScreenPos.y;
            }
            else if (posLTScreenPos.y < 0)
            {
                fHeightOff = 0 - posLTScreenPos.y + fRectHeight;
            }

            // 进行改变一次
            if (fWidthOff != 0
                || fHeightOff != 0)
            {
                posLTScreenPos.x += fWidthOff;
                posLTScreenPos.y += fHeightOff;
                posRBScreenPos.x = posLTScreenPos.x + fRectWidth;
                posRBScreenPos.y = posLTScreenPos.y - fRectHeight;
            }

            // 再判断右下角是否超出。所以如果这个rect比屏幕大，则最后是按右下角的位置最后得出结果的
            if (posRBScreenPos.x > 1f)
            {
                fWidthOff = 1f - posRBScreenPos.x;
            }
            else if (posRBScreenPos.x < 0)
            {
                fWidthOff = 0 - posRBScreenPos.x;
            }
            if (posRBScreenPos.y > 1f)
            {
                fHeightOff = 1f - posRBScreenPos.y;
            }
            else if (posRBScreenPos.y < 0)
            {
                fHeightOff = 0f - posRBScreenPos.y;
            }

            // 再改变一次
            posLTScreenPos.x += fWidthOff;
            posLTScreenPos.y += fHeightOff;
            // 转成世界坐标吧
            return screenCam.ViewportToWorldPoint(posLTScreenPos);
        }
        /// <summary>
        /// 屏幕坐标转世界坐标
        /// </summary>
        /// <param name="screenPos"></param>
        /// <returns></returns>
        public static Vector3 GetWorldPos(Vector2 screenPos, Camera cam = null)
        {
            if (cam == null)
            {
                cam = Camera.main;
            }
            Ray ray = cam.ScreenPointToRay(screenPos);

            // we solve for intersection with z = 0 plane
            float t = -ray.origin.z / ray.direction.z;

            return ray.GetPoint(t);
        }

        /// <summary>
        /// 世界坐标转屏幕坐标
        /// </summary>
        /// <param name="worldPos"></param>
        /// <param name="useCamera"></param>
        /// <returns></returns>
        public static Vector2 GetScreenPos(Vector3 worldPos, Camera useCamera)
        {
            worldPos.z = 0;
            Vector3 uipos = useCamera.WorldToScreenPoint(worldPos);
            Vector2 pos = new Vector2(uipos.x, uipos.y);
            return pos;
        }

        /// <summary>
        /// 转换屏幕坐标到世界坐标。
        /// 参数并不是屏幕坐标，而是比例，以左下角为原点，参数为1,1的话，就是屏幕的右上角了
        /// </summary>
        /// <param name="fWidthScale">X方向的屏幕比例位置</param>
        /// <param name="fHeightScale">Y方向的屏幕比例位置</param>
        /// <returns>世界坐标，Z为0</returns>
        public static Vector3 GenerateScreenToAppearPos(float fWidthScale, float fHeightScale)
        {
            // 1. 指定左侧中心点为原点
            float fX = Screen.width * fWidthScale;
            float fY = Screen.height / 2 * fHeightScale;
            // 2. 转成屏幕坐标系，左下角为原点
            fY += Screen.height / 2;
            // 3. 转到世界坐标系
            return ToolsUseful.GetWorldPos(new Vector2(fX, fY));
        }

        /// <summary>
        /// 二维坐标转为一维索引
        /// </summary>
        /// <param name="nXPos"></param>
        /// <param name="nYPos"></param>
        /// <param name="nWidth">二维宽度</param>
        /// <returns></returns>
        public static int CoordTranslateToIndex(int nXPos, int nYPos, int nWidth)
        {
            return nYPos * nWidth + nXPos;
        }

        /// <summary>
        /// 一维索引转换为二维坐标
        /// </summary>
        /// <param name="nIndex">一维索引</param>
        /// <param name="nWidth">二维宽度</param>
        /// <returns></returns>
        public static Vector2 IndexTranslateToCoord(int nIndex, int nWidth)
        {
            Vector2 pos = Vector2.zero;
            pos.y = (int)(nIndex / nWidth);
            pos.x = nIndex % nWidth;
            return pos;
        }

        /// <summary>
        /// 检测指定索引是否在一个二维范围内
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <returns></returns>
        public static bool CheckIndexInMapRange(int nIndex, int nWidth, int nHeight)
        {
            if (nIndex < 0
                || nIndex >= nWidth * nHeight)
            {
                return false;
            }
            return true;
        }
        public static bool CheckCoordInMapRange(int nX, int nY, int nWidth, int nHeight)
        {
            if (nX < 0
                || nX >= nWidth
                || nY < 0
                || nY >= nHeight)
            {
                return false;
            }
            return true;
            //return CheckIndexInMapRange(CoordTranslateToIndex(nX, nY, nWidth), nWidth, nHeight);
        }
        #endregion

        #region _GameObject_

        /// <summary>
        /// 获取obj的T组件，如果没有，则自动Add并返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DefaultGetComponent<T>(GameObject obj) where T : Component
        {
            T com = obj.GetComponent<T>();
            if (com == null)
            {
                com = obj.AddComponent<T>();
            }
            return com;
        }
        /// <summary>
        /// 删除符合条件的子对象
        /// 如果没有条件，就是删除所有子对象
        /// </summary>
        /// <param name="root">根节点</param>
        /// <param name="destroyCondition">删除条件，返回值为bool，参数为一个子对象</param>
        public static void DestroyChildren(Transform root, System.Func<Transform, bool> destroyCondition = null, bool bImmediate = false)
        {
            List<Transform> lstChild = new List<Transform>();
            foreach (Transform item in root.transform)
            {
                lstChild.Add(item);
            }
            for (int i = 0; i < lstChild.Count; ++i)
            {
                // 没有条件或满足条件时进行删除
                if (destroyCondition == null
                    || destroyCondition(lstChild[i]))
                {
                    lstChild[i].transform.parent = null;
                    if (bImmediate)
                    {
                        GameObject.DestroyImmediate(lstChild[i].gameObject);
                    }
                    else
                    {
                        GameObject.Destroy(lstChild[i].gameObject);
                    }
                }
            }
            lstChild.Clear();
        }

        /// <summary>
        /// 遍历子对象进行一些操作
        /// </summary>
        /// <param name="root">要遍历的根</param>
        /// <param name="processChild">要对子对象进行的操作</param>
        /// <param name="bInteration">是否迭代，即是否也对子对象的子对象进行操作</param>
        public static void ProcessChildren(Transform root, System.Action<Transform> processChild, bool bInteration = false)
        {
            Transform trsChild = null;
            for (int i = 0; i < root.childCount; ++i)
            {
                trsChild = root.GetChild(i);
                processChild(trsChild);

                if (bInteration
                    && trsChild.childCount > 0)
                {
                    ProcessChildren(trsChild, processChild, bInteration);
                }
            }
        }

        /// <summary>
        /// 获取某个对象在目录树上的位置，通过字符串返回
        /// 以后就可以使用GameObject.Find找到这个对象了
        /// </summary>
        /// <param name="trs">一个对象的位置</param>
        /// <param name="strOutPath">返回的路径</param>
        public static void GenerateItemTreePath(Transform trs, ref string strOutPath, Transform root = null)
        {
            strOutPath = trs.name + "/" + strOutPath;
            if (trs.parent != null
                && trs.parent != root)
            {
                GenerateItemTreePath(trs.parent, ref strOutPath, root);
            }
        }
        /// <summary>
        /// 获取一个空的gameobject对象，用来感谢坏事
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static GameObject CreateNullGameObjec(string strName)
        {
            GameObject go = new GameObject(strName);
            return go;
        }

        /// <summary>
        /// 获取某个节点的子节点
        /// 因为GetComponentsInChildren会将自己本身也获得，所以用这个方法来提供规避
        /// </summary>
        /// <param name="trRoot"></param>
        /// <param name="bIncludeRoot">是否包含自身</param>
        /// <param name="bIncludInactive">是否包含inactive的</param>
        /// <returns></returns>
        public static List<T> GetComponentsInChildren<T>(T trRoot, bool bIncludeRoot = false, bool bIncludInactive = false) where T : Component
        {
            List<T> lst = new List<T>();
#if UNITY_IOS
            for (int i = 0; i < trRoot.transform.childCount; ++i)
            {
                lst.Add(trRoot.transform.GetChild(i).GetComponent<T>());
            }
#else
            lst.AddRange(trRoot.GetComponentsInChildren<T>(bIncludInactive));
#endif
            if (!bIncludeRoot)
            {
                lst.Remove(trRoot);
            }
            return lst;
        }
        #endregion

        #region _枚举_

        /// <summary>
        /// 返回枚举类型的枚举数量
        /// int nCount = GetEnumLength(typeof(MyEnum));
        /// </summary>
        /// <param name="EnumType"></param>
        /// <returns></returns>
        public static int GetEnumLength(System.Type EnumType)
        {
            return System.Enum.GetNames(EnumType).Length;
        }

        /// <summary>
        /// 将枚举转换成字符串
        /// string strName = GetEnumNameString(typeof(MyEnum), MyEnum.EnumType1)
        /// 此时 strName = "EnumType1";
        /// </summary>
        /// <param name="EnumType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumNameString(System.Type EnumType, System.Object value)
        {
            return System.Enum.GetName(EnumType, value);//推荐
        }

        /// <summary>
        /// 遍历枚举
        /// 我擦，为什么会有遍历枚举这样的需求。。。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="EnumType"></param>
        /// <param name="actionProcess"></param>
        public static void TravelProcessEnum<T>(System.Action<T> actionProcess)
        {
            System.Array array = System.Enum.GetValues(typeof(T));
            for (int i = 0; i < array.Length; ++i)
            {
                actionProcess((T)array.GetValue(i));
            }
        }
        #endregion

        #region _特效_
        /// <summary>
        /// 从那个对sprite淡入淡出的函数抽出来的，使得更通用一点
        /// </summary>
        /// <param name="fDuration">变化持续时间</param>
        /// <param name="fStartValue">开始值</param>
        /// <param name="fEndValue">变化到的值</param>
        /// <param name="actionPerUpdate">每次更新执行的东西</param>
        /// <param name="actionComplete">变化完成执行的东西</param>
        /// <param name="fStartDelay">开始延迟</param>
        /// <returns></returns>
        public static IEnumerator OnFadeInOrOutValue(float fDuration, float fStartValue, float fEndValue, System.Action<float> actionPerUpdate, System.Action actionComplete = null, float fStartDelay = 0)
        {
            if (fStartDelay > 0)
            {
                yield return new WaitForSeconds(fStartDelay);
            }
            // 变化的范围
            float fDist = fEndValue - fStartValue;
            float fPercent = 0f;

            for (float f = 0.0f; f < fDuration; f += Time.deltaTime)
            {
                float alpha = Mathf.Clamp01(2.0f * (1.0f - f / fDuration));
                fPercent = 1.0f - alpha;
                if (actionPerUpdate != null)
                {
                    actionPerUpdate(fStartValue + fPercent * fDist);
                }
                yield return 0;
            }

            if (actionComplete != null)
            {
                actionComplete();
            }
        }

        /// <summary>
        /// 重新播放粒子特效
        /// </summary>
        /// <param name="particleRoot"></param>
        public static void ReplayParticleRoot(Transform particleRoot)
        {
            ParticleSystem[] effects = particleRoot.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < effects.Length; ++i)
            {
                effects[i].Play();
            }
        }
        #endregion

        #region _工具_
        // 流hash, algName = sha1或md5
        public static string HashData(System.IO.Stream stream, string algName)
        {
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            System.Security.Cryptography.HashAlgorithm algorithm;

            if (algName == null)
            {
                throw new ArgumentNullException("algName 不能为 null");
            }
            if (string.Compare(algName, "sha1", true) == 0)
            {
                algorithm = System.Security.Cryptography.SHA1.Create();
            }
            else
            {
                if (string.Compare(algName, "md5", true) != 0)
                {
                    throw new Exception("algName 只能使用 sha1 或 md5");
                }
                algorithm = System.Security.Cryptography.MD5.Create();
            }
            byte[] resultByteAr = algorithm.ComputeHash(stream);
            return BitConverter.ToString(resultByteAr).Replace("-", "");
        }
        #endregion

        #region _Collection_
        /// <summary>
        /// 直接将参数转为hashtable,所以比要求必须为偶数数量
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Hashtable Hash(params object[] args)
        {
            Hashtable hashTable = new Hashtable(args.Length / 2);
            if (args.Length % 2 != 0)
            {
                return null;
            }
            else
            {
                int i = 0;
                while (i < args.Length - 1)
                {
                    hashTable.Add(args[i], args[i + 1]);
                    i += 2;
                }
                return hashTable;
            }
        }

        /// <summary>
        /// 从hashTable中获取T类型的参数
        /// 如果存在则通过tOut返回，并且函数返回值为true
        /// 否则函数返回值为false
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="tableParam">要从那个table获取</param>
        /// <param name="strKey">key</param>
        /// <param name="tOut">如果存在则会填充该值</param>
        /// <returns>如果存在key则返回true, 否则返回false</returns>
        public static bool GetTableParam<T>(Hashtable tableParam, string strKey, ref T tOut)
        {
            try
            {
                if (tableParam.Contains(strKey))
                {
                    tOut = (T)tableParam[strKey];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Dict排序功能
        /// </summary>
        /// <typeparam name="Tk"></typeparam>
        /// <typeparam name="Tv"></typeparam>
        /// <param name="dic"></param>
        /// <param name="customSortFunc">排序方法</param>
        /// <returns>返回排序后的dict</returns>
        public static Dictionary<Tk, Tv> SortDictionary<Tk, Tv>(Dictionary<Tk, Tv> dic, System.Comparison<KeyValuePair<Tk, Tv>> customSortFunc)
        {
            List<KeyValuePair<Tk, Tv>> myList = new List<KeyValuePair<Tk, Tv>>(dic);
            myList.Sort(customSortFunc);
            dic.Clear();
            foreach (KeyValuePair<Tk, Tv> pair in myList)
            {
                dic.Add(pair.Key, pair.Value);
            }
            return dic;
        }

        /// <summary>
        /// list排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <param name="customSortFunc"></param>
        public static void SortList<T>(ref List<T> lst, System.Comparison<T> customSortFunc)
        {
            lst.Sort(customSortFunc);
        }

        /// <summary>
        /// 洗牌，列表重新排列
        /// </summary>
        /// <param name="source"></param>
        public static void Shuffle(IList source)
        {
            var n = source.Count;

            while (n > 1)
            {
                n--;
                var k = UnityEngine.Random.Range(0, n + 1);
                var value = source[k];
                source[k] = source[n];
                source[n] = value;
            }
        }

        /// <summary>
        /// 合并两个List，返回一个新的List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst1"></param>
        /// <param name="lst2"></param>
        /// <returns></returns>
        public static List<T> CombineList<T>(List<T> lst1, List<T> lst2)
        {
            List<T> lstNew = new List<T>();
            if (lst1 != null)
            {
                lstNew.AddRange(lst1);
            }
            if (lst2 != null)
            {
                lstNew.AddRange(lst2);
            }
            return lstNew;
        }

        // 测试输出一个list
        public static void DebugOutList<T>(IList<T> lst)
        {
            if (lst == null)
            {
                return;
            }
            for (int i = 0; i < lst.Count; ++i )
            {
                Debug.Log(i + ":" + lst[i].ToString());
            }
        }
        #endregion

        #region _位相关操作_

        /// <summary>
        /// 检查2的幂的包含情况
        /// 检查ncheckValue是否包含在nContain中
        /// 比如nContain为 2 + 4 = 6，nCheckValue = 1
        /// 则返回false， 如果nCheckValue = 2， 则返回true    /// 
        /// </summary>
        /// <param name="nContain">容器值</param>
        /// <param name="nCheckValue">这个值必须是2的幂的值或几个2的幂的值的和</param>
        /// <returns></returns>
        public static bool BitContain(int nContain, int nCheckValue)
        {
            if ((nContain & nCheckValue) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 位中索引值， 
        /// </summary>
        /// <param name="nContain"></param>
        /// <param name="nIndex">0 ~ 31</param>
        /// <returns></returns>
        public static bool BitValue(int nContain, int nIndex)
        {
            if (nIndex >= 32)
            {
                return false;
            }
            // 计算该数据的哪一位
            int nObPos = (nIndex % 32);
            int index = 1;
            index <<= nObPos;
            index &= nContain;

            return (index != 0);
        }

        /// <summary>
        /// 从位中去掉某些值
        /// 比如 nContain = 1 + 2 + 4 + 8 = 15
        /// nRemove = 2 + 4 = 6
        /// 则 nContai & ~nRemove = 15 & ~6 = 9
        /// </summary>
        /// <param name="nContain"></param>
        /// <param name="nRemove"></param>
        /// <returns></returns>
        public static int BitRemove(int nContain, int nRemove)
        {
            return nContain & ~nRemove;
        }

        /// <summary>
        /// 在位中添加一些值
        /// 其实就是或操作了
        /// 比如 2 | 6 = 6
        /// 2 | 4 = 6
        /// </summary>
        /// <param name="nContain"></param>
        /// <param name="nAdd"></param>
        /// <returns></returns>
        public static int BitAdd(int nContain, int nAdd)
        {
            return nContain | nAdd;
        }
        #endregion

#region _常量_
        public const int MaxIntCanUse = 2147483646;  // int.MaxValue == 2147483647 但实际用的时候会当-1用，所以这里使用它的前一个值
        public const float GoldPointRadio = 0.618f; // 黄金比例分割点
#endregion
    }
}

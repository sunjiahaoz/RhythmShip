using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace sunjiahaoz.lianliankan
{
    public class LianLianKanGameControl<T> where T : class, IBaseTile
    {
#region _Event_
        public class LianLianKanGameControlEvent
        {
            // 游戏棋盘初始化结束之后的事件
            public delegate void OnAfterInitChessBoard();
            public event OnAfterInitChessBoard _eventOnAfterInitChessBoard;
            public void OnAfterInitChessBoardEvent()
            {
                if (_eventOnAfterInitChessBoard != null)
                {
                    _eventOnAfterInitChessBoard();
                }
            }

            // 配对成功
            public delegate void OnMatchSuccess(T tile1, T tile2, params T[] inflexionPoint);
            public event OnMatchSuccess _eventOnMatchSuccess;
            public void OnMatchSuccessEvent(T tile1, T tile2, params T[] inflexionPoint)
            {
                if (_eventOnMatchSuccess != null)
                {
                    _eventOnMatchSuccess(tile1, tile2, inflexionPoint);
                }
            }

            // 游戏结束， 0胜利，1失败
            public delegate void GameEnd(int nEndState);
            public event GameEnd _eventGameEnd;
            public void OnGameEnd(int nEndState)
            {
                if (_eventGameEnd != null)
                {
                    _eventGameEnd(nEndState);
                }
            }
        }
        public LianLianKanGameControlEvent _event = new LianLianKanGameControlEvent();
#endregion

        int _nRowCount = 8; // 行
        int _nColCount = 8; // 列
        int _nTileTypeCount = 27;   // tile种类数量,也即tile的ID数量

        T[,] tileArr = null;
        int[,] array = null;

        int _nLeftCount = 0;

        // 遍历
        public void TravelTileArray(System.Action<T> actionProcess)
        {
            for (int i = 0; i < _nRowCount + 2; i++)
            {
                for (int j = 0; j < _nColCount + 2; j++)
                {
                    actionProcess(tileArr[i, j]);
                }
            }
        }

        // 初始化棋盘
        public void InitChessBoard(int nRowCount, int nColCount, int nTileTypeCount, System.Func<T> createTile)
        {
            _nRowCount = nRowCount;
            _nColCount = nColCount;
            _nTileTypeCount = nTileTypeCount;

            // +2是为了在原有的棋盘基础上在扩展2行2列用于路径计算
            tileArr = new T[_nRowCount + 2, _nColCount + 2];
            array = new int[_nRowCount + 2, _nColCount + 2];

            _nLeftCount = _nRowCount * _nColCount;

            List<int> list = new List<int>();
            int nPairCount = _nRowCount * _nColCount / 2;
            for (int i = 0; i < nPairCount; i++)
            {
                int ac = Random.Range(1, _nTileTypeCount);
                list.Add(ac);
                list.Add(ac);
            }
            // 打乱顺序
            ToolsUseful.Shuffle(list);

            // 数据创建
            if (tileArr[0, 0] == null)
            {
                for (int i = 0; i < _nRowCount + 2; i++)
                {
                    for (int j = 0; j < _nColCount + 2; j++)
                    {
                        tileArr[i, j] = createTile();
                        tileArr[i, j].SetCoordinate(i, j);
                        tileArr[i, j].SetState(TileState.Disvisible);
                    }
                }
            }

            int nCount = 0;
            for (int i = 0; i < _nRowCount; i++)
            {
                for (int j = 0; j < _nColCount; j++)
                {
                    T bt = tileArr[i + 1, j + 1];
                    bt.SetID(list[nCount++]);
                    bt.SetState(TileState.Visible);
                    array[i + 1, j + 1] = bt.ID;
                }
            }

            _event.OnAfterInitChessBoardEvent();
        }

        T _lastSel = null;
        public void Select(T tile)
        {
            // 有没有选中过
            if (_lastSel == null)
            {
                _lastSel = tile;                
                tile.SetState(TileState.Seleted);
            }
            else
            {
                //如果两次点击的是同一个，则清空上次点击的内容
                if (_lastSel == tile)
                {
                    tile.SetState(TileState.Visible);
                    _lastSel = null;
                }
                else
                {
                    T A = _lastSel;
                    T B = tile;
                    
                    if (canHide(A, B) && A.ID == B.ID)
                    {                        
                        array[A.Row, A.Line] = 0;
                        array[B.Row, B.Line] = 0;
                        A.SetState(TileState.Disvisible);
                        B.SetState(TileState.Disvisible);
                        _lastSel = null;                        
                        _nLeftCount -= 2;
                        //DrawLine(A, B);

                        // 两个拐点
                        int x1 = otherPoint[0, 0];
                        int y1 = otherPoint[0, 1];                        
                        int x2 = otherPoint[1, 0];
                        int y2 = otherPoint[1, 1];                        
                        _event.OnMatchSuccessEvent(A, B, tileArr[x1, y1], tileArr[x2, y2]);

                        if (_nLeftCount == 0)
                        {
                            // 游戏结束！！！！！ 
                            _event.OnGameEnd(0);
                            return;
                        }
                    }
                    else
                    {
                        _lastSel = null;
                        A.SetState(TileState.Visible);
                    }
                }
            }
        }

        int[,] otherPoint = new int[2, 2];

        public bool CheckIsPair(T t1, T t2)
        {
            if (t1.ID == t2.ID)                
            {
                return canHide(t1, t2);    
            }
            return false;
        }

        //void DrawLine(BaseTile a, BaseTile b)
        //{
        //    int x, y;
            
        //    line.SetVertexCount(4);
        //    line.SetWidth(0.01f, 0.01f);
        //    line.SetPosition(0, a.transform.position);
        //    x = otherPoint[0, 0];
        //    y = otherPoint[0, 1];
        //    line.SetPosition(1, imgArr[x, y].img.transform.position);
        //    x = otherPoint[1, 0];
        //    y = otherPoint[1, 1];
        //    line.SetPosition(2, imgArr[x, y].img.transform.position);
        //    line.SetPosition(3, b.transform.position);
        //}

        /// <summary>
        /// 可以消除的方法
        /// </summary>
        /// <param name="a">第一张图片</param>
        /// <param name="b">第二张图片</param>
        /// <returns>返回到对应的方法</returns>
        bool canHide(T a, T b)
        {

            //行相等
            if (a.Row == b.Row)
            {
                return isRows(a, b);
            }
            //列相等
            else if (a.Line == b.Line)
            {
                return isCols(a, b);
            }
            else
            {
                if (isRows(a, b))
                    return true;
                else
                    return isCols(a, b);
            }
        }

        /// <summary>
        /// 横向检测是否可以消除
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        bool isRows(T a, T b)
        {
            int x1, y1, x2, y2;
            x1 = a.Row;
            y1 = a.Line;
            x2 = b.Row;
            y2 = b.Line;
            int x_1, y_1, x_2, y_2;
            for (int i = 0; i < 10; i++)
            {
                x_1 = i;
                y_1 = y1;
                x_2 = i;
                y_2 = y2;
                if (isAllHide(x1, y1, x_1, y_1))    //A -A'
                {
                    if (x1 != x_1 || y1 != y_1)
                    {
                        if (array[x_1, y_1] != 0)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    continue;
                }
                if (isAllHide(x_1, y_1, x_2, y_2))    //A'-B'
                {
                    if (x1 != x_1 || y1 != y_1)
                    {
                        if (array[x_1, y_1] != 0)
                        {
                            continue;
                        }
                    }
                    if (x2 != x_2 || y2 != y_2)
                    {
                        if (array[x_2, y_2] != 0)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    continue;
                }
                if (isAllHide(x_2, y_2, x2, y2))    //B'-B
                {
                    if (x2 != x_2 || y2 != y_2)
                    {
                        if (array[x_2, y_2] != 0)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    continue;
                }
                otherPoint[0, 0] = x_1;
                otherPoint[0, 1] = y_1;
                otherPoint[1, 0] = x_2;
                otherPoint[1, 1] = y_2;

                return true;
            }
            return false;
        }

        /// <summary>
        /// 纵向检测是否可以消除
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        bool isCols(T a, T b)
        {
            int x1, y1, x2, y2;
            x1 = a.Row;
            y1 = a.Line;
            x2 = b.Row;
            y2 = b.Line;

            int x_1, y_1, x_2, y_2;

            for (int i = 0; i < 10; i++)
            {
                x_1 = x1;
                y_1 = i;
                x_2 = x2;
                y_2 = i;

                if (isAllHide(x1, y1, x_1, y_1))    //A -A'
                {
                    if (x1 != x_1 || y1 != y_1)
                    {
                        if (array[x_1, y_1] != 0)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    continue;
                }
                if (isAllHide(x_1, y_1, x_2, y_2))    //A'-B'
                {
                    if (x1 != x_1 || y1 != y_1)
                    {
                        if (array[x_1, y_1] != 0)
                        {
                            continue;
                        }
                    }
                    if (x2 != x_2 || y2 != y_2)
                    {
                        if (array[x_2, y_2] != 0)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    continue;
                }
                if (isAllHide(x_2, y_2, x2, y2))    //B'-B
                {
                    if (x2 != x_2 || y2 != y_2)
                    {
                        if (array[x_2, y_2] != 0)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    continue;
                }

                otherPoint[0, 0] = x_1;
                otherPoint[0, 1] = y_1;
                otherPoint[1, 0] = x_2;
                otherPoint[1, 1] = y_2;

                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断消除路径上的点是否为空
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        bool isAllHide(int x1, int y1, int x2, int y2)
        {
            //同列
            if (x1 == x2 && y1 != y2)
            {
                if (y1 > y2)
                {
                    for (int i = y2 + 1; i < y1; i++)
                    {
                        if (array[x1, i] != 0)
                        {
                            return false;
                        }
                    }
                }
                else if (y2 > y1)
                {
                    for (int i = y1 + 1; i < y2; i++)
                    {
                        if (array[x1, i] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            //同行
            if (y1 == y2 && x1 != x2)
            {
                if (x1 > x2)
                {
                    for (int i = x2 + 1; i < x1; i++)
                    {
                        if (array[i, y1] != 0)
                        {
                            return false;
                        }
                    }
                }
                else if (x2 > x1)
                {
                    for (int i = x1 + 1; i < x2; i++)
                    {
                        if (array[i, y1] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}

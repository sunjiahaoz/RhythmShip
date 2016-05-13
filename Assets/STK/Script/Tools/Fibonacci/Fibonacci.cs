using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace sunjiahaoz
{
    public class Fibonacci
    {        
        private int cellCount = 6;

        private List<FibonacciCell> _cells = new List<FibonacciCell>();
        private List<Vector3> _lstPos = new List<Vector3>();
        private int _currentCell;
        private FibonacciCell _selectedCell;

        float n1 = 0, n2 = 1, n3;


        public void Generate(int nCellCount = 6, int nScale = 1)
        {
            // 数据初始化
            cellCount = nCellCount;
            _lstPos.Clear();
            _cells.Clear();
            n1 = 0;
            n2 = 1 * nScale;

            _lstPos.Add(Vector3.zero);
            FibonacciCell firstCell = new FibonacciCell();
            float initSize = CalculateFibonacciNumber();
            firstCell.cellDirection = CellDirection.up;

            firstCell.SetUp(0, 0, initSize, -initSize);            
            _lstPos.Add(new Vector3(initSize, -initSize));
            _cells.Add(firstCell);            

            for (int i = 0; i < cellCount; i++)
            {
                int modulos = i % 4;
                FibonacciCell cell = null;
                float size = CalculateFibonacciNumber();
                FibonacciCell lastCell = null;

                float top;
                float left;
                float right;
                float bottom;

                if (_cells.Count > 0)
                    lastCell = _cells[i];

                //x = left, y == top
                switch (modulos)
                {
                    case 0:
                        //left
                        cell = new FibonacciCell();
                        cell.cellDirection = CellDirection.left;

                        top = lastCell.top;
                        left = lastCell.left - size;
                        right = lastCell.left;
                        bottom = lastCell.top - size;
                        
                        _lstPos.Add(new Vector3(right, top));

                        cell.SetUp(top, left, right, bottom);
                        break;
                    case 1:
                        //down
                        cell = new FibonacciCell();
                        cell.cellDirection = CellDirection.down;

                        top = lastCell.bottom;
                        left = lastCell.left;
                        right = lastCell.left + size;
                        bottom = top - size;
                        
                        _lstPos.Add(new Vector3(left, top));

                        cell.SetUp(top, left, right, bottom);
                        break;
                    case 2:
                        //right
                        cell = new FibonacciCell();
                        cell.cellDirection = CellDirection.right;

                        bottom = lastCell.bottom;
                        left = lastCell.right;
                        right = left + size;
                        top = bottom + size;
                        
                        _lstPos.Add(new Vector3(left, bottom));

                        cell.SetUp(top, left, right, bottom);
                        break;

                    case 3:
                        //up
                        cell = new FibonacciCell();
                        cell.cellDirection = CellDirection.up;

                        top = lastCell.top + size;
                        left = lastCell.right - size;
                        right = lastCell.right;
                        bottom = lastCell.top;
                        
                        _lstPos.Add(new Vector3(right, bottom));

                        cell.SetUp(top, left, right, bottom);
                        break;
                }
                if (cell != null)
                    _cells.Add(cell);
            }
        }

        public List<Vector3> GetFibnacciPointPos()
        {
            return _lstPos;
        }

        public List<FibonacciCell> GetFibnacciCell()
        {
            return _cells;
        }

        float CalculateFibonacciNumber()
        {
            n3 = n1 + n2;

            n1 = n2;
            n2 = n3;

            return n3;
        }        
    }
}

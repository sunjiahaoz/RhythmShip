using UnityEngine;
using System.Collections;

namespace sunjiahaoz.lianliankan
{
    public enum TileState
    {
        Disvisible,
        Visible,
        Seleted,
    }

    public interface IBaseTile
    {
        int ID { get; }
        int Row { get; }
        int Line { get; }
        TileState state { get; }

        void SetID(int nId);
        void SetState(TileState state);
        void SetCoordinate(int nRow, int nLine);
    }

    public class BaseTile : IBaseTile
    {
        int _nID = 0;   // 用于标记是否相同的tile
        public int ID
        {
            get { return _nID; }
        }

        int _nRow = 0;    // 坐标
        int _nLine = 0;
        public int Row
        {
            get { return _nRow; }
        }
        public int Line
        {
            get { return _nLine; }
        }

        TileState _nState = TileState.Disvisible;    // 0 disvisible, 1, visible, 2 seleted
        public TileState state
        {
            get { return _nState; }
        }

        public void SetID(int nId)
        {
            _nID = nId;            
        }

        public void SetState(TileState state)
        {
            _nState = state;
        }

        public void SetCoordinate(int nRow, int nLine)
        {
            _nRow = nRow;
            _nLine = nLine;
        }
    }
}

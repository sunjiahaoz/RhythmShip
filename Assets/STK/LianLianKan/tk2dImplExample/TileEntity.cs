using UnityEngine;
using System.Collections;
namespace sunjiahaoz.lianliankan
{
    public class TileEntity : MonoBehaviour, IBaseTile
    {
        BaseTile _data = new BaseTile();
        public tk2dSprite _body;
        public tk2dTextMesh _tkTmpText;
        
#region _IBaseTile_
        public int ID
        {
            get { return _data.ID; }
        }

        public int Row
        {
            get { return _data.Row; }
        }

        public int Line
        {
            get { return _data.Line; }
        }

        public TileState state
        {
            get { return _data.state; }
        }

        public void SetID(int nId)
        {
            _data.SetID(nId);
            _tkTmpText.text = nId.ToString();
        }

        public void SetState(TileState state)
        {
            _data.SetState(state);
            switch (state)
            {
                case TileState.Disvisible:
                    _body.gameObject.SetActive(false);
                    break;
                case TileState.Visible:
                    _body.color = Color.white;
                    _body.gameObject.SetActive(true);
                    break;
                case TileState.Seleted:
                    _body.gameObject.SetActive(true);
                    _body.color = Color.gray;
                    break;
                default:
                    break;
            }
        }

        public void SetCoordinate(int nRow, int nLine)
        {
            _data.SetCoordinate(nRow, nLine);
        }
#endregion


    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace sunjiahaoz.lianliankan
{
    public class LianLianKanManager : MonoBehaviour
    {
        public int _nRowCount = 8;
        public int _nColCount = 8;
        public int _nTileTypeCount = 10;

        public float _fRowOff = 100;
        public float _fColOff = 100;

        public TileEntity _prefabEntity;


        LianLianKanGameControl<TileEntity> _llkControl = new LianLianKanGameControl<TileEntity>();
        List<TileEntity> _lstEntity = new List<TileEntity>();

        void Awake()
        {
            _llkControl._event._eventOnAfterInitChessBoard += _event__eventOnAfterInitChessBoard;
            _llkControl._event._eventOnMatchSuccess += _event__eventOnMatchSuccess;
        }
        void OnDestroy()
        {
            _llkControl._event._eventOnAfterInitChessBoard -= _event__eventOnAfterInitChessBoard;
            _llkControl._event._eventOnMatchSuccess -= _event__eventOnMatchSuccess;
        }

        // TEST CODE
        public TileEntity _selEntity;
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Home))
            {
                Init();
            }
            if (Input.GetKeyUp(KeyCode.End))
            {
                _llkControl.Select(_selEntity);
            }
            if (Input.GetKeyUp(KeyCode.PageUp))
            {
                SearchPair();
            }
        }

        void Init()
        {
            _lstEntity.Clear();
            if(!_llkControl.InitChessBoard(_nRowCount, _nColCount, _nTileTypeCount, CreateTileEntity))
            {
                TagLog.Log(0, "LianLianKanManager Init Failed !!!!!!!!!!!!!!!");
            }
        }

        public LineRenderer _line;
        void _event__eventOnMatchSuccess(TileEntity t1, TileEntity t2, params TileEntity[] inflexionPoints)
        {
            _line.SetVertexCount(4);
            _line.SetPosition(0, t1.transform.position);
            _line.SetPosition(1, inflexionPoints[0].transform.position);
            _line.SetPosition(2, inflexionPoints[1].transform.position);
            _line.SetPosition(3, t2.transform.position);
        }        

        void _event__eventOnAfterInitChessBoard()
        {
            // 显示位置
            for (int i = 0; i < _lstEntity.Count; ++i )
            {
                _lstEntity[i].transform.localPosition = new Vector3(
                    _fRowOff * _lstEntity[i].Line - (_nColCount + 2) / 2 * _fRowOff + _fRowOff / 2
                    , _fColOff * _lstEntity[i].Row - (_nRowCount + 2) / 2 * _fColOff + _fColOff/2
                    , 0);
            }
        }

        void SearchPair()
        {
            bool bFind = false;
            for (int i = 0; i < _lstEntity.Count;  ++i)
            {
                if (bFind)
                {
                    break;
                }

                if (_lstEntity[i].state != TileState.Visible)
                {
                    continue;
                }
                for (int j = 0; j < _lstEntity.Count; ++j )
                {
                    if (i == j
                        || _lstEntity[j].state != TileState.Visible)
                    {
                        continue;
                    }
                    if (_llkControl.CheckIsPair(_lstEntity[i], _lstEntity[j]))
                    {
                        _llkControl.Select(_lstEntity[i]);
                        _llkControl.Select(_lstEntity[j]);
                        bFind = true;
                        break;
                    }
                }
            }
            if (!bFind)
            {
                Debug.LogError("<color=red>[Error]</color>---" + "找不到！！！");
            }
            else
            {
                Debug.LogWarning("<color=orange>[Warning]</color>---" + "找到了！！！！");
            }
        }

        TileEntity CreateTileEntity()
        {
            GameObject go = ObjectPoolController.Instantiate(_prefabEntity.gameObject, transform.position, Quaternion.identity);
            TileEntity entity = go.GetComponent<TileEntity>();
            _lstEntity.Add(entity);
            return entity;
        }
    }
}

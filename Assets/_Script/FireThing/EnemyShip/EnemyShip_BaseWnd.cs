using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class EnemyShip_BaseWnd : BaseEnemyShip {
    [Header("=======EnemyShip_BaseWnd========")]
    public IKAnchor _trLeftTop;
    public IKAnchor _trRightBottom;

    public tk2dTextMesh _textTitle;
    public tk2dTextMesh _textContent;
    public tk2dSprite _spImage;

    float _fDestroyDelay = 0f;   // 延迟这些时间之后销毁    
    public System.Action<EnemyShip_BaseWnd> _actionWndDestroy = null;

    tk2dTextMesh[] _textMeshs = null;
    tk2dBaseSprite[] _baseSprites = null;

    int _nMaxOrder = 0;
    protected override void Awake()
    {
        base.Awake();        
        if (_trLeftTop == null 
            || _trRightBottom == null)
        {
            TagLog.LogError(LogIndex.Enemy, "wnd 需要左上角或右下角的IK点", this);
            return;
        }

        _textMeshs = GetComponentsInChildren<tk2dTextMesh>();
        for (int i = 0; i < _textMeshs.Length; ++i)
        {
            if (_textMeshs[i].SortingOrder >= _nMaxOrder)
            {
                _nMaxOrder = _textMeshs[i].SortingOrder;
            }
        }
        _baseSprites = GetComponentsInChildren<tk2dBaseSprite>();
        for (int i = 0; i < _baseSprites.Length; ++i)
        {
            if (_baseSprites[i].SortingOrder >= _nMaxOrder)
            {
                _nMaxOrder = _baseSprites[i].SortingOrder;
            }
        }
    }

    public virtual void InitContent(int nWndIndex, float fDestroyDelay)
    {
        _fDestroyDelay = fDestroyDelay;
        for (int i = 0; i < _textMeshs.Length; ++i)
        {
            _textMeshs[i].SortingOrder += (nWndIndex * _nMaxOrder);
        }        
        for (int i = 0; i < _baseSprites.Length; ++i)
        {
            _baseSprites[i].SortingOrder += (nWndIndex * _nMaxOrder);
        }

        TanGeJinBiConfig config = WndConfigDataMgr.Instance.GetRandomWndConfig();
        _textTitle.text = Localization.Get(config.title);
        _textContent.text = Localization.Get(config.content);
        // 长度<2表示不显示图片
        if (config.spriteName.Length > 2)
        {
            _spImage.gameObject.SetActive(true);
            _spImage.spriteId = _spImage.GetSpriteIdByName(config.spriteName);
        }
        else
        {
            _spImage.gameObject.SetActive(false);
        }
    }

    public override void OnThingDestroy()
    {
        if (_actionWndDestroy != null)
        {
            _actionWndDestroy(this);
        }
        StartCoroutine(OnThingDestroyProcess());
    }   


    IEnumerator OnThingDestroyProcess()
    {
        yield return new WaitForSeconds(_fDestroyDelay);
        base.OnThingDestroy();
    }
}

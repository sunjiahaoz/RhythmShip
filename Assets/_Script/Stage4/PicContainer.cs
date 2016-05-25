using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class PicContainer : MonoBehaviour {
    public string _strPicPrefix;

    public PicItem _prefabPicItem;

    PicItem[] _picItems = null;

    List<int> _lstRandomIndex = new List<int>();
    void Awake()
    {
        InitAllSprites();
    }

    void Start()
    {
        BallManager.Instance._OnBallDestroyEvent.AddListener(_eventOnBallDestroy);
    }
    void OnDestroy()
    {
        if (!BallManager.DoesInstanceExist())
        {
            BallManager.Instance._OnBallDestroyEvent.RemoveListener(_eventOnBallDestroy);
        }        
    }

    void _eventOnBallDestroy(Enemy_BallBase ball)
    {
        if (!ball._bSmallest)
        {
            return;
        }

        if (_lstRandomIndex.Count == 0)
        {            
            return;
        }

        ShowSprite(_lstRandomIndex[0]);
        _lstRandomIndex.RemoveAt(0);

        if (_lstRandomIndex.Count == 0)
        {
            BallManager.Instance._OnBallDestroyEvent.RemoveListener(_eventOnBallDestroy);
            PlayAllShow();
        }
    }

    void InitAllSprites()
    {
        _picItems = GetComponentsInChildren<PicItem>();
        _lstRandomIndex.Clear();
        for (int i = 0; i < _picItems.Length; ++i)
        {
            _lstRandomIndex.Add(i);
            _picItems[i].InitSprite(_strPicPrefix + "/" + i);
        }
        ToolsUseful.Shuffle(_lstRandomIndex);
    }

    void ShowSprite(int nIndex)
    {
        if (nIndex < 0
            || nIndex >= _picItems.Length)
        {
            return;
        }
        _picItems[nIndex].PlayShow();
    }

    void PlayAllShow()
    {
        for (int i = 0; i < _picItems.Length; ++i )
        {
            _picItems[i].PlayAllShow();
        }
    }
    

    // 创建
    [ContextMenu("根据前缀创建item")]
    void CreateItems()
    {        
        int nIndex = 0;
        int nId = _prefabPicItem._sprite.Collection.GetSpriteIdByName(_strPicPrefix + "/" + nIndex++, -1);
        while (nId != -1)
        {
            GameObject go = ObjectPoolController.Instantiate(_prefabPicItem.gameObject);
            go.transform.parent = transform;
            nId = _prefabPicItem._sprite.Collection.GetSpriteIdByName(_strPicPrefix + "/" + nIndex++, -1);
        }        
    }

    [ContextMenu("清空子对象")]
    void Clear()
    {
        ToolsUseful.DestroyChildren(transform);
    }


    [ContextMenu("刷新")]
    public void FreshSprites()
    {
        _picItems = GetComponentsInChildren<PicItem>();            


        for (int i = 0; i < _picItems.Length; ++i)
        {
            _picItems[i].InitSprite(_strPicPrefix + "/" + i, true);
        }
    }
}

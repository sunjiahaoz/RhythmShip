using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;
using DG.Tweening;

public class PicContainer : MonoBehaviour {
    public string _strPicPrefix;    
    public EffectParam _showSpriteEffect;
    public Transform _prefabMoveBall;
    public PicItem _prefabPicItem;

    PicItem[] _picItems = null;

    List<int> _lstRandomIndex = new List<int>();
    
    public void PicContainerStart()
    {
        InitAllSprites();
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

        StartCoroutine(PlayShowSprite(ball.transform.position));        
    }

    IEnumerator PlayShowSprite(Vector3 appearPos)
    {
        if (_lstRandomIndex.Count == 0)
        {
            yield break;
        }
        int nIndex = _lstRandomIndex[0];
        _lstRandomIndex.RemoveAt(0);

        GameObject goBall = ObjectPoolController.Instantiate(_prefabMoveBall.gameObject, appearPos, Quaternion.identity);        
        goBall.transform.DOMove(_picItems[nIndex].transform.position, 0.3f);
        yield return new WaitForSeconds(0.3f);
        if (_showSpriteEffect._strName.Length > 0)
        {
            _showSpriteEffect._pos = goBall.transform.position;
            ShotEffect.Instance.Shot(_showSpriteEffect);
        }
        ObjectPoolController.Destroy(goBall);
        ShowSprite(nIndex);

        if (_lstRandomIndex.Count == 0)
        {
            yield return new WaitForSeconds(1f);
            BallManager.Instance._OnBallDestroyEvent.RemoveListener(_eventOnBallDestroy);
            PlayAllShow();
            yield return new WaitForSeconds(1.5f);
            BallManager.Instance._OnPicContainerFinish.Invoke();
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
            nId = _prefabPicItem._sprite.Collection.GetSpriteIdByName(_strPicPrefix + "/" + nIndex++, -1);
            // 如果为-1就直接退出，主要是用于少创建一个
            if (nId == -1)
            {
                break;
            }

            GameObject go = ObjectPoolController.Instantiate(_prefabPicItem.gameObject);
            go.transform.parent = transform;            
        }
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

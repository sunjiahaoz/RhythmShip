using UnityEngine;
using System.Collections;
using sunjiahaoz;
using DG.Tweening;

public class Elem_HPHeart : MonoBehaviour {
    public UISprite _spFull;
    public UISprite _spEmpty;

    Quaternion _srcQ = Quaternion.identity;
    Vector3 _srcFullPos = Vector3.zero;

    // 是否还是满的
    bool _isFull = true;
    public bool IsFull
    {
        get { return _isFull; }
    }

    void Awake()
    {
        // 保存源数据
        _srcQ = transform.rotation;
        _srcFullPos = _spFull.transform.position;
    }

    public void Init()
    {        
        transform.rotation = _srcQ;
        _spFull.transform.position = _srcFullPos;
        _spEmpty.transform.position = _srcFullPos;
        ChangeSpriteAlpha(_spFull, 1);
        ChangeSpriteAlpha(_spEmpty, 0);        
        _spEmpty.transform.localScale = Vector3.zero;
        _isFull = true;
    }

    //void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Home))
    //    {
    //        PlayLoseHeart();
    //    }
    //    if (Input.GetKeyUp(KeyCode.End))
    //    {
    //        Init();
    //    }
    //}

    public void PlayLoseHeart()
    {
        if (!_bIsPlaying)
        {
            StartCoroutine(OnPlayLoseHeart());
        }
    }

    bool _bIsPlaying = false;
    float _fShakeDur = 0.3f;
    float _fMoveYDst = 150f;
    float _fMoveYDur = 0.5f;
    float _fShowEmptyDur = 0.2f;
    IEnumerator OnPlayLoseHeart()
    {
        _isFull = false;
        _bIsPlaying = true;

        _spFull.transform.DOShakeScale(_fShakeDur);
        yield return new WaitForSeconds(_fShakeDur);
        _spFull.transform.DOLocalMoveY(_fMoveYDst, _fMoveYDur);
        yield return StartCoroutine(ToolsUseful.OnFadeInOrOutValue(_fMoveYDur, 1f, 0f, (value) =>
        {
            ChangeSpriteAlpha(_spFull, value);
        }));        
        _spEmpty.transform.DOScale(Vector3.one, _fShowEmptyDur).SetDelay(_fShowEmptyDur/ 2);
        yield return StartCoroutine(ToolsUseful.OnFadeInOrOutValue(_fShowEmptyDur, 0f, 1f, (value) =>
        {
            ChangeSpriteAlpha(_spEmpty, value);
        }));

        _bIsPlaying = false;
    }

    void ChangeSpriteAlpha(UISprite sp, float fValue)
    {
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, fValue);
    }
}

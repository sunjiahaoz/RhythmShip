using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(UISprite))]
public class Elem_SpriteAnim : MonoBehaviour {
    public string mPrefix = "";
    public float _fDur = 1f;
    public bool _bLoop = false;
    UISprite mSprite;
    List<string> mSpriteNames = new List<string>();
    public System.Action _actionAnimComplete = null;

    void Awake()
    {
        mSprite = GetComponent<UISprite>();
    }

    float _fPerInerval = 0;
    float _fCurWaitInterval = 0;
    int _nCurIndex = 0;
    bool _bIsPlaying = false;
    public void Play()
    {
        RebuildSpriteList();
        _fPerInerval = _fDur / mSpriteNames.Count;
        _nCurIndex = 0;
        _fCurWaitInterval = 0;
        _bIsPlaying = true;
    }

    void Update()
    {
        if (!_bIsPlaying)
        {
            return;
        }

        if (_nCurIndex >= mSpriteNames.Count)
        {
            if (_bLoop)
            {
                _nCurIndex = 0;
            }
            else
            {
                _bIsPlaying = false;
                if (_actionAnimComplete != null)
                {
                    _actionAnimComplete();
                }
                return;
            }
        }

        _fCurWaitInterval += Time.deltaTime;
        if (_fCurWaitInterval >= _fPerInerval)
        {
            mSprite.spriteName = mSpriteNames[_nCurIndex++];
            _fCurWaitInterval = 0;
        }
    }

    public void RebuildSpriteList()
    {
        if (mSprite == null) mSprite = GetComponent<UISprite>();
        mSpriteNames.Clear();

        if (mSprite != null && mSprite.atlas != null)
        {
            List<UISpriteData> sprites = mSprite.atlas.spriteList;

            for (int i = 0, imax = sprites.Count; i < imax; ++i)
            {
                UISpriteData sprite = sprites[i];

                if (string.IsNullOrEmpty(mPrefix) || sprite.name.StartsWith(mPrefix))
                {
                    mSpriteNames.Add(sprite.name);
                }
            }
            mSpriteNames.Sort();
        }
    }
	
}

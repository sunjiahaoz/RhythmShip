using UnityEngine;
using System.Collections;

public class EventTriggerChangeSprite : MonoBehaviour {
    [Header("动画相关")]
    public tk2dSpriteAnimator _anim;
    [Header("图片相关")]
    public tk2dSprite _sprite;
    public string[] _spriteName;

    void OnDisable()
    {
        _nCurIndex = 0;
    }
    
    public void PlayDefaultAnim()
    {
        _anim.Play();
    }

    int _nCurIndex = 0;
    public void ChangeToNextSprite()
    {
        if (_nCurIndex < 0
        || _nCurIndex >= _spriteName.Length)
        {
            return;
        }
        _sprite.spriteId = _sprite.GetSpriteIdByName(_spriteName[_nCurIndex]);
        _nCurIndex++;
        if (_nCurIndex >= _spriteName.Length)
        {
            _nCurIndex = 0;
        }
    }
}

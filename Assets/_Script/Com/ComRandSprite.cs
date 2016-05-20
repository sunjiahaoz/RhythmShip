using UnityEngine;
using System.Collections;

public class ComRandSprite : MonoBehaviour {
    public tk2dSprite _sprite;
    public string _strSpriteNamePrefix;
    public IntRange _rangeSuffixNum;

    public void SetRandSprite()
    {
        string strSpriteName = _strSpriteNamePrefix + _rangeSuffixNum.RandomValue;
        _sprite.spriteId = _sprite.GetSpriteIdByName(strSpriteName);
    }

}

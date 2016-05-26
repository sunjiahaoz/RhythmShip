using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ComGetRandomColor))]
public class ComRandSpriteColor : MonoBehaviour {
    public tk2dSprite _sprite;
    ComGetRandomColor _comGetColor;    

    public ComGetRandomColor GetColorCom
    {
        get
        {
            if (_comGetColor == null)
            {
                _comGetColor = GetComponent<ComGetRandomColor>();
            }
            return _comGetColor;
        }
    }

    public void SetRandColor()
    {
        _sprite.color = GetColorCom.GetColor();
    }
}

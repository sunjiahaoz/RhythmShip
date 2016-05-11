using UnityEngine;
using System.Collections;
using RUL;

public enum RandomColorType
{
    Random,
    RandomWhitLightness,
    RandomWhitAlpha,
    Between,
    Hues,
    List,
}

public class ComGetRandomColor : ComGetColor {
    public RandomColorType _randomType = RandomColorType.Random;

    [Header("RandomWhitLightness有效")]
    public float _fLightNess = 1;
    [Header("RandomWhitAlpha有效")]
    public float _fAlpha = 1f;
    [Header("Between类型时有效")]
    public Color _cFrom = Color.white;
    public Color _cTo = Color.white;
    [Header("Hues时有效")]
    public Hues _hues = Hues.Monochrome;
    [Header("List时有效")]
    public Color[] _lstColor;


    public override Color GetColor()
    {
        switch (_randomType)
        {
            case RandomColorType.Random:
                return RulCol.RandColor();                
            case RandomColorType.Between:
                return RulCol.RandColorBetween(_cFrom, _cTo);
            case RandomColorType.RandomWhitLightness:
                return RulCol.RandColor(_fLightNess);
            case RandomColorType.RandomWhitAlpha:
                {
                    Color color = RulCol.RandColor();
                    color.a = _fAlpha;
                    return color;
                }
            case RandomColorType.List:
                {
                    if (_lstColor.Length == 0)
                    {
                        return Color.white;
                    }
                    return _lstColor[Random.Range(0, _lstColor.Length)];
                }
            case RandomColorType.Hues:
                {
                    return RulCol.RandColor(_hues);
                }
                
            default:
                break;
        }
        return Color.white;
    }
}

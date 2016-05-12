/*
*BGColorWall
*by sunjiahaoz 2016-5-11
*
*颜色变换的
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;
public class BGColorWall : MonoBehaviour {
    public bool _bUseValue = false;
    [Header("bUseValue为true时使用这三个")]
    public string _strColor1 = "ff0000";
    public string _strColor2 = "00ff00";
    public string _strColor3 = "0000ff";
    [Header("bUseValue为false时使用这三个")]
    public Color _color1 = Color.red;
    public Color _color2 = Color.blue;
    public Color _color3 = Color.green;

    public float _fGray = 0.5f;

    public int _nColorCount = 10;
    List<Color> _lstColors = new List<Color>();
    tk2dSprite[] _lstSprites = null;
    int _nCurIndexColor = 0;

    void Awake()
    {
        _lstSprites = GetComponentsInChildren<tk2dSprite>();

        for (int i = 0; i < _lstSprites.Length; ++i )
        {
            _lstSprites[i].color = new Color(1, 1, 1, 0);
        }
        if (_bUseValue)
        {
            _color1 = ToolsUseful.TranslateCodeToColor(_strColor1);
            _color2 = ToolsUseful.TranslateCodeToColor(_strColor2);
            _color3 = ToolsUseful.TranslateCodeToColor(_strColor3);
        }
        GenerateColors();
    }

    public void ChangeColor()
    {
        if (_lstSprites == null)
        {
            return;
        }
        for (int i = 0; i < _lstSprites.Length; ++i )
        {
            _lstSprites[i].color = _lstColors[_nCurIndexColor];
        }
        _nCurIndexColor++;
        if (_nCurIndexColor >= _lstColors.Count)
        {
            _nCurIndexColor = 0;
        }
    }

    void GenerateColors()
    {
        _lstColors.Clear();
        for (int i = 0; i < _nColorCount; ++i )
        {
            _lstColors.Add(UtilityTool.RandomMixTriad(_color1, _color2, _color3, _fGray, 0.5f));
        }
        _nCurIndexColor = 0;
    }
}

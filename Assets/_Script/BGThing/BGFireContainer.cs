using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class BGFireContainer : MonoBehaviour {
    public float _fDisappearDur = 0.5f;
    public float _fMinAlpha = 0.2f;
    public float _fMaxAlpha = 1f;

    List<tk2dSpriteAnimator> _lstFireEffect = new List<tk2dSpriteAnimator>();

    Color _colorNor = Color.white;
    void Awake()
    {
        _colorNor.a = _fMaxAlpha;
        _lstFireEffect.AddRange(GetComponentsInChildren<tk2dSpriteAnimator>());
        // 默认隐藏
        ToolsUseful.TravelList<tk2dSpriteAnimator>(_lstFireEffect, (elem) => 
        {
            elem.gameObject.SetActive(false);
        });
    }

    public void AppearFire()
    {
        for (int i = 0; i < _lstFireEffect.Count; ++i )
        {
            _lstFireEffect[i].Sprite.color = _colorNor;
            _lstFireEffect[i].gameObject.SetActive(true);
        }
    }

    // 彻底隐藏火焰
    public void DisappearFire()
    {
        Color color = Color.white;
        StartCoroutine(ToolsUseful.OnFadeInOrOutValue(_fDisappearDur, _fMaxAlpha, _fMinAlpha, (value) => 
        {
            color.a = value;
            ToolsUseful.TravelList<tk2dSpriteAnimator>(_lstFireEffect, (elem) =>
            {
                elem.Sprite.color = color;
            });
        }, () => { }));
    }
}

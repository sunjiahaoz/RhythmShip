using UnityEngine;
using System.Collections;
using sunjiahaoz;
using UnityEngine.Events;

public class PicItem : MonoBehaviour {
    [SerializeField]
    public UnityEvent _OnPlayShow;
    [SerializeField]
    public UnityEvent _OnPlayAllShow;
    public tk2dSprite _sprite;    
    Renderer _comRenderer;
    void Awake()
    {   
        _comRenderer = _sprite.GetComponent<Renderer>();
    }

    public void InitSprite(string strSpriteName, bool bFromMenu = false)
    {
        if (_comRenderer == null)
        {            
            _comRenderer = _sprite.GetComponent<Renderer>();
        }

        _sprite.spriteId = _sprite.GetSpriteIdByName(strSpriteName);
        _comRenderer.enabled = bFromMenu;
    }

    public void PlayShow()
    {
        _comRenderer.enabled = true;
        _OnPlayShow.Invoke();
    }

    public void PlayAllShow()
    {
        _OnPlayAllShow.Invoke();
    }

}

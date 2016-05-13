using UnityEngine;
using System.Collections;
using sunjiahaoz;
public class Elem_SkillTTBase : MonoBehaviour {
    public UISprite _spFull;
    public UISprite _spEmpty;
    public Elem_SpriteAnim _anim;

    ActionCD _cd = null;
    PlayerShipSkillTriggerTypeBase _skill = null;
    public void Init(PlayerShipSkillTriggerTypeBase triggerSkill)
    {
        _cd = triggerSkill.SkillCD;
        _skill = triggerSkill;    
        _skill._event._eventOnCastSkill += _event__eventOnCastSkill;
        _anim.gameObject.SetActive(false);
        _anim._actionAnimComplete = () => { _anim.gameObject.SetActive(false); };
    }

    void OnDestroy()
    {
        _skill._event._eventOnCastSkill -= _event__eventOnCastSkill;
    }

    void _event__eventOnCastSkill(int nState)
    {
        if (nState == 0)
        {
            _anim.gameObject.SetActive(true);
            _anim.Play();
        }        
    }


    void Update()
    {
        if (_cd == null)
        {
            return;
        }
        float fPercent = _cd.GetLeftPercent();
        _spFull.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, fPercent);        
    }

}

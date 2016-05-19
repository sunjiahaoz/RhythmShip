using UnityEngine;
using System.Collections;

public class Com_Play2dEffect : MonoBehaviour {
    public EffectParam _param;
	
    public void PlayEffect()
    {
        if (_param._strName.Length > 0)
        {
            _param._pos = transform.position;
            ShotEffect.Instance.Shot(_param);
        }        
    }
}

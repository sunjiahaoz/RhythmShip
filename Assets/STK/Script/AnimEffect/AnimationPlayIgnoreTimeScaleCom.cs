using UnityEngine;
using System.Collections;

namespace sunjiahaoz.AnimEffect
{
    [RequireComponent(typeof(Animation))]
    public class AnimationPlayIgnoreTimeScaleCom : MonoBehaviour
    {
        Animation _comAnim;
        void Awake()
        {
            _comAnim = GetComponent<Animation>();
        }

        Coroutine _cor = null;
        void OnEnable()
        {
            _cor = StartCoroutine(AnimationPlayIgnoreTimeScale.Play(_comAnim, _comAnim.clip.name, true, null));
        }

        void OnDisable()
        {
            StopCoroutine(_cor);
        }
    }
}

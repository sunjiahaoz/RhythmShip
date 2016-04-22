using UnityEngine;
using System.Collections;
namespace sunjiahaoz
{

    public class ObjeStampAnim : MonoBehaviour
    {
        public float _fAnimDur = 0.4f;
        public float _fYRoate = 60;
        public iTween.EaseType _roateEaseType = iTween.EaseType.linear;
        public float _fScale = 1.2f;
        public iTween.EaseType _scaleEaseType = iTween.EaseType.linear;
        public Vector3 _v3Pos = Vector3.zero;
        public iTween.EaseType _posEaseType = iTween.EaseType.linear;

        public void Run()
        {
            Vector3 srcPosition = transform.localPosition;
            transform.Translate(_v3Pos);
            Vector3 srcScale = transform.localScale;
            transform.localScale *= _fScale;
            Vector3 srcRotate = transform.localEulerAngles;
            transform.Rotate(0, _fYRoate, 0);


            iTween.RotateTo(gameObject, iTween.Hash("rotation", srcRotate, "islocal", true, "time", _fAnimDur, "easetype", _roateEaseType));
            iTween.MoveTo(gameObject, iTween.Hash("position", srcPosition, "islocal", true, "time", _fAnimDur, "easeType", _posEaseType));
            iTween.ScaleTo(gameObject, iTween.Hash("scale", srcScale, "islocal", true, "time", _fAnimDur, "easetype", _scaleEaseType));
        }

        public IEnumerator RunByCroutine()
        {
            Run();
            yield return new WaitForSeconds(_fAnimDur);
        }
    }
}

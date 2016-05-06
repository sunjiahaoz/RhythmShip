using UnityEngine;
using UnityEditor;
using System.Collections;
namespace sunjiahaoz
{
    public class ObjeMoveAnim : ObjectAnimBase
    {
        public Vector3 _vecStart = Vector3.zero;
        public Vector3 _vecGoal = Vector3.one;
        public float _fDelay = 0f;
        public float _fDuration = 0.5f;
        public iTween.LoopType _loopType = iTween.LoopType.loop;
        public iTween.EaseType _type = iTween.EaseType.spring;        
        public bool _bIgnoreTimeScale = false;

        public override void Run()
        {
            transform.localPosition = _vecStart;
            //iTween.ScaleTo(gameObject, iTween.Hash("scale", _vecGoalScale, "time", _fDuration, "easetype", _type, "looptype", _loopType));
            iTween.MoveTo(gameObject, iTween.Hash("ignoretimescale", _bIgnoreTimeScale, "position", _vecGoal, "islocal", true, "delay", _fDelay, "time", _fDuration, "easetype", _type, "looptype", _loopType));
        }

        public void RunBack()
        {
            transform.localPosition = _vecGoal;            
            iTween.MoveTo(gameObject, iTween.Hash("ignoretimescale", _bIgnoreTimeScale, "position", _vecStart, "islocal", true, "delay", _fDelay, "time", _fDuration, "easetype", _type, "looptype", _loopType));
        }

#region _Menu_
        [ContextMenu("位置置为Start")]
        void SetPosToStart()
        {
            transform.localPosition = _vecStart;
        }
        [ContextMenu("位置置为Goal")]
        void SetPosToGoal()
        {
            transform.localPosition = _vecGoal;
        }
        [ContextMenu("Start为当前位置")]
        void SetStartPos()
        {
            _vecStart = transform.localPosition;            
        }
        [ContextMenu("Goal为当前位置")]
        void SetGoalPos()
        {
            _vecGoal = transform.localPosition;
        }
#endregion
    }
}

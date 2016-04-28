using UnityEngine;
using System.Collections;

namespace sunjiahaoz.SteerTrack
{
    public class SteeringDirLine : SteeringBehaviour
    {
        public Vector2 _vec2DirOff = Vector2.zero;
        public void SetDir(Vector3 dir)
        {
            _vec2DirOff = dir;
        }

        public override Vector3 GetVelocity()
        {
            return _vec2DirOff * agent.MaxVelocity;
        }
    }
}
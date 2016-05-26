using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;
using sunjiahaoz.SteerTrack;
using RUL;
using DG.Tweening;

public class TEST : MonoBehaviour {

    public PlayerShip _ship;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Home))
        {            
            _ship._skill.EquipSkillMotor(1);
        }
        if (Input.GetKey(KeyCode.End))
        {            
            _ship._shooter.EquipFirePoint(0);
        }
        
    }
#region _Trail_
    //public float speed;
    //public SplineTrailRenderer trailReference;    

    //private float distance = 0;
    //private Vector3 lastPosition;

    //void Start()
    //{
    //    lastPosition = transform.position;
    //}

    //void Update()
    //{
    //    float length = trailReference.spline.Length();

    //    distance = Mathf.Clamp(distance + speed * Time.deltaTime, 0, length - 0.1f);
    //    trailReference.maxLength = Mathf.Max(length - distance, 0);

    //    Vector3 forward = trailReference.spline.FindTangentFromDistance(distance);
    //    Vector3 position = trailReference.spline.FindPositionFromDistance(distance);

    //    //if (forward != Vector3.zero)
    //    {
    //        //transform.forward = forward;
    //        transform.position = lastPosition = position;
    //    }
    //}
#endregion
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class ChildrenRegularOnCircle : MonoBehaviour {    

    public float _fRadius = 1;
    public bool _bDebug = false;
    public bool _bLookAtCenter = false;
    public float _bLookAtRotateOff = 0;

    void Update()
    {
        if (_bDebug)
        {
            ReGeneratePos();
        }
    }
    
    [ContextMenu("重新生成")]
    public void ReGeneratePos()
    {   
        Vector3[] poses = UtilityTool.RegularPolygonPosPoint(transform.position, _fRadius, transform.childCount);
        if (poses != null)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).position = poses[i];                
                if (_bLookAtCenter)
                {
                    Vector3 dir = ToolsUseful.LookDir(transform.GetChild(i).position, transform.position);
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.GetChild(i).eulerAngles = new Vector3(transform.GetChild(i).eulerAngles.x, transform.GetChild(i).eulerAngles.y, angle + _bLookAtRotateOff);
                }                
            }
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _fRadius);
    }
}

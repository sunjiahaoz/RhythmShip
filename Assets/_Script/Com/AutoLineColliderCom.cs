/*
AutoLineColliderCom
By: @sunjiahaoz, 2016-4-22

根据两个点的位置自动生成BoxCollider的大小及位置、角度
 * 就是两个点组成的线段生成一段Collider
*/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class AutoLineColliderCom : MonoBehaviour {
    public float _fDefaultHeight = 10;
    public float _fDefaultDepth = 10;

    BoxCollider _collider;
    public BoxCollider lineCollider
    {
        get { return _collider; }
    }
    void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        Reset();
    }

    public void Reset()
    {
        _collider.size = Vector3.zero;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }   

    public void CalCollider(Vector3 pos1, Vector3 pos2)
    {
        CalCollider(pos1, pos2, _fDefaultHeight, _fDefaultDepth);
    }

    public void CalCollider(Vector3 pos1, Vector3 pos2, float fHeight, float fDepth)
    {
        Vector3 size = Vector3.one;
        float fDistance = Vector3.Distance(pos1, pos2);
        size.z = fDistance;
        size.y = fHeight;
        size.x = fDepth;
        _collider.size = size;

        transform.rotation = Quaternion.LookRotation((pos2 - pos1).normalized);
        transform.position = Vector3.Lerp(pos1, pos2, 0.5f);
    }
}

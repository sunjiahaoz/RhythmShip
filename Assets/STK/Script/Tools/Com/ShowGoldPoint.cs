using UnityEngine;
using System.Collections;
using sunjiahaoz;

[ExecuteInEditMode]
public class ShowGoldPoint : MonoBehaviour {
    public Transform _trPt1;
    public Transform _trPt2;
    
    Vector3[] _goldPos = new Vector3[2];    

    void Generate()
    {
        _goldPos[0] = Vector3.Lerp(_trPt1.position, _trPt2.position, ToolsUseful.GoldPointRadio);
        _goldPos[1] = Vector3.Lerp(_trPt2.position, _trPt1.position, ToolsUseful.GoldPointRadio);
    }

    void Update()
    {
        if (_trPt1 != null
            && _trPt2 != null)
        {
            Generate();
        }
    }

    public float _fGizmosRadios = 40;
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_goldPos[0], _fGizmosRadios);
        Gizmos.DrawSphere(_goldPos[1], _fGizmosRadios);

    }
}

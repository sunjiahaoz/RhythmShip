using UnityEngine;
using System.Collections;

public class ComGetCreatePos : MonoBehaviour {
    public virtual Vector3 GetNextPos()
    {
        return transform.position;
    }
}

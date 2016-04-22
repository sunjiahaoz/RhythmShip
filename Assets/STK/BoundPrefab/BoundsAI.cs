using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BoundSide
{
    Top,
    Right,
    Bottom,
    Left,    
}
public class BoundsAI : MonoBehaviour {
    public Transform[] _Sides;
	
	void Start () {
		if (_Sides == null
        || _Sides.Length != 4)
		{
            Debug.LogError("Bound Sides count is not 4 !!!!");
            return;
		}
	}

    public void CloseSide(BoundSide side, bool bClose = true)
    {
        _Sides[(int)side].gameObject.SetActive(!bClose);
    }
	
}

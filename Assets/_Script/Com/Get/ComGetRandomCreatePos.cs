using UnityEngine;
using System.Collections;

public class ComGetRandomCreatePos : ComGetCreatePos {

    public Transform _trLeftPos;
    public Transform _trRightBottom;

    public override Vector3 GetNextPos()
    {
        return GenerateRandomPos();
    }

    Vector3 GenerateRandomPos()
    {
        Vector3 pos = Vector3.zero;
        if (_trLeftPos != null
            && _trRightBottom != null)
        {
            pos.x = Random.Range(_trLeftPos.position.x, _trRightBottom.position.x);
            pos.y = Random.Range(_trLeftPos.position.y, _trRightBottom.position.y);
        }
        else
        {
            base.GetNextPos();
        }
        return pos;
    }    

}

/*
OAElem_ScrollItem
By: @sunjiahaoz, 2016-5-24

ScrollItem.transfom.position本身作为起始点，
 * 同时指定一个最后点LastPos
*/
using UnityEngine;
using System.Collections;

public class OAElem_ScrollItem : MonoBehaviour {
    public Transform _trLastPos;

    public bool CheckGoal(Vector3 posGoal, float fGoalRadius = 1f)
    {
        // 如果lastPos在Goal范围内，则表示到达
        if (Vector3.Distance(_trLastPos.position, posGoal) <= fGoalRadius)
        {
            return true;
        }
        return false;
    }

    public void FollowScrollItem(OAElem_ScrollItem followScroll)
    {
        transform.position = followScroll._trLastPos.position;
    }
}

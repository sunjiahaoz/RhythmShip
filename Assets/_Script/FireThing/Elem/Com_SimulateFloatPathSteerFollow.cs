/*
*Com_SimulateFloatPathSteerFollow
*by sunjiahaoz 2016-5-19
*
 * 生成模拟飘落路径，让本对象移动
*本对象必须有SteeringFollowPath
 *
*/
using UnityEngine;
using System.Collections;
using sunjiahaoz;
using sunjiahaoz.SteerTrack;

public class Com_SimulateFloatPathSteerFollow : MonoBehaviour {
    SteeringFollowPath _steerFP;
    void Awake()
    {
        _steerFP = GetComponent<SteeringFollowPath>();
        if (_steerFP == null)
        {
            TagLog.LogError(LogIndex.Normal, gameObject.name + "需要SteeringFollowPath", this);
        }
    }

    public void FollowPathWithPos(Vector3 posStart, Vector3 posGoal, int nNodeCount, float fOffRangeMin, float fOffRangeMax)
    {
        _steerFP.Path = UtilityTool.GenerateSimulateFloatPath(posStart, posGoal, nNodeCount, fOffRangeMin, fOffRangeMax);        
    }

    public void FollowPathWithTr(Transform trStart, Transform trGoal,int nNodeCount, float fOffRangeMin, float fOffRangeMax)
    {
        FollowPathWithPos(trStart.position, trGoal.position, nNodeCount, fOffRangeMin, fOffRangeMax);
    }
}

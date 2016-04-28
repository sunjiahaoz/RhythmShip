using UnityEngine;
using System.Collections;
using sunjiahaoz.SteerTrack;
using sunjiahaoz;

[RequireComponent(typeof(SteeringDirLine))]
public class SteerCom_ToTargetDir : MonoBehaviour {    

    SteeringDirLine _dir;
    void Awake()
    {
        _dir = GetComponent<SteeringDirLine>();
    }

    void OnEnable()
    {
        Vector3 dir = Vector3.left;
        if (GamingData.Instance.gameBattleManager.playerShip != null)
        {
            _dir._vec2DirOff = ToolsUseful.LookDir(transform.position, GamingData.Instance.gameBattleManager.playerShip.transform.position);
        }
    }

}

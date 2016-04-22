using UnityEngine;
using System.Collections;

public class DeathAreaAI : MonoBehaviour {
    public LayerMask _checkLayer;
    void OnTriggerExit(Collider other)
    {
        if (sunjiahaoz.ToolsUseful.CheckLayerContainedGo(_checkLayer, other.gameObject))
        {
            sunjiahaoz.TagLog.Log(LogIndex.DeathArea, other.gameObject + " is OnTriggerExit " + gameObject.name);
            ProcessCollision(other.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (sunjiahaoz.ToolsUseful.CheckLayerContainedGo(_checkLayer, collision.gameObject))
        {
            sunjiahaoz.TagLog.Log(LogIndex.DeathArea, collision.gameObject + " is OnCollisionEnter " + gameObject.name);
            ProcessCollision(collision.gameObject);      
        }        
    }

    void ProcessCollision(GameObject go)
    {
        ObjectPoolController.Destroy(go.transform.root.gameObject);
    }
}

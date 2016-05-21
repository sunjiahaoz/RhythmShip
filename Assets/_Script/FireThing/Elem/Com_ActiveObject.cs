using UnityEngine;
using System.Collections;

public class Com_ActiveObject : MonoBehaviour {
    public void SetObjActive(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void SetObjInActive(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void SetObjActiveOrInActive(GameObject obj)
    {
        if (obj.activeInHierarchy)
        {
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
        }
    }
}

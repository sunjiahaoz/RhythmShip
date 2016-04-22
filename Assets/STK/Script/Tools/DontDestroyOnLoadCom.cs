using UnityEngine;
using System.Collections;

namespace sunjiahaoz
{
    public class DontDestroyOnLoadCom : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

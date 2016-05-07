using UnityEngine;
using System.Collections;

namespace sunjiahaoz
{

    public class ObjectAnimBase : MonoBehaviour
    {
        public RunOpportunity _opt = RunOpportunity.Start;

        void Awake()
        {
            if (_opt == RunOpportunity.Awake)
            {
                Run();
            }
        }

        void Start()
        {
            if (_opt == RunOpportunity.Start)
            {
                Run();
            }
        }

        void OnEnable()
        {
            if (_opt == RunOpportunity.Enable)
            {
                Run();
            }
        }

        public virtual void Run()
        {

        }

        public virtual void Stop(bool bComplete = false)
        {

        }
    }
}
using UnityEngine;
using System.Collections;

namespace sunjiahaoz
{

    public class ObjectAnimBase : MonoBehaviour
    {
        public RunOpportunity _opt = RunOpportunity.Start;        

        protected virtual void Awake()
        {
            if (_opt == RunOpportunity.Awake)
            {
                Run();
            }
        }

        protected virtual void Start()
        {
            if (_opt == RunOpportunity.Start)
            {
                Run();
            }
        }

        protected virtual void OnEnable()
        {
            if (_opt == RunOpportunity.Enable)
            {
                Run();
            }
        }

        public virtual void Run()
        {

        }

        public virtual void RunBack()
        {

        }

        public virtual void Stop(bool bComplete = false)
        {

        }
    }
}
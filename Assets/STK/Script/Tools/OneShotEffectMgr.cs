using UnityEngine;
using System.Collections;

namespace sunjiahaoz
{    
    public struct OneShotParticleParam
    {
        public OneShotParticleParam(string strFolder, string strName)
        {
            _strFolder = strFolder;
            _strName = strName;
            _bDestroyAuto = true;            
            _vecPos = Vector3.zero;
            _bindPos = null;
            _vecRotate = Vector3.zero;
            _vecScale = Vector3.one;
            _effectComplete = null;
            _afterLoad = null;
        }

        public string _strFolder;
        public string _strName;        
        public bool _bDestroyAuto; // 是否使用自动销毁        
        public Vector3 _vecPos;         // 创建时出现的位置
        public Transform _bindPos;      // 绑定位置
        public Vector3 _vecRotate;
        public Vector3 _vecScale;
        public System.Action<GameObject> _effectComplete;   // 特效播放完成之后执行
        public System.Action<GameObject> _afterLoad;            // 特效加载完成之后执行
    }

    public class OneShotEffectMgr : MonoBehaviour
    {
        public static OneShotEffectMgr _Instance;
        public static OneShotEffectMgr Instance()
        {
            if (_Instance != null)
            {
                return _Instance;
            }
            else
            {
                _Instance = GameObject.FindObjectOfType<OneShotEffectMgr>();
                if (_Instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "OneShotEffectManager";
                    _Instance = go.AddComponent<OneShotEffectMgr>();
                }
            }
            return _Instance;
        }
        public bool _bDontDestroyOnLoad = false;
        void Awake()
        {
            _Instance = this;
            if (_bDontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }            
        }

        public void OneShotParticleEffect(OneShotParticleParam param)
        {
            CreateEffectEx(param);
            return;
        }

        public void OneShotParticleEffect(Hashtable tableParam, System.Action<GameObject> effectComplete = null, System.Action<GameObject> loadComplete = null)
        {
            OneShotParticleParam param = new OneShotParticleParam("", "");
            param._strName = "";
            if (tableParam.Contains("name"))
            {
                param._strName = (string)tableParam["name"];
            }
            param._strFolder = "";
            if (tableParam.Contains("folder"))
            {
                param._strFolder = (string)tableParam["folder"];
            }

            param._vecPos = Vector3.zero;
            if (tableParam.Contains("position"))
            {
                param._vecPos = (Vector3)tableParam["position"];
            }

            param._vecRotate = Vector3.zero;
            if (tableParam.Contains("rotation"))
            {
                param._vecRotate = (Vector3)tableParam["rotation"];
            }
            param._vecScale = Vector3.one;
            if (tableParam.Contains("scale"))
            {
                param._vecScale = (Vector3)tableParam["scale"];
            }
            if (tableParam.Contains("eulerAnglesy"))
            {
                param._vecRotate = new Vector3(0, (float)tableParam["eulerAnglesy"], 0);
            }            
            param._bindPos = null;
            if (tableParam.Contains("bindPoint"))
            {
                param._bindPos = (Transform)tableParam["bindPoint"];
            }
            param._bDestroyAuto = true;
            if (tableParam.Contains("destroyAuto"))
            {
                param._bDestroyAuto = (bool)tableParam["destroyAuto"];
            }            

            param._effectComplete = effectComplete;
            param._afterLoad = loadComplete;

            OneShotParticleEffect(param);
        }

        void CreateEffectEx(OneShotParticleParam param)
        {
            ResourceManager.instance.AddRequest(param._strName, param._strFolder, OnCreateEffectEx, param);
        }

        void OnCreateEffectEx(UnityEngine.Object assert, System.Object arg)
        {
            if (assert == null)
            {
                return;
            }

            OneShotParticleParam param = (OneShotParticleParam)arg;

            // 根据vecPos创建
            GameObject go = null;
            go = ObjectPoolController.Instantiate(assert as GameObject, param._vecPos, Quaternion.identity);            

            go.transform.position = param._vecPos;
            go.transform.localRotation = Quaternion.identity;
            go.SetActive(true);
            // 旋转
            go.transform.eulerAngles = param._vecRotate;
            // 缩放
            go.transform.localScale = param._vecScale;            
            // 如果有绑定点，就绑定
            if (param._bindPos != null)
            {
                go.transform.parent = param._bindPos;
            }
            if (param._afterLoad != null)
            {
                param._afterLoad(go);
            }

            ////根据不同的组件，进行不同的开始处理///////////////////////////////////////////////////////////////////////
            float _fDuration = 0;
            AutoDestroy autoDes = go.GetComponent<AutoDestroy>();
            if (autoDes != null)
            {
                _fDuration = autoDes.m_period;
            }
            else
            {
                ParticleSystem[] rootSys = go.GetComponentsInChildren<ParticleSystem>();
                for (int i = 0; i < rootSys.Length; i++)
                {
                    _fDuration = Mathf.Max(_fDuration, rootSys[i].duration + rootSys[i].startDelay + rootSys[i].startLifetime);

                    rootSys[i].Play(true);
                }
            }
            ///////////////////////////////////////////////////////////////////////////


            StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
            {
                if (param._effectComplete != null)
                {
                    param._effectComplete(go);
                }

                if (param._bDestroyAuto)
                {
                    RemoveEffect(go, param);
                }
            },
            _fDuration));
        }

        GameObject CreateEffect(OneShotParticleParam param)
        {
            // 加载特效 todo 内存池获取
            //Debug.LogWarning("加载特效");
            UnityEngine.Object obj = ResourceManager.instance.RequestImediate(param._strName, param._strFolder);
            if (obj != null)
            {
                // 根据vecPos创建
                GameObject go = (GameObject)Instantiate(obj, param._vecPos, Quaternion.identity);
                // 旋转
                go.transform.eulerAngles = param._vecRotate;                
                // 如果有绑定点，就绑定
                if (param._bindPos != null)
                {
                    go.transform.parent = param._bindPos;
                }
                return go;
            }
            return null;
        }
        void RemoveEffect(GameObject goEffect, OneShotParticleParam param)
        {
            if (goEffect == null)
            {
                return;
            }
            goEffect.transform.parent = null;
            ObjectPoolController.Destroy(goEffect);            
        }
    }
}
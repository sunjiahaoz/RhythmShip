using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace sunjiahaoz
{
    public class ResourceManager
    {
        public static ResourceManager instance = new ResourceManager();
        public delegate void RequestDelegate(UnityEngine.Object assert, System.Object arg);
        Dictionary<string, UnityEngine.Object> m_Objs = new Dictionary<string, UnityEngine.Object>();
        public void AddRequest(string szName, string m_Folder, RequestDelegate onLoaded, System.Object arg)
        {//取物体//

            //first wo must check if the object has a copy
            if (m_Objs.ContainsKey(szName))
            {//字典里有//
                UnityEngine.Object mObj = m_Objs[szName];
                if (null != mObj)
                {
                    if (null != onLoaded)
                    {
                        onLoaded(mObj, arg);
                    }
                    return;
                }
                else
                {
                    m_Objs.Remove(szName);
                }
            }

            // when it comes here , that means  the resource is the base package
            // just use resource to load it;
            System.DateTime time = System.DateTime.Now;
            string strPath = "";
            if (m_Folder.Length == 0)
            {
                strPath = szName;
            }
            else
            {
                strPath = m_Folder + "/" + szName;
            }
            Object resO = Resources.Load(strPath);

            //PublicHolder.Instance.GlobalData.Logger ("Load resource from file" + m_Folder + " " + szName,true);

            if (null != resO)
            {
                m_Objs[szName] = resO;
                if (null != onLoaded)
                    onLoaded(m_Objs[szName], arg);

                if (m_Folder == "Monster")
                {
                    System.TimeSpan sp = System.DateTime.Now - time;
                    Debug.Log("MonsterLoadTime:" + sp.TotalSeconds.ToString());
                }
                return;
            }
            else
            {
                Debug.LogError("//没有读取到//" + szName + "//这个资源,都怪王欣//");
            }
            // whene come here  it means  this resouce does not exist;
        }

        public UnityEngine.Object RequestImediate(string szName, string m_Folder)
        {
            //first wo must check if the object has a copy
            if (m_Objs.ContainsKey(szName))
            {
                UnityEngine.Object mObj = m_Objs[szName];
                if (null != mObj)
                {
                    return mObj;
                }
                else
                {
                    m_Objs.Remove(szName);
                }
            }

            // when it comes here , that means  the resource is the base package
            // just use resource to load it;
            System.DateTime time = System.DateTime.Now;

            Object resO = Resources.Load(m_Folder + "/" + szName);

            //PublicHolder.Instance.GlobalData.Logger ("Load resource from file" + m_Folder + " " + szName,true);

            if (null != resO)
            {

                m_Objs[szName] = resO;
                if (m_Folder == "Monster")
                {
                    System.TimeSpan sp = System.DateTime.Now - time;
                    Debug.Log("MonsterLoadTime:" + sp.TotalSeconds.ToString());
                }
                return resO;
            }
            return null;
        }

    }
}

using UnityEngine;
using System.Collections;

namespace sunjiahaoz.Persist
{

    public interface IPersist
    {
        bool HasKey(string strKey);
        void DeleteKey(string key);
        void DeleteAll();

        void SetString(string strKey, string strValue);
        string GetString(string strKey, string strDefault = "");

        void SetInt(string strKey, int nValue);
        int GetInt(string strKey, int nDefault = 0);

        void SetBool(string strKey, bool bValue);
        bool GetBool(string strKey, bool bDefault = false);

        void SetFloat(string strKey, float fValue);
        float GetFloat(string strKey, float fDefault = 0f);

        void SetBinary(string strKey, System.Object objValue);
        System.Object GetBinary(string strKey, string strDefaultValue = null);

        ////File///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// objData必须是[Serializable]的
        /// </summary>
        /// <param name="objData"></param>
        /// <param name="strFileName"></param>
        void SaveBinaryToFile(System.Object objData, string strFileName);
        System.Object LoadBinaryFromFile(string strFileName);

    }
}

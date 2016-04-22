using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
namespace sunjiahaoz.Persist
{

    public class PlayerPrefsPersist : IPersist
    {
        public bool HasKey(string strKey)
        {
            return PlayerPrefs.HasKey(strKey);
        }

        public void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public void SetString(string strKey, string strValue)
        {
            PlayerPrefs.SetString(strKey, strValue);
        }

        public string GetString(string strKey, string strDefault = "")
        {
            return PlayerPrefs.GetString(strKey, strDefault);
        }

        public void SetInt(string strKey, int nValue)
        {
            PlayerPrefs.SetInt(strKey, nValue);
        }

        public int GetInt(string strKey, int nDefault = 0)
        {
            return PlayerPrefs.GetInt(strKey, nDefault);
        }

        public void SetBool(string strKey, bool bValue)
        {
            PlayerPrefs.SetInt(strKey, (bValue) ? 1 : 0);
        }

        public bool GetBool(string strKey, bool bDefault = false)
        {
            return PlayerPrefs.GetInt(strKey, bDefault ? 1 : 0) == 0 ? false : true;
        }

        public void SetFloat(string strKey, float fValue)
        {
            PlayerPrefs.SetFloat(strKey, fValue);
        }

        public float GetFloat(string strKey, float fDefault = 0f)
        {
            return PlayerPrefs.GetFloat(strKey, fDefault);
        }

        public void SetBinary(string strKey, object objValue)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, objValue);
            PlayerPrefs.SetString(strKey, Convert.ToBase64String(ms.GetBuffer()));
        }

        public object GetBinary(string strKey, string strDefaultValue = null)
        {
            string strData = PlayerPrefs.GetString(strKey, strDefaultValue);
            if (!string.IsNullOrEmpty(strData))
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(Convert.FromBase64String(strData));
                return bf.Deserialize(ms);
            }
            return null;
        }


        public void SaveBinaryToFile(object objData, string strFileName)
        {
            string dataPath = Application.persistentDataPath;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(dataPath + "/" + strFileName + ".bin");
            bf.Serialize(file, objData);
            file.Close();
        }

        public object LoadBinaryFromFile(string strFileName)
        {
            if (File.Exists(Application.persistentDataPath + "/" + strFileName + ".bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + strFileName + ".bin", FileMode.Open);
                object data = bf.Deserialize(file);
                file.Close();
                return data;
            }
            return null;
        }
    }
}

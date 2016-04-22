using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace sunjiahaoz.Persist
{
    public interface IJSonSerializeData
    {
        // 将自己的数据转成JSON 数据
        string ToJson();
        // 利用Json数据装配自己
        void LoadFromJson(string strContent);
    }

    public enum SerializationType
    {
        XML,        // 需要 [System.Serializable]
        Binnary,    // 需要 [System.Serializable]
        JSon,       // 需要实现IJSonSerializeData接口，并且Deserialize返回的是数据字符串，不是具体对象
    }
    public class SerializeFileTool
    {
        public static string Serialize(Type objectType, object serializableObject, SerializationType typeOfSerialization)
        {
            string returnData = string.Empty;
            switch (typeOfSerialization)
            {
                case SerializationType.XML:
                    XmlSerializer serializer = new XmlSerializer(objectType);
                    TextWriter writer = new StringWriter();
                    serializer.Serialize(writer, serializableObject);
                    returnData = writer.ToString();
                    serializer = null;
                    writer.Close();
                    writer = null;
                    break;
                case SerializationType.Binnary:
                    //Binary Format Deserialize using Memorystreem
                    var formatter = new BinaryFormatter();
                    var mf = new System.IO.MemoryStream();
                    formatter.Serialize(mf, serializableObject);
                    returnData = System.Convert.ToBase64String(mf.ToArray());
                    mf.Close();
                    formatter = null;
                    mf = null;
                    break;
                case SerializationType.JSon:
                    {
                        if (!(serializableObject is IJSonSerializeData))
                        {
                            Debug.LogError("<color=red>[Error]</color>---" + objectType + " 必须实现 JSonSerializeData 接口才可以！");
                            return string.Empty;
                        }
                        returnData = (serializableObject as IJSonSerializeData).ToJson();
                    }
                    break;
            }
            return returnData;
        }

        public static object Deserialize(Type objectType, string serializedString, SerializationType typeOfSerialization)
        {
            object retValue = new object();
            switch (typeOfSerialization)
            {
                case SerializationType.XML:
                    XmlSerializer serializer = new XmlSerializer(objectType);
                    TextReader reader = new StringReader(serializedString);
                    retValue = serializer.Deserialize(reader);
                    reader.Close();
                    reader = null;
                    serializer = null;
                    break;
                case SerializationType.Binnary:
                    //Binary Format Deserialize using Memorystreem
                    var formatter = new BinaryFormatter();
                    var mf = new System.IO.MemoryStream(System.Convert.FromBase64String(serializedString));
                    retValue = formatter.Deserialize(mf);
                    mf.Close();
                    mf = null;
                    formatter = null;
                    break;
                case SerializationType.JSon:    // 这里因为IJSonSerializeData是个接口，所以为了避免使用反射，这里就不能进行实例化了，直接返回字符串好了，在外面再调用接口的LoadFromJson进行处理
                    {
                        retValue = serializedString;
                    } break;
            }
            return retValue;
        }

    }
}

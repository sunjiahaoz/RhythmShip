/*
RhythmDataNormalContainer
By: @sunjiahaoz, 2016-4-20

很多对象使用的同样的节奏文件，所以为了避免同一个节奏文件被多次加载
 * 所以使用这个container来保存所有加载的节奏文件，这样在其他对象需要的时候既可以直接拿来用了
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class RhythmDataNormalContainer
{
    #region _Instance_
    public static RhythmDataNormalContainer Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new RhythmDataNormalContainer();
            }
            return _Instance;
        }
    }
    private static RhythmDataNormalContainer _Instance;
    #endregion

    Dictionary<string, RhythmDataMgr_Normal> _dictContainer = new Dictionary<string, RhythmDataMgr_Normal>();

    public void Init()
    {
        _dictContainer.Clear();
    }

    public RhythmDataMgr_Normal GetData(string strFolder, string strFileName)
    {
        string str = GenerateKey(strFolder, strFileName);
        if (_dictContainer.ContainsKey(str))
        {
            return _dictContainer[str];
        }
        else
        {
            return Load(strFolder, strFileName);
        }
    }

    RhythmDataMgr_Normal Load(string strFolder, string strFileName)
    {
        UnityEngine.Object obj = ResourceManager.instance.RequestImediate(strFileName, strFolder);
        if (obj == null)
        {
            TagLog.LogWarning(LogIndex.RecordRhythm, strFolder + "中找不到" + strFileName);
            return null;
        }
        RhythmDataMgr_Normal normal = new RhythmDataMgr_Normal();
        TextAsset ta = obj as TextAsset;
        normal.LoadByConfigData(ta.text);
        _dictContainer.Add(GenerateKey(strFolder, strFileName), normal);
        return normal;
    }

    string GenerateKey(string strFolder, string strFileName)
    {
        return strFolder + "/" + strFileName;
    }

}

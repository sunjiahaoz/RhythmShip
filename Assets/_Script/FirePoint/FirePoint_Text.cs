using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class FirePoint_Text : FirePointRhythm
{
    public TextAsset _content;

    List<string> _lstLines = new List<string>();
    int _nCurIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        if (_prefabFireThing.GetType() != typeof(EnemyText))
        {
            TagLog.LogError(LogIndex.FirePoint, "发射点"+gameObject.name + "的发射物必须为EnemyText！！！");
            return;
        }
        ReLoadContent();
    }

    void ReLoadContent()
    {
        _nCurIndex = 0;
        _lstLines.Clear();
        ArrayList lstLines = SimpleFileProcess.LoadFile(_content);
        for (int i = 0; i < lstLines.Count; ++i)
        {
            _lstLines.Add((string)lstLines[i]);
        }
    }

    protected override void CreateObject(Vector3 createPos, System.Action<BaseFireThing> afterCreate = null)
    {
        base.CreateObject(createPos, (thing) =>
        {
            if (_nCurIndex >= _lstLines.Count)
            {
                TagLog.LogWarning(LogIndex.FirePoint, gameObject.name + "还要文本？？！！", this);
                return;
            }
            EnemyText et = (EnemyText)thing;
            et.SetText(_lstLines[_nCurIndex]);
            _nCurIndex++;

            if (afterCreate != null)
            {
                afterCreate(thing);
            }
        });
    }
}

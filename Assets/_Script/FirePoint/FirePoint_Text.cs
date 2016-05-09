using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class FirePoint_Text : FirePointRhythm
{
    protected override void Awake()
    {
        base.Awake();
        if (_prefabFireThing.GetType() != typeof(EnemyText))
        {
            TagLog.LogError(LogIndex.FirePoint, "发射点"+gameObject.name + "的发射物必须为EnemyText！！！");
            return;
        }
    }

    string _strText = "";
    public void SetString(string text)
    {
        _strText = text;
    }
    protected override void CreateObject(Vector3 createPos, System.Action<BaseFireThing> afterCreate = null)
    {
        base.CreateObject(createPos, (thing) =>
        {
            if (_strText.Length == 0)
            {
                return;
            }
            EnemyText et = (EnemyText)thing;
            et.SetText(_strText);            

            if (afterCreate != null)
            {
                afterCreate(thing);
            }
        });
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class WndConfigDataMgr : SingletonMonoBehaviour<WndConfigDataMgr>{
    // 获得的配置不能是完全随机的
    // 现在的方法是打乱配置的顺序将索引保存到list中，从list中依次获得一个
    // 当每个都获得一遍之后就重新打乱顺序再来一遍
    List<int> _lstPopKey = new List<int>();

    int _nNextWndIndex = 0;

    void Awake()
    {
        TanGeJinBiConfig.load();
        Shuffle();
        _nNextWndIndex = 0;
    }

    
    // 获得一个config
    public TanGeJinBiConfig GetRandomWndConfig()
    {
        if (_lstPopKey.Count == 0)
        {
            Shuffle();
        }
        int nIndex = _lstPopKey[0];
        _lstPopKey.RemoveAt(0);        
        return TanGeJinBiConfig.dic[nIndex];
    }

    // 获得下一个窗口ID并使ID+1
    public int GetNextWndIndex()
    {
        return _nNextWndIndex++;
    }

    void Shuffle()
    {
        _lstPopKey.Clear();        
        for (int i = 0; i < TanGeJinBiConfig.dic.Count; ++i)
        {
            _lstPopKey.Add(i);  // 所以要求config的ID必须从0开始，而且是必须按+1递增
        }
        ToolsUseful.Shuffle(_lstPopKey);
    }
}

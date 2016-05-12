using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public class TextFirePointContent : FirePointRhythm
{
    public TextAsset _textContent;
    public int _nSmallLength = 3;   // 字符串小于等于这个值就表示是smallText
    public FirePoint_Text[] _sliderTextFire;
    public FirePoint_Text[] _smallTextFire;
    
    List<string> _lstLines = new List<string>();
    int _nCurIndex = 0;


    protected override void Start()
    {
        base.Start();
        Load();
        _nCurIndex = 0;
    }

    void Load()
    {
        _nCurIndex = 0;
        _lstLines.Clear();
        ArrayList lstLines = SimpleFileProcess.LoadFile(_textContent);
        for (int i = 0; i < lstLines.Count; ++i)
        {
            _lstLines.Add((string)lstLines[i]);
        }
    }

    public string GetNextString()
    {
        if (_nCurIndex < 0
        || _nCurIndex >= _lstLines.Count)
        {
            return string.Empty;
        }        
        return _lstLines[_nCurIndex++];
    }

    int _nCurSmallFPIndex = 0;
    int _nCurSliderFPIndex = 0;
    public override void Fire()
    {
        string str = GetNextString();
        //TagLog.Log(LogIndex.FirePoint, "FireSting:" + _lstLines[_nCurIndex]);
        FirePoint_Text fp = null;
        if (str.Length <= _nSmallLength)
        {
            _smallTextFire[_nCurSmallFPIndex].SetString(str);
            _smallTextFire[_nCurSmallFPIndex].Fire();
            _nCurSmallFPIndex++;
            if (_nCurSmallFPIndex >= _smallTextFire.Length)
            {
                _nCurSmallFPIndex = 0;
            }
            
        }
        else
        {
            _sliderTextFire[_nCurSliderFPIndex].SetString(str);
            _sliderTextFire[_nCurSliderFPIndex].Fire();

            _nCurSliderFPIndex++;
            if (_nCurSliderFPIndex >= _sliderTextFire.Length)
            {
                _nCurSliderFPIndex = 0;
            }
        }
    }
}

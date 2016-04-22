using UnityEngine;
using System.Collections;
public class TimeStamp : System.IDisposable
{
    string _strHead = string.Empty;
    System.DateTime _dtStart;
    
    public TimeStamp(string strHead)
    {
        _dtStart = System.DateTime.Now;
        _strHead = strHead;
    }


    public void Dispose()
    {
        double dOff = (System.DateTime.Now - _dtStart).TotalMilliseconds;
        Debug.Log("<color=green>[" + _strHead + ":" + dOff.ToString("f2") + "]</color>");
    }
}

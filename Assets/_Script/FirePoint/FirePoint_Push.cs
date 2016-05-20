using UnityEngine;
using System.Collections;
using sunjiahaoz;
using DG.Tweening;

[System.Serializable]
public class PushFirePoint
{
    public tk2dSprite _sprite;
    public BaseFirePoint _firePoint;
}

public class FirePoint_Push : FirePointRhythm {
    public ObjectAnim_Move _comMoveBody;
    public PushFirePoint[] _pushFirePoint;
    

    public override void Fire()
    {
        base.Fire();
        StartCoroutine(OnProgress());
    }

    IEnumerator OnProgress()
    {
        // 各sprite显示
        for (int i = 0; i < _pushFirePoint.Length; ++i )
        {
            _pushFirePoint[i]._sprite.gameObject.SetActive(true);
        }
        // 推出来
        _comMoveBody.Run();
        yield return new WaitForSeconds(_comMoveBody._fDelay + _comMoveBody._fDur);
        // 生成实际的thing
        for (int i = 0; i < _pushFirePoint.Length; ++i )
        {
            _pushFirePoint[i]._firePoint.Fire();
            // 隐藏sprite显示
            _pushFirePoint[i]._sprite.gameObject.SetActive(false);
        }
    }
}

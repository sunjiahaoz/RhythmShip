using UnityEngine;
using System.Collections;
using sunjiahaoz.SteerTrack;

public enum BillBulletType
{
    Star = 0,
    RedBall = 1, 
    SanDan = 2,
}
public class FirePointBill : BaseFirePoint {
    public BaseBullet _prefabRedBullet;
    public BillBulletType _nBulletType = 0;    // 0, 星星， 1，红弹， 2散弹

    public Transform _pos0Root;
    public float _fDur0;
    public Transform _pos1Root;
    public float _fDur1;
    public Transform _pos2Root;
    public float _fDur2;
    public Transform _pos3Root;
    public float _fDur3;    

    protected override void CreateObject(Vector3 createPos, System.Action<BaseFireThing> afterCreate = null)
    {
        if (_nBulletType == BillBulletType.Star
            || _nBulletType == BillBulletType.RedBall)
        {
            base.CreateObject(createPos, afterCreate);
        }
        else
        {
            StartCoroutine(OnCreateSanDan(createPos));
        }
    }

    IEnumerator OnCreateSanDan(Vector3 createPos)
    {
        yield return OnCreateSanDanSeg(_fDur0, _pos0Root, createPos);
        yield return OnCreateSanDanSeg(_fDur1, _pos1Root, createPos);
        yield return OnCreateSanDanSeg(_fDur2, _pos2Root, createPos);
        yield return OnCreateSanDanSeg(_fDur3, _pos3Root, createPos);        
    }

    IEnumerator OnCreateSanDanSeg(float fDur, Transform posRoot, Vector3 createPos)
    {
        for (int i = 0; i < posRoot.childCount; ++i)
        {
            Vector3 posTarget = posRoot.GetChild(i).position;
            Vector3 dir = (posTarget - createPos).normalized;

            BaseBullet bullet = ObjectPoolController.Instantiate(_prefabRedBullet.gameObject, createPos, Quaternion.identity).GetComponent<BaseBullet>();
            bullet.gameObject.SetActive(true);
            SteeringDirLine dirL = bullet.GetComponent<SteeringDirLine>();
            dirL._vec2DirOff = dir;
        }
        yield return new WaitForSeconds(fDur / 2);
    }
}

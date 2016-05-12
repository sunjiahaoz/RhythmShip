using UnityEngine;
using System.Collections;

public class EnemyText : BaseEnemyShip {
    
    public tk2dTextMesh _comText;
    public tk2dTextMesh ComText
    {
        get
        {            
            return _comText;
        }
    }

    string _strText = "";

    public override void OnThingCreate(IFirePoint fp)
    {        
        base.OnThingCreate(fp);
    }

    public void SetText(string strText)
    {
        _strText = strText;
        ReGenerateContent();
        _lifeCom.ReSet(_strText.Length, _strText.Length);
    }

    void ReGenerateContent()
    {
        // 重新设置BoxCollier
        ComText.text = _strText;
        ComText.Commit();

        // 重新计算collider
        // 我这里移出再加新的。。。。为了时间，我擦！
        BoxCollider collider = ComText.GetComponent<BoxCollider>();
        if (collider != null)
        {
            Destroy(collider);
            collider = null;
        }
        collider = ComText.gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.size = new Vector3(collider.size.x, collider.size.y, 500);        
    }

    protected override void ShotDestroyEffectWhenThingDestroy()
    {
        if (_effectDestroy._strName.Length > 0)
        {
            _effectDestroy._pos = transform.position;

            BoxCollider collider = ComText.GetComponent<BoxCollider>();
            if (collider != null)
            {                
                _effectDestroy._pos.x += (collider.size.x / 2);
            }            
            ShotEffect.Instance.Shot(_effectDestroy);
        }
    }
}

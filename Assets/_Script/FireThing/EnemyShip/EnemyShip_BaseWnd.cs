using UnityEngine;
using System.Collections;

public class EnemyShip_BaseWnd : BaseEnemyShip {
    [Header("=======EnemyShip_BaseWnd========")]

    public tk2dTextMesh _textTitle;
    public tk2dTextMesh _textContent;
    public tk2dSprite _spImage;

    public override void OnThingCreate(IFirePoint fp)
    {
        base.OnThingCreate(fp);
        InitContent();
    }

    protected virtual void InitContent()
    {
        TanGeJinBiConfig config = WndConfigDataMgr.Instance.GetRandomWndConfig();
        _textTitle.text = Localization.Get(config.title);
        _textContent.text = Localization.Get(config.content);
        // 长度<2表示不显示图片
        if (config.spriteName.Length > 2)
        {
            _spImage.gameObject.SetActive(true);
            _spImage.spriteId = _spImage.GetSpriteIdByName(config.spriteName);
        }
        else
        {
            _spImage.gameObject.SetActive(false);
        }
    }
}

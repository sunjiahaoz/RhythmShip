/*
IBullet
By: @sunjiahaoz, 2016-4-7

子弹通用
*/
using UnityEngine;
using System.Collections;
namespace sunjiahaoz.gamebase
{
    public interface IBullet
    {
        // 释放该子弹的主体，需要在创建出子弹的时候设置
        IAttackerEntity attacker { get; set; }

        // 子弹创建时
        void OnBulletCreate();

        // 子弹碰撞某entity时
        void OnBulletHurt(IInjuredEntity entity);

        // 子弹销毁时
        void OnBulletDestroy();
    }
}

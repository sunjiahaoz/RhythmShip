/*
IInjuredEntity
By: @sunjiahaoz, 2016-4-7

可以受到伤害的Entity
*/
using UnityEngine;
using System.Collections;

namespace sunjiahaoz.gamebase
{
    public interface IInjuredEntity
    {
        // 可以受到伤害，所以需要有个ILife属性
        ILife life { get; }

        // 扩展的就可能还有其他属性，比如防御值什么的 todo
    }
}

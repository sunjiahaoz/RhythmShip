/*
ILife
By: @sunjiahaoz, 2016-4-7

生命值
*/
using UnityEngine;
using System.Collections;
namespace sunjiahaoz.gamebase
{
    public interface ILife : IEntityProperty<int>
    {
        /// <summary>
        /// 是否已经死亡
        /// </summary>
        /// <returns></returns>
        bool IsDead();

        /// <summary>
        /// 当前生命值的百分比
        /// </summary>
        /// <returns></returns>
        float CurPercent();
    }
}

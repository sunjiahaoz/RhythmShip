/*
ISkill
By: @sunjiahaoz, 2016-4-7

技能基本
*/
using UnityEngine;
using System.Collections;

namespace sunjiahaoz.gamebase
{
    public interface ISkill
    {
        // 释放该技能的主体，需要在初始化的时候进行设置
        IAttackerEntity attacker { get; set; }
        // 是否可以释放
        bool CheckCanCast();
        // 开始释放时
        void OnCastStart();
        // 正在释放过程中
        void OnCasting();
        // 是否释放完了
        bool CheckCastIsEnd();
        // 释放完成
        void OnCastEnd();
    }
}
